using Microsoft.EntityFrameworkCore;
using QuanLyDiemRenLuyenSV.Models; // Thay QuanLyDiemRenLuyenSV bằng tên project của bạn

namespace QuanLyDiemRenLuyenSV.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSet cho mỗi bảng trong CSDL của bạn
        public DbSet<CauHinhTrangThaiSinhVien> CauHinhTrangThaiSinhViens { get; set; }
        public DbSet<CauHinhTrangThaiDanhGia> CauHinhTrangThaiDanhGias { get; set; }
        public DbSet<CauHinhVaiTro> CauHinhVaiTros { get; set; }
        public DbSet<CauHinhXepLoai> CauHinhXepLoais { get; set; }
        public DbSet<Truong> Truongs { get; set; }
        public DbSet<Khoa> Khoas { get; set; }
        public DbSet<NienKhoa> NienKhoas { get; set; }
        public DbSet<Lop> Lops { get; set; }
        public DbSet<HocKy> HocKys { get; set; }
        public DbSet<SinhVien> SinhViens { get; set; }
        public DbSet<ChucVu> ChucVus { get; set; } // Thêm DbSet cho bảng ChucVu
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<NhomTieuChi> NhomTieuChis { get; set; }
        public DbSet<TieuChi> TieuChis { get; set; }
        public DbSet<PhieuDanhGia> PhieuDanhGias { get; set; }
        public DbSet<ChiTietPhieuDanhGia> ChiTietPhieuDanhGias { get; set; }
        public DbSet<MinhChung> MinhChungs { get; set; }
        public DbSet<KetQuaRenLuyen> KetQuaRenLuyens { get; set; }
        public DbSet<LichSuTrangThaiSinhVien> LichSuTrangThaiSinhViens { get; set; }
        public DbSet<ThongBaoBaoCao> ThongBaoBaoCaos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình các mối quan hệ (nếu cần thiết, EF Core có thể tự suy luận phần lớn)
            // Ví dụ: Thiết lập khóa ngoại và mối quan hệ rõ ràng nếu có bất kỳ sự phức tạp nào
            // Mối quan hệ 1-nhiều giữa Khoa và NhanVien
            modelBuilder.Entity<NhanVien>()
                .HasOne(nv => nv.Khoa)
                .WithMany()
                .HasForeignKey(nv => nv.KhoaID)
                .IsRequired(false); // KhoaID có thể NULL

            // Mối quan hệ 1-nhiều giữa ChucVu và NhanVien
            modelBuilder.Entity<NhanVien>()
                .HasOne(nv => nv.ChucVu)
                .WithMany()
                .HasForeignKey(nv => nv.ChucVuID);

            // Mối quan hệ 1-nhiều giữa SinhVien và NguoiDung
            modelBuilder.Entity<NguoiDung>()
                .HasOne(nd => nd.SinhVien)
                .WithMany()
                .HasForeignKey(nd => nd.SinhVienID)
                .IsRequired(false); // SinhVienID có thể NULL

            // Mối quan hệ 1-nhiều giữa NhanVien và NguoiDung
            modelBuilder.Entity<NguoiDung>()
                .HasOne(nd => nd.NhanVien)
                .WithMany()
                .HasForeignKey(nd => nd.NhanVienID)
                .IsRequired(false); // NhanVienID có thể NULL

            // Cấu hình cho các cột UNIQUE
            modelBuilder.Entity<SinhVien>()
                .HasIndex(s => s.MaSV)
                .IsUnique();

            modelBuilder.Entity<NhanVien>()
                .HasIndex(nv => nv.MaNV)
                .IsUnique();

            modelBuilder.Entity<NguoiDung>()
                .HasIndex(nd => nd.Username)
                .IsUnique();

            modelBuilder.Entity<ChucVu>()
                .HasIndex(cv => cv.TenChucVu)
                .IsUnique();

            // Cấu hình cho cột `NgayCapNhat` trong `KetQuaRenLuyen`
            modelBuilder.Entity<KetQuaRenLuyen>()
                .Property(kqr => kqr.NgayCapNhat)
                .HasDefaultValueSql("GETDATE()");

            // Các cấu hình mặc định khác từ CSDL bạn đã cung cấp
            modelBuilder.Entity<PhieuDanhGia>()
                .Property(pdg => pdg.NgayLapPhieu)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<MinhChung>()
                .Property(mc => mc.NgayUpload)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<LichSuTrangThaiSinhVien>()
                .Property(lstts => lstts.NgayCapNhat)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<ThongBaoBaoCao>()
                .Property(tb => tb.NgayCongBo)
                .HasDefaultValueSql("GETDATE()");

            // Ràng buộc CHECK (sẽ kiểm tra ở tầng ứng dụng hoặc qua Data Annotations)
            // Các ràng buộc này không được ánh xạ trực tiếp trong EF Core theo cách thông thường,
            // bạn nên xử lý chúng thông qua Data Annotations trong Models hoặc trong logic nghiệp vụ.
            // Ví dụ cho Check Constraint
            modelBuilder.Entity<ChiTietPhieuDanhGia>()
                .ToTable(tb => tb.HasCheckConstraint("CK_DiemTuDanhGia_Positive", "DiemTuDanhGia >= 0"))
                .ToTable(tb => tb.HasCheckConstraint("CK_DiemGiaoVienDeXuat_Positive", "DiemGiaoVienDeXuat >= 0"))
                .ToTable(tb => tb.HasCheckConstraint("CK_DiemHoiDongDuyet_Positive", "DiemHoiDongDuyet >= 0"));


            // UNIQUE constraint cho KetQuaRenLuyen
            modelBuilder.Entity<KetQuaRenLuyen>()
                .HasIndex(kqr => new { kqr.SinhVienID, kqr.HocKyID })
                .IsUnique();


            base.OnModelCreating(modelBuilder);
        }
    }
}