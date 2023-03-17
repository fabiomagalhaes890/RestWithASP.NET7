using Microsoft.EntityFrameworkCore;
using RestWithASPNET.Models.Mappings;

namespace RestWithASPNET.Models
{
    public class SQLContext : DbContext
    {
        public SQLContext() {}

        public SQLContext(DbContextOptions<SQLContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("default");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PeopleMapping());
            modelBuilder.ApplyConfiguration(new UserMapping());

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Email = "testuser@gmail.com",
                    FullName = "Test User",
                    Password = "24-0B-E5-18-FA-BD-27-24-DD-B6-F0-4E-EB-1D-A5-96-74-48-D7-E8-31-C0-8C-8F-A8-22-80-9F-74-C7-20-A9",
                    RefreshToken = string.Empty,
                    RefreshTokenExpiryTime = null,
                    UserName = "testuser",
                    Id = Guid.NewGuid()
                }); ;
        }
    }
}
