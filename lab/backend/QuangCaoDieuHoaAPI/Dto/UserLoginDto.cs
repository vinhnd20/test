using QuangCaoDieuHoaAPI.Models;

namespace QuangCaoDieuHoaAPI.Dto
{
    public class UserLoginDto
    {
        public string token { get; set; }
        public User user { get; set; }
    }
}
