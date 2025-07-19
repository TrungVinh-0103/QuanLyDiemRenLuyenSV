using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDiemRenLuyenSV.Models
{
    [Table("PhieuDanhGia")]
    public class PhieuDanhGia
    {
        [Key]
        public int PhieuDanhGiaID { get; set; }

        public int? SinhVienID { get; set; }
        [ForeignKey("SinhVienID")]
        public SinhVien? SinhVien { get; set; }

        public int? HocKyID { get; set; }
        [ForeignKey("HocKyID")]
        public HocKy? HocKy { get; set; }

        [DataType(DataType.Date)]
        public DateTime? NgayLapPhieu { get; set; }

        public int? TrangThaiDanhGiaID { get; set; }
        [ForeignKey("TrangThaiDanhGiaID")]
        public CauHinhTrangThaiDanhGia? TrangThaiDanhGia { get; set; }

        public int? TongDiemTuDanhGia { get; set; }

        public int? TongDiemGiaoVienDeXuat { get; set; }

        public int? TongDiemHoiDongDuyet { get; set; }

        [StringLength(500)]
        public string? GhiChuCuaSV { get; set; }

        [StringLength(500)]
        public string? NhanXetGiaoVien { get; set; }

        [DataType(DataType.Date)]
        public DateTime? NgayGVCN_Duyet { get; set; }

        public int? NhanVienGVCN_DuyetID { get; set; }
        [ForeignKey("NhanVienGVCN_DuyetID")]
        public NhanVien? NhanVienGVCN_Duyet { get; set; }

        [StringLength(500)]
        public string? NhanXetHoiDong { get; set; }

        [DataType(DataType.Date)]
        public DateTime? NgayHoiDong_Duyet { get; set; }

        public int? NhanVienHoiDong_DuyetID { get; set; }
        [ForeignKey("NhanVienHoiDong_DuyetID")]
        public NhanVien? NhanVienHoiDong_Duyet { get; set; }
    }
}