using FamilyWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FamilyWebAPI.Persistence
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            // name of database
            optionsBuilder.UseSqlite(@"Data Source = /home/gimpe/Nextcloud/School/RiderProjects/Assignments/Assignments/Assignment3/FamilyWebAPI/users.db");
        }
    }
}