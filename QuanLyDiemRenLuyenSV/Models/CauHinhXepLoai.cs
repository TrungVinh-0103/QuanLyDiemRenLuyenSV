using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDiemRenLuyenSV.Models
{
    [Table("CauHinhXepLoai")]
    public class CauHinhXepLoai
    {
        [Key]
        public int XepLoaiID { get; set; }

        [Required]
        [StringLength(50)]
        public string TenXepLoai { get; set; } = string.Empty;

        [Required]
        public int DiemToiThieu { get; set; }

        [Required]
        public int DiemToiDa { get; set; }

        [StringLength(200)]
        public string? MoTa { get; set; }
    }
}