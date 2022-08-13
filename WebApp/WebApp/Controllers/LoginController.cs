using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApp.AuthManager;
using WebApp.DataConnect;

namespace WebApp.Controllers
{
    [Route("Login")]
    public class LoginController : Controller
    {
        private readonly NhanVienDBContext _context;

        public LoginController(NhanVienDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Authentication([FromForm] string username, string password)
        {
            if (username != null && password != null)
            {
                var user = _context.UserNhanViens.FirstOrDefault(u => u.UserName == username && u.Password == password);
                if(user != null)
                {
                    var tokenString = TokenManipulate.GenerateJWToken(user.UserId, user.UserName);
                    HttpContext.Response.Cookies.Append("access_token", tokenString, new CookieOptions { HttpOnly = true, Secure = true });
                    return RedirectToAction("Index", "NhanVien");
                }
            }
            return View("Fail");
        }
    }
}
