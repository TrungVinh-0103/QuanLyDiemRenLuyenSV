using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDiemRenLuyenSV.Models
{
    [Table("KetQuaRenLuyen")]
    public class KetQuaRenLuyen
    {
        [Key]
        public int KetQuaID { get; set; }

        public int? SinhVienID { get; set; }
        [ForeignKey("SinhVienID")]
        public SinhVien? SinhVien { get; set; }

        public int? HocKyID { get; set; }
        [ForeignKey("HocKyID")]
        public HocKy? HocKy { get; set; }

        public int? PhieuDanhGiaID { get; set; }
        [ForeignKey("PhieuDanhGiaID")]
        public PhieuDanhGia? PhieuDanhGia { get; set; }

        [Required]
        public int TongDiemRenLuyen { get; set; }

        public int? XepLoaiID { get; set; }
        [ForeignKey("XepLoaiID")]
        public CauHinhXepLoai? XepLoai { get; set; }

        [DataType(DataType.Date)]
        public DateTime? NgayCapNhat { get; set; }
    }
}