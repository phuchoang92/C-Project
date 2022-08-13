using System.ComponentModel.DataAnnotations;

namespace NhanVienAPI.Models
{
    public class LoginUser
    {
        [Required]
        [Key]
        public string UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
