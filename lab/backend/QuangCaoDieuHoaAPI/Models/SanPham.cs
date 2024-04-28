using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel.DataAnnotations;

namespace QuangCaoDieuHoaAPI.Models
{
    public class SanPham : BaseModel
    {
        public int ID { get; set; }
        public string ten { get; set; }
        public float giaTien { get; set; }
        public int soLuong { get; set; }
        public string moTa { get; set; }
        public string congSuat { get; set; }
        public string kieuDang { get; set; }
        public string model { get; set; }
        public string soChieu { get; set; }
        public string Image { get; set; }
        public int UserID { get; set; }
        public int HangID { get; set; }
        
        public int TrangThai { get; set; } // 0 : Dieu hoa cu || 1:  Dieu hoa moi

        public int Status { get; set; } // 0 : An khoi danh sach || 1 : Hien thi
    }    
}

