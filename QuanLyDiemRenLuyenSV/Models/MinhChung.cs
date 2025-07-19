using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDiemRenLuyenSV.Models
{
    [Table("MinhChung")]
    public class MinhChung
    {
        [Key]
        public int MinhChungID { get; set; }

        public int? ChiTietPhieuDanhGiaID { get; set; }
        [ForeignKey("ChiTietPhieuDanhGiaID")]
        public ChiTietPhieuDanhGia? ChiTietPhieuDanhGia { get; set; }

        [Required]
        [StringLength(255)]
        public string DuongDan { get; set; } = string.Empty;

        [StringLength(500)]
        public string? MoTa { get; set; }

        public DateTime? NgayUpload { get; set; }
    }
}