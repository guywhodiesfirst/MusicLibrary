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
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<AlbumPlaylist> AlbumPlaylists { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Reviews)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<User>()
                .HasMany(u => u.Playlists)
                .WithOne(ac => ac.User)
                .HasForeignKey(ac => ac.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Album>()
                .HasMany(a => a.Reviews)
                .WithOne(r => r.Album)
                .HasForeignKey(r => r.AlbumId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Album>()
                .HasMany(a => a.Playlists)
                .WithMany(ac => ac.Albums)
                .UsingEntity<AlbumPlaylist>();

            modelBuilder.Entity<Genre>()
                .HasMany(g => g.Albums)
                .WithOne(a => a.Genre)
                .HasForeignKey(a => a.GenreId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}