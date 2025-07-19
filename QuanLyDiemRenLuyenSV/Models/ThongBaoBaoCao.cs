using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDiemRenLuyenSV.Models
{
    [Table("ThongBaoBaoCao")]
    public class ThongBaoBaoCao
    {
        [Key]
        public int ThongBaoID { get; set; }

        [Required]
        [StringLength(50)]
        public string LoaiThongBao { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string TieuDe { get; set; } = string.Empty;

        [Required]
        public string NoiDung { get; set; } = string.Empty; // NVARCHAR(MAX)

        public DateTime? NgayCongBo { get; set; }

        public int? NguoiCongBoID { get; set; }
        [ForeignKey("NguoiCongBoID")]
        public NguoiDung? NguoiCongBo { get; set; }

        public int? HocKyID { get; set; }
        [ForeignKey("HocKyID")]
        public HocKy? HocKy { get; set; }

        public int? LopID { get; set; }
        [ForeignKey("LopID")]
        public Lop? Lop { get; set; }
    }
}