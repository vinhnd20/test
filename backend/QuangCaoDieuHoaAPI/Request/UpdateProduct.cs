namespace QuangCaoDieuHoaAPI.Request
{
    public class UpdateProduct
    {
        public int id { get; set; }
        public string ten { get; set; }
        public float giaTien { get; set; }
        public int soLuong { get; set; }
        public string moTa { get; set; }
        public string congSuat { get; set; }
        public string kieuDang { get; set; }
        public string model { get; set; }
        public string soChieu { get; set; }
        public IFormFile? Image { get; set; }
        public int HangID { get; set; }
        public int TrangThai { get; set; } // 0: Điều hòa cũ  || 1: Điều hòa mới
    }
}
