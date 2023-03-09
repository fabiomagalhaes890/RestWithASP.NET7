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
        }

        public DbSet<People> People { get; set; }
    }
}
