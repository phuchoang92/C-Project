using System.ComponentModel.DataAnnotations;

namespace NhanVienAPI.Models
{
    public class LoginUser
    {
        public string UserId { get; set; } = "unknown";
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
