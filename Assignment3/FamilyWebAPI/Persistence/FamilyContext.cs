using Microsoft.EntityFrameworkCore;
using Models;

namespace FamilyWebAPI.Persistence
{
    public class FamilyContext : DbContext
    {
        public DbSet<Family> Families { get; set; }
        public DbSet<Adult> Adults { get; set; }
        public DbSet<Child> Children { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Interest> Interests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            // name of database
            optionsBuilder.UseSqlite(@"Data Source = /home/gimpe/Nextcloud/School/RiderProjects/Assignments/Assignments/Assignment3/FamilyWebAPI/families.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Family>().HasKey(family => new
            {
                family.StreetName,
                family.HouseNumber
            });
        }
    }
}