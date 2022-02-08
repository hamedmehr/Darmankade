using Darmankade.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Authentication
{
    public class AuthDBContext : DbContext
    {

        public AuthDBContext(DbContextOptions<AuthDBContext> options) : base(options)
        {

        }
        public virtual DbSet<Auth> Auth { get; set; }
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