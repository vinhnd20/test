using System.ComponentModel.DataAnnotations;

namespace QuangCaoDieuHoaAPI.Models
{
    public class FormKhachHang : BaseModel
    {
        public int ID { get; set; }
        public string email { get; set; }
        public int productId { get; set; }
        public string soDienThoai { get; set; }
        public string hoTen { get; set; }
        public string diaChi { get; set; } = "";
        public int Status { get; set; } = 1;
    }
}

