using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDiemRenLuyenSV.Models
{
    [Table("Khoa")]
    public class Khoa
    {
        [Key]
        public int KhoaID { get; set; }

        [Required]
        [StringLength(100)]
        public string TenKhoa { get; set; } = string.Empty;

        public int? TruongID { get; set; }
        [ForeignKey("TruongID")]
        public Truong? Truong { get; set; } // Navigation property

        [StringLength(200)]
        public string? DiaChi { get; set; }
    }
}