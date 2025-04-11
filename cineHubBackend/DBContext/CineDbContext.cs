using cineHubBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace cineHubBackend.DBContext
{
    public class CineDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public DbSet<Auditorium> Auditoriums { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        
        public CineDbContext(DbContextOptions<CineDbContext> options)
            : base(options) 
        { 
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("identity");

            builder.Entity<Cinema>().ToTable("Cinemas", "app");
            builder.Entity<Ticket>().ToTable("Tickets", "app");
            builder.Entity<Session>().ToTable("Sessions", "app");
    
            builder.Entity<Auditorium>().ToTable("Auditoriums", "app");
            builder.Entity<User>().ToTable("Users", "app");
        }
    }
}