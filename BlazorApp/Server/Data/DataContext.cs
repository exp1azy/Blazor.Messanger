using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Server.Data
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _config;

        public DataContext(IConfiguration config) 
        { 
            _config = config;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_config["ConnectionString"]);
        }
    }
}
