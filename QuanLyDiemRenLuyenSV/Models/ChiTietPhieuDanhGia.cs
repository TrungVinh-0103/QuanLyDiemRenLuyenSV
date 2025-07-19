using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDiemRenLuyenSV.Models
{
    [Table("ChiTietPhieuDanhGia")]
    public class ChiTietPhieuDanhGia
    {
        [Key]
        public int ChiTietPhieuDanhGiaID { get; set; }

        public int? PhieuDanhGiaID { get; set; }
        [ForeignKey("PhieuDanhGiaID")]
        public PhieuDanhGia? PhieuDanhGia { get; set; }

        public int? TieuChiID { get; set; }
        [ForeignKey("TieuChiID")]
        public TieuChi? TieuChi { get; set; }

        [Required]
        public int DiemTuDanhGia { get; set; }

        public int? DiemGiaoVienDeXuat { get; set; }

        public int? DiemHoiDongDuyet { get; set; }

        [StringLength(500)]
        public string? GhiChuChiTiet { get; set; }
    }
}