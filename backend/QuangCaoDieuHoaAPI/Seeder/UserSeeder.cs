using Microsoft.EntityFrameworkCore;
using QuangCaoDieuHoaAPI.Models;
using QuangCaoDieuHoaAPI.Utility;

namespace QuangCaoDieuHoaAPI.Seeder
{
    class UserSeeder
    {
        private readonly ModelBuilder _modelBuilder;
        public UserSeeder(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        /// <summary>
        /// Excute data
        /// </summary>
        public void SeedData()
        {
            _modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    UserName = "Test",
                    Password = UtilityFunction.CreateMD5("Test"),
                    Email = "Test@gmail.com",
                    FirstName = "Test",
                    LastName = "Test",
                    Birthday = new DateTime(2001, 05, 14),
                    Avatar = "",
                    Address = "",
                    Status = 1,
                    RoleName = "Admin"
                }
                );
        }
    }
}
