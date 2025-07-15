using Microsoft.EntityFrameworkCore;
using QuaveChallenge.API.Data.Configurations;
using QuaveChallenge.API.Models;

namespace QuaveChallenge.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Community> Communities { get; set; }
        public DbSet<Person> People { get; set; }

        public DbSet<CheckinInformation> CheckinInformation { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CommunityConfiguration());
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new CheckinInformationConfiguration());
        }
    }
} 