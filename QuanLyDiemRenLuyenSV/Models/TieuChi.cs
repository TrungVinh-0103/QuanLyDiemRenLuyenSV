using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDiemRenLuyenSV.Models
{
    [Table("TieuChi")]
    public class TieuChi
    {
        [Key]
        public int TieuChiID { get; set; }

        public int? NhomTieuChiID { get; set; }
        [ForeignKey("NhomTieuChiID")]
        public NhomTieuChi? NhomTieuChi { get; set; }

        [Required]
        [StringLength(600)]
        public string TenTieuChi { get; set; } = string.Empty;

        [Required]
        public int DiemToiDa { get; set; }

        [Required]
        public bool YeuCauMinhChung { get; set; } = false; // Default 0 in SQL

        [StringLength(500)]
        public string? GhiChu { get; set; }
    }
}