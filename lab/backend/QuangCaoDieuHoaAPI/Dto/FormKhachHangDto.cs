using Microsoft.EntityFrameworkCore.Query.Internal;

namespace QuangCaoDieuHoaAPI.Dto
{
    public class FormKhachHangDto
    {
        public int ID { get; set; }
        public string email { get; set; }
        public int productId { get; set; }
        public string soDienThoai { get; set; }
        public string hoTen { get; set; }
        public string diaChi { get; set; } = "";
        public int Status { get; set; }

        public string productName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
