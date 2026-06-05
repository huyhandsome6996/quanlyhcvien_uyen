using Microsoft.EntityFrameworkCore;
using QuanLyTrungTam.Entities;

namespace QuanLyTrungTam.DAL
{
    /// <summary>
    /// Database Context - Quản lý kết nối và ánh xạ các bảng SQLite
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TaiKhoan> TaiKhoans { get; set; }
        public DbSet<HocVien> HocViens { get; set; }
        public DbSet<KhoaHoc> KhoaHocs { get; set; }
        public DbSet<LopHoc> LopHocs { get; set; }
        public DbSet<GiangVien> GiangViens { get; set; }
        public DbSet<PhanCongGiangVien> PhanCongGiangViens { get; set; }
        public DbSet<DangKyHoc> DangKyHocs { get; set; }
        public DbSet<ThanhToan> ThanhToans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình mối quan hệ (Bắt buộc giữ lại để DB hiểu Khóa Ngoại)

            // Cấu hình mối quan hệ
            modelBuilder.Entity<LopHoc>()
                .HasOne(l => l.KhoaHoc)
                .WithMany(k => k.LopHocs)
                .HasForeignKey(l => l.MaKhoaHoc);

            modelBuilder.Entity<DangKyHoc>()
                .HasOne(d => d.HocVien)
                .WithMany(h => h.DangKyHocs)
                .HasForeignKey(d => d.MaHocVien);

            modelBuilder.Entity<DangKyHoc>()
                .HasOne(d => d.KhoaHoc)
                .WithMany(k => k.DangKyHocs)
                .HasForeignKey(d => d.MaKhoaHoc);

            modelBuilder.Entity<PhanCongGiangVien>()
                .HasOne(p => p.GiangVien)
                .WithMany(g => g.PhanCongGiangViens)
                .HasForeignKey(p => p.MaGiangVien);

            modelBuilder.Entity<PhanCongGiangVien>()
                .HasOne(p => p.LopHoc)
                .WithMany(l => l.PhanCongGiangViens)
                .HasForeignKey(p => p.MaLopHoc);

            modelBuilder.Entity<ThanhToan>()
                .HasOne(t => t.DangKyHoc)
                .WithMany(d => d.ThanhToans)
                .HasForeignKey(t => t.MaDangKy);
        }
    }
}


