using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDiemRenLuyenSV.Models
{
    [Table("NguoiDung")]
    public class NguoiDung
    {
        [Key]
        public int NguoiDungID { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        public int? VaiTroID { get; set; }
        [ForeignKey("VaiTroID")]
        public CauHinhVaiTro? VaiTro { get; set; }

        public int? SinhVienID { get; set; }
        [ForeignKey("SinhVienID")]
        public SinhVien? SinhVien { get; set; }

        public int? NhanVienID { get; set; }
        [ForeignKey("NhanVienID")]
        public NhanVien? NhanVien { get; set; }

        public DateTime? LastLogin { get; set; }
    }
}