using System.ComponentModel.DataAnnotations;

namespace NhanVienAPI.Models
{
    public class UserNhanVien
    {
        [Required]
        [Key]
        public string UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string NhanVienId { get; set; }
        public virtual NhanVien nhanvien { get; set; }

    }
}
