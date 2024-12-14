using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Data
{
    public class MusicLibraryDataContext : DbContext
    {
        public MusicLibraryDataContext(DbContextOptions<MusicLibraryDataContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<AlbumPlaylist> AlbumPlaylists { get; set; }
        public DbSet<ReviewReaction> Reactions { get; set; }
        public DbSet<Comment> Comments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();

            modelBuilder.Entity<Album>()
                .Property(a => a.ReleaseDate)
                .IsRequired(false);

            modelBuilder.Entity<Album>()
                .Property(a => a.AverageRating)
                .IsRequired(false);

            modelBuilder.Entity<Album>()
                .Property(a => a.Genre)
                .IsRequired(false);

            modelBuilder.Entity<Album>()
                .Property(a => a.Name)
                .IsRequired();

            modelBuilder.Entity<Album>()
                .HasMany(a => a.Playlists)
                .WithMany(ac => ac.Albums)
                .UsingEntity<AlbumPlaylist>();

            modelBuilder.Entity<Playlist>()
                .Property(p => p.Name)
                .IsRequired();

            modelBuilder.Entity<Comment>()
                .Property(c => c.Content)
                .IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder
                .LogTo(Console.WriteLine)   // Логування до консолі
                .EnableSensitiveDataLogging();  // Дозволяє бачити SQL-запити для відлагодження
        }
    }
}