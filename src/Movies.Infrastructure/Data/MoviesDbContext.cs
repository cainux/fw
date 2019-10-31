using Microsoft.EntityFrameworkCore;
using Movies.Core.Entities;

namespace Movies.Infrastructure.Data
{
    public class MoviesDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MovieRating> MovieRatings { get; set; }

        public MoviesDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<MovieRating>()
                .HasKey(l => new { l.MovieId, l.UserId });

            modelBuilder.Entity<MovieRating>()
                .HasOne(l => l.Movie)
                .WithMany(a => a.Ratings)
                .HasForeignKey(l => l.MovieId);

            modelBuilder.Entity<MovieRating>()
                .HasOne(l => l.User)
                .WithMany(u => u.Ratings)
                .HasForeignKey(l => l.UserId);
        }
    }
}