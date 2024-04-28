namespace QuangCaoDieuHoaAPI.Request
{
    public class BrandRequest
    {
        public string tenHang { get; set; }
        public string moTa { get; set; }
        public IFormFile? image { get; set; }
    }
}
