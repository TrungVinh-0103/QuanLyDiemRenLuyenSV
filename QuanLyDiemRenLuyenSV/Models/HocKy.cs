using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDiemRenLuyenSV.Models
{
    [Table("HocKy")]
    public class HocKy
    {
        [Key]
        public int HocKyID { get; set; }

        [Required]
        [StringLength(50)]
        public string TenHocKy { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string NamHoc { get; set; } = string.Empty;

        public int? NienKhoaID { get; set; }
        [ForeignKey("NienKhoaID")]
        public NienKhoa? NienKhoa { get; set; } // Navigation property
    }
}