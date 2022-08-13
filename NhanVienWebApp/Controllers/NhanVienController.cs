using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace NhanVienWebApp.Controllers
{
    [Route("NhanVien")]
    public class NhanVienController : Controller
    {

        
        public static string BaseUrl = "https://localhost:7215/api/NhanVien";
        public IActionResult Index()
        {
            return View();
        }

        [Route("Report")]
        public async Task<IActionResult> GetReport()
        {
            var accessToken = HttpContext.Session.GetString("Access_token");
            if (!string.IsNullOrEmpty(accessToken))
            {
                var url = BaseUrl;
                HttpClient client = new HttpClient();
                HttpResponseMessage file = new HttpResponseMessage();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await client.PostAsync(url, null);
                
                return File(response.Content.ReadAsByteArrayAsync().Result,"application/pdf");
            }
            return Unauthorized();
        }
    }
}
