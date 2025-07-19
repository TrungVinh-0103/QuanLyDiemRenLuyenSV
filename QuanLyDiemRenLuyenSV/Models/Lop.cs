using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDiemRenLuyenSV.Models
{
    [Table("Lop")]
    public class Lop
    {
        [Key]
        public int LopID { get; set; }

        [Required]
        [StringLength(50)]
        public string TenLop { get; set; } = string.Empty;

        public int? KhoaID { get; set; }
        [ForeignKey("KhoaID")]
        public Khoa? Khoa { get; set; } // Navigation property

        public int? NienKhoaID { get; set; }
        [ForeignKey("NienKhoaID")]
        public NienKhoa? NienKhoa { get; set; } // Navigation property
    }
}