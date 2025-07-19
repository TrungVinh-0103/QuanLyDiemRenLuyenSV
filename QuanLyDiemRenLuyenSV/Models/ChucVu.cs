using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDiemRenLuyenSV.Models
{
    [Table("ChucVu")]
    public class ChucVu
    {
        [Key]
        public int ChucVuID { get; set; }

        [Required]
        [StringLength(50)]
        public string TenChucVu { get; set; } = string.Empty;

        [StringLength(200)]
        public string? MoTa { get; set; }
    }
}