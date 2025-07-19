using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDiemRenLuyenSV.Models
{
    [Table("SinhVien")]
    public class SinhVien
    {
        [Key]
        public int SinhVienID { get; set; }

        [Required]
        [StringLength(20)]
        public string MaSV { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string HoTen { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime? NgaySinh { get; set; }

        [Required]
        [StringLength(100)]
        public string NoiSinh { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        public string GioiTinh { get; set; } = string.Empty;

        public int? KhoaID { get; set; }
        [ForeignKey("KhoaID")]
        public Khoa? Khoa { get; set; }

        public int? LopID { get; set; }
        [ForeignKey("LopID")]
        public Lop? Lop { get; set; }

        public int? TrangThaiID { get; set; }
        [ForeignKey("TrangThaiID")]
        public CauHinhTrangThaiSinhVien? TrangThai { get; set; }

        public DateTime? NgayCapNhatTrangThai { get; set; }
    }
}