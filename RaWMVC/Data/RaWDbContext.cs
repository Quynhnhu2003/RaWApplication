using Microsoft.EntityFrameworkCore;
using RaW.MVC.Data.Configurations;
using RaWMVC.Data.Configurations;
using RaWMVC.Data.Entities;

namespace RaW.MVC.Data
{
    public class RaWDbContext : DbContext
    {
        public RaWDbContext(DbContextOptions<RaWDbContext> options)
        : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=LAPTOP-M2M74TDG\\SQLEXPRESS;Database=RaWDbContext;User Id=itsjuneka; password=Quynhnhu@14; TrustServerCertificate=True; Trusted_Connection=False; MultipleActiveResultSets=true;";
            optionsBuilder.UseSqlServer(connectionString);

        }

        protected override void OnModelCreating(ModelBuilder Builder)
        {
            Builder
                .ApplyConfiguration(new TagConfiguration());
            Builder
                .ApplyConfiguration(new StatusConfiguration());
            Builder
                .ApplyConfiguration(new GenreConfiguration());
            Builder
                .ApplyConfiguration(new StoryConfiguration());
        }

        public DbSet<Tag> Tags { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Story> Stories { get; set; }
    }
}
