using Microsoft.EntityFrameworkCore;
using NhanVienAPI.Models;

namespace WebApp.DataConnect
{

    public class NhanVienDBContext: DbContext
    {
        public NhanVienDBContext(DbContextOptions<NhanVienDBContext> options) : base(options) { }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<UserNhanVien> UserNhanViens { get; set; }
    }
}
