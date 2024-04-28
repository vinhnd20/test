namespace QuangCaoDieuHoaAPI.Request
{
    public class UpdateForm
    {
        public int ID { get; set; }
        public string email { get; set; }
        public int productId { get; set; }
        public string soDienThoai { get; set; }
        public string hoTen { get; set; }
        public string diaChi { get; set; } = "";
    }
}
