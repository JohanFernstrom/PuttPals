using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PuttPals.Data.Models;

namespace PuttPals.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Disc> Discs { get; set; }
        public DbSet<PlayerDisc> PlayerDiscs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure the relationship between Player and ApplicationUser
            builder.Entity<Player>()
                .HasOne(p => p.IdentityUser)
                .WithMany()
                .HasForeignKey(p => p.IdentityUserId);

            // Configure the one-to-one relationship between Player and PlayerStats
            builder.Entity<Player>()
                .HasOne(p => p.Stats)
                .WithOne(s => s.Player)
                .HasForeignKey<PlayerStats>(s => s.PlayerId);

            // Configure the one-to-many relationship between Player and Bags
            builder.Entity<Player>()
                .HasMany(p => p.Bags)
                .WithOne(b => b.Player)
                .HasForeignKey(b => b.PlayerId);

            // Configure the many-to-many relationship between Player and Disc
            builder.Entity<PlayerDisc>()
                .HasKey(pd => new { pd.PlayerId, pd.DiscId });

            builder.Entity<PlayerDisc>()
                .HasOne(pd => pd.Player)
                .WithMany(p => p.PlayerDiscs)
                .HasForeignKey(pd => pd.PlayerId);

            builder.Entity<PlayerDisc>()
                .HasOne(pd => pd.Disc)
                .WithMany(d => d.PlayerDiscs)
                .HasForeignKey(pd => pd.DiscId);
        }
    }
}
