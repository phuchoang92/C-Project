using Microsoft.AspNetCore.Mvc;

namespace NhanVienWebApp.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
