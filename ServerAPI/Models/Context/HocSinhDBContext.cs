using Microsoft.EntityFrameworkCore;
using ServerAPI.Models.HocSinh;

namespace ServerAPI.Models.Context
{
    public class HocSinhDBContext : DbContext
    {
        public HocSinhDBContext(DbContextOptions<HocSinhDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<HocSinh.HocSinh>()
                .HasKey(s => s.MaHS);
            modelBuilder.Entity<Lop>()
                .HasKey(c => c.MaLop);
            modelBuilder.Entity<MonHoc>()
                .HasKey(s => s.MaMH);
        }

        public DbSet<HocSinh.HocSinh> HocSinhs { get; set; }
        public DbSet<Lop> Lops { get; set; }
        public DbSet<MonHoc> MonHocs { get; set; }
    }
}
