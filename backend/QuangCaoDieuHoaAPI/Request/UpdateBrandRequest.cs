namespace QuangCaoDieuHoaAPI.Request
{
    public class UpdateBrandRequest
    {
        public int Id { get; set; }
        public string ten { get; set; }
        public string moTa { get; set; }
        public IFormFile? image { get; set; }
    }
}
