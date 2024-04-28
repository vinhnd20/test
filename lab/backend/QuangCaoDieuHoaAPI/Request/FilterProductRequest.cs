namespace QuangCaoDieuHoaAPI.Request
{
    public class FilterProductRequest
    {
        public int hangId { get; set; }
        public string congSuat { get; set; }
        public string kieuDang { get; set; }
        public string soChieu { get; set; }

        public double? GiaDau { get; set; }

        public double? GiaCuoi { get; set; }
    }
}
