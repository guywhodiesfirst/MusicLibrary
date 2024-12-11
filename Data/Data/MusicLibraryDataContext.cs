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
                .HasMany(u => u.Reviews)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<User>()
                .HasMany(u => u.Playlists)
                .WithOne(ac => ac.User)
                .HasForeignKey(ac => ac.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Reactions)
                .WithOne(rr => rr.User)
                .HasForeignKey(rr => rr.UserId)
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

            //modelBuilder.Entity<Genre>()
            //    .HasMany(g => g.Albums)
            //    .WithOne(a => a.Genre)
            //    .HasForeignKey(a => a.GenreId)
            //    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
                .HasMany(r => r.Comments)
                .WithOne(c => c.Review)
                .HasForeignKey(c => c.ReviewId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
                .HasMany(r => r.Reactions)
                .WithOne(rr => rr.Review)
                .HasForeignKey(rr => rr.ReviewId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}