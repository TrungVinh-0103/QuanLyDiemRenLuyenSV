using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDiemRenLuyenSV.Models
{
    [Table("CauHinhTrangThaiDanhGia")]
    public class CauHinhTrangThaiDanhGia
    {
        [Key]
        public int TrangThaiDanhGiaID { get; set; }

        [Required]
        [StringLength(50)]
        public string TenTrangThai { get; set; } = string.Empty;

        [StringLength(200)]
        public string? MoTa { get; set; }
    }
}