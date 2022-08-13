using Microsoft.AspNetCore.Mvc;
using NhanVienAPI.CreatePDF;
using NhanVienAPI.Models;
using System.Diagnostics;
using WebApp.DataConnect;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NhanVienDBContext dbContext;
      
        public HomeController(ILogger<HomeController> logger, NhanVienDBContext dbContext)
        {
            _logger = logger;
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}