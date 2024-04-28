    namespace QuangCaoDieuHoaAPI.Models
{
    public class User : BaseModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; } = "";
        public int Position { get; set; } = 0;
        public string Avatar { get; set; } = "";
        public int Status { get; set; } = 0;
        public string OTP { get; set; } = "";

        public string RoleName { get; set; } = "";// ten quyen han
    }
}
