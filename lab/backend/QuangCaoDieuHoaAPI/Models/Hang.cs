using System.ComponentModel.DataAnnotations;

namespace QuangCaoDieuHoaAPI.Models
{
    public class Hang : BaseModel
    {
        public int Id { get; set; }
        public string tenHang { get; set; }
        public string moTa { get; set; }
        public string image { get; set; }
        public int status { get; set; } // 0: ẩn, 1 : hiện
    }
}
