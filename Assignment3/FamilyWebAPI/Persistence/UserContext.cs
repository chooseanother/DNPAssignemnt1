using Microsoft.EntityFrameworkCore;
using Models;

namespace FamilyWebAPI.Persistence
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            // name of database
            optionsBuilder.UseSqlite(@"Data Source = users.db");
        }
    }
}