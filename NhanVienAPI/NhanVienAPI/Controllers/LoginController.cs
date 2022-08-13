using Microsoft.AspNetCore.Mvc;
using System.Text;
using NhanVienAPI.Data;
using NhanVienAPI.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;

namespace NhanVienAPI.Controllers
{

    [ApiController]
    [Route("api/token")]
    public class JWTokenController : Controller
    {
        public IConfiguration _configuration;
        private readonly NhanVienAPIDbContext _context;

        public JWTokenController(IConfiguration config, NhanVienAPIDbContext context)
        {
            _configuration = config;
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> GenerateToken(LoginUser userData)
        {
            if (userData != null && userData.UserName != null && userData.Password != null)
            {
                var user = await _context.UserNhanViens.FirstOrDefaultAsync(u => u.UserName == userData.UserName && u.Password == userData.Password);

                if (user != null)
                {

                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserID",user.UserId),
                        new Claim("UserName", user.UserName),
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["JWT:Issuer"],
                        _configuration["JWT:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn
                    );

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid Credentials");
                }
            }
            else
            {
                return BadRequest();
            }

        }

    }
}
