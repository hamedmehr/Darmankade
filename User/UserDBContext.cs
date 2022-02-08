using Microsoft.EntityFrameworkCore;

namespace User
{
    public class UserDBContext : DbContext
    {

        public UserDBContext(DbContextOptions<UserDBContext> options) : base(options)
        {

        }
        public virtual DbSet<Darmankade.Model.Models.User> User { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // something
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder for all of the tables            
        }
    }
}