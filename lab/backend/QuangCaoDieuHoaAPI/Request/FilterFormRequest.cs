namespace QuangCaoDieuHoaAPI.Request
{
    public class FilterFormRequest
    {
        public string hoTen { get; set; }
        public string soDienThoai { get; set; }

        public DateTime? TuNgay { get; set; }

        public DateTime? DenNgay { get; set; }
    }
}
