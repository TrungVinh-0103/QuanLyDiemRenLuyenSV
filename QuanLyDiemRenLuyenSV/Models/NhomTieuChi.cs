using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDiemRenLuyenSV.Models
{
    [Table("NhomTieuChi")]
    public class NhomTieuChi
    {
        [Key]
        public int NhomTieuChiID { get; set; }

        [Required]
        [StringLength(200)]
        public string TenNhom { get; set; } = string.Empty;

        [Required]
        public int DiemToiDa { get; set; }
    }
}
