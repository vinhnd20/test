using System.ComponentModel.DataAnnotations;

namespace QuangCaoDieuHoaAPI.Models
{
    public class DonHang : BaseModel
    {
        public int ID { get; set; }
        public string hoTenKH { get; set; }
        public string soDienThoaiKH { get; set; }
        public int soLuongSanPham { get; set; }
        public string diaChiGiaoHang { get; set; }
        public DateTime ngayDatHang { get; set; }
        public string ghiChuThem { get; set; }
    }
}
