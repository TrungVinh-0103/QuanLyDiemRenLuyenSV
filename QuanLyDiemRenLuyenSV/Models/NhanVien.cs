using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDiemRenLuyenSV.Models
{
    public class NhanVien
    {
        [Key]
        public int NhanVienID { get; set; }

        [Required]
        [StringLength(20)]
        public string MaNV { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string HoTen { get; set; } = string.Empty;

        public int? KhoaID { get; set; }
        [ForeignKey("KhoaID")]
        public Khoa? Khoa { get; set; }

        public int ChucVuID { get; set; } // Chắc chắn có chức vụ
        [ForeignKey("ChucVuID")]
        public ChucVu? ChucVu { get; set; }// Navigation property
    }
}