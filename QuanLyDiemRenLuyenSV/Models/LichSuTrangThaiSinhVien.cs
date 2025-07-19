using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDiemRenLuyenSV.Models
{
    [Table("LichSuTrangThaiSinhVien")]
    public class LichSuTrangThaiSinhVien
    {
        [Key]
        public int LichSuID { get; set; }

        public int? SinhVienID { get; set; }
        [ForeignKey("SinhVienID")]
        public SinhVien? SinhVien { get; set; }

        public int? TrangThaiIDCu { get; set; }
        [ForeignKey("TrangThaiIDCu")]
        public CauHinhTrangThaiSinhVien? TrangThaiCu { get; set; }

        public int? TrangThaiIDMoi { get; set; }
        [ForeignKey("TrangThaiIDMoi")]
        public CauHinhTrangThaiSinhVien? TrangThaiMoi { get; set; }

        public DateTime? NgayCapNhat { get; set; }

        public int? NguoiCapNhatID { get; set; }
        [ForeignKey("NguoiCapNhatID")]
        public NguoiDung? NguoiCapNhat { get; set; }

        [StringLength(200)]
        public string? GhiChu { get; set; }
    }
}