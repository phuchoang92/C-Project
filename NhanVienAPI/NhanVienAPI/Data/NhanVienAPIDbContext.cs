using Microsoft.EntityFrameworkCore;
using NhanVienAPI.Models;

namespace NhanVienAPI.Data
{
    public class NhanVienAPIDbContext: DbContext
    {
        public NhanVienAPIDbContext(DbContextOptions<NhanVienAPIDbContext> options) : base(options) { }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<UserNhanVien> UserNhanViens { get; set; }

    }
}
