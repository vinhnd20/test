namespace QuangCaoDieuHoaAPI.Request
{
    public class QuanLyFormRequest
    {
        public string email { get; set; }
        public int productId { get; set; }
        public string soDienThoai { get; set; }
        public string hoTen { get; set; }
        public string diaChi { get; set; } = "";
    }
}
