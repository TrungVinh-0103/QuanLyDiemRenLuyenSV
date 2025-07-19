using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDiemRenLuyenSV.Models
{
    [Table("Truong")]
    public class Truong
    {
        [Key]
        public int TruongID { get; set; }

        [Required]
        [StringLength(100)]
        public string TenTruong { get; set; } = string.Empty;

        [StringLength(200)]
        public string? DiaChi { get; set; }

        [StringLength(255)]
        public string? LogoUrl { get; set; }
    }
}