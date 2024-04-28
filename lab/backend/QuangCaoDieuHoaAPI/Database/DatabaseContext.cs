using QuangCaoDieuHoaAPI.Models;
using QuangCaoDieuHoaAPI.Seeder;
using Microsoft.EntityFrameworkCore;

namespace QuangCaoDieuHoaAPI.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }


        #region User
        public DbSet<User> users { get; set; }
        public DbSet<FormKhachHang> tblQuanLies { get; set; }
        public DbSet<SanPham> tblSanPhams { get; set; } 
        public DbSet<DonHang> tblDonHangs { get; set; }
        public DbSet<Hang> tblHangs { get; set; }
        public DbSet<TrangThai> trangThais { get; set; }
        #endregion

        public static void UpdateDatabase(DatabaseContext context)
        {
            context.Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var sqlConnection = "Server=localhost;Port=3306;Database=QuangCaoDieuHoa;Uid=root;Pwd=2010;MaximumPoolSize=500;";
                optionsBuilder.UseMySql(sqlConnection, MySqlServerVersion.LatestSupportedServerVersion);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region User
            new UserSeeder(modelBuilder).SeedData();
            #endregion

            #region tblSanPham
            modelBuilder.Entity<SanPham>()
                  .HasIndex(e => e.ID)
                  .IsUnique();
            #endregion

            #region tblHang
            modelBuilder.Entity<Hang>()
                .HasIndex(e => e.tenHang)
                .IsUnique();
            #endregion

            #region tblTrangThai
            modelBuilder.Entity<TrangThai>()
                .HasIndex(e => e.trangThai)
                .IsUnique();
            #endregion

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}