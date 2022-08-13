using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NhanVienAPI.Models;
using NhanVienWebApp.Models;
using System.Diagnostics;
using System.Text;

namespace NhanVienWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Access_token") == null)
            {
                return Redirect("~/Login/Index");
            }
            return View();
        }

        public async Task<IActionResult> Login(LoginUser userInfo)
        {
            using (var httpClient = new HttpClient())
            {
                var jsonObject = JsonConvert.SerializeObject(userInfo);
                StringContent stringContent = new StringContent(jsonObject,Encoding.UTF8,"application/json");
                using (var response = await httpClient.PostAsync("https://localhost:7215/api/token", stringContent)) 
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        TempData["StatusMessage"] = "Login Fail! Check your username or password";
                        return Redirect("~/Login/Index");
                    }
                    else
                    {
                        string token = await response.Content.ReadAsStringAsync();
                        if (token == "Invalid Credentials")
                        {
                            ViewBag.Message = "Incorrect Usename or Password";
                            return Redirect("~/Home/Index");
                        }
                        HttpContext.Session.SetString("Access_token", token);
                    }                 
                }
            }
            return RedirectToAction("Index", "NhanVien");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}