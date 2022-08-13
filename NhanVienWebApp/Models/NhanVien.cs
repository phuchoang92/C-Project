using System.ComponentModel.DataAnnotations;

namespace NhanVienAPI.Models
{
    public class NhanVien
    {
        [Required]
        public string NhanVienId { get; set; }

        [Required]
        public string TenNhanVien { get; set; }

        [Required]
        public string TenDangNhap { get; set; }

        public string PhongBan { get; set; }
        public string DonVi { get; set; }

    }
}
