using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDiemRenLuyenSV.Models
{
    [Table("CauHinhVaiTro")]
    public class CauHinhVaiTro
    {
        [Key]
        public int VaiTroID { get; set; }

        [Required]
        [StringLength(50)]
        public string TenVaiTro { get; set; } = string.Empty;

        [StringLength(200)]
        public string? MoTa { get; set; }
    }
}