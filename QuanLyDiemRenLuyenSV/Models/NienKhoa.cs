using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDiemRenLuyenSV.Models
{
    [Table("NienKhoa")]
    public class NienKhoa
    {
        [Key]
        public int NienKhoaID { get; set; }

        [Required]
        [StringLength(50)]
        public string TenNienKhoa { get; set; } = string.Empty;

        [Required]
        public int NamBatDau { get; set; }

        [Required]
        public int NamKetThuc { get; set; }
    }
}
