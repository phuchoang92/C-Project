using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApp.AuthManager
{
    public class TokenManipulate
    {
        private static string SecretKey = "ThisisAVerySecretKeyThatWillBeUsedToCreateTheTokenForAuthentication";

        public static string GenerateJWToken(string UserID, string UserName)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "JWTServiceAccessToken"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("UserID",UserID),
                new Claim("UserName", UserName),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                "JWTAuthenticationServer",
                "JWTServicePostmanClient",
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string ValidateJWToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            var userName = jwt.Claims.First(claim => claim.Type == "UserName").Value;

            return userName;

        }
    }
}
