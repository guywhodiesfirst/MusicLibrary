using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLibrary.Tests
{
    internal class ExpectedEntities
    {
        public static IEnumerable<Album> Albums =>
        [
            new Album
            {
                Id = Guid.Parse("7b0d93e6-9e3d-4e58-9c7b-bc52e0a730af"),
                Name = "Dark Side of the Moon",
                Artists = new List<string> {"Pink Floyd"},
                ReleaseDate = new DateTime(1973, 3, 1),
                Genre = "art rock"
            },
            new Album
            {
                Id = Guid.Parse("285f6c88-dbf2-4dc0-8b82-30a06e125b8c"),
                Name = "Thriller",
                Artists = new List<string> {"Michael Jackson"},
                ReleaseDate = new DateTime(1982, 11, 30),
                Genre = "pop"
            }
        ];

        public static IEnumerable<User> Users =>
        [
            new User
            {
                Id = Guid.Parse("98c9c918-77f2-4b8b-8df1-55f3eecf74e3"),
                Username = "Admin",
                Password = "adminpass",
                Email = "admin@example.com",
                IsAdmin = true,
                IsBlocked = false
            },
            new User
            {
                Id = Guid.Parse("bdbf650b-5551-4aeb-b978-88f7d15c7bcf"),
                Username = "JohnDoe",
                Password = "userpass",
                Email = "johndoe@example.com",
                IsAdmin = false,
                IsBlocked = false
            }
        ];

        public static IEnumerable<Playlist> Playlists =>
        [
            new Playlist
            {
                Id = Guid.Parse("439d8404-5bcb-4bd4-a91f-71f15a15a2d9"),
                Name = "My Favorites",
                Description = "Best of the best",
                UserId = Guid.Parse("bdbf650b-5551-4aeb-b978-88f7d15c7bcf")
            },
            new Playlist
            {
                Id = Guid.Parse("bf09353e-89dc-4147-8a87-dce98783d6f8"),
                Name = "Relaxing Tunes",
                Description = "Chill and unwind",
                UserId = Guid.Parse("bdbf650b-5551-4aeb-b978-88f7d15c7bcf")
            }
        ];

        public static IEnumerable<Review> Reviews =>
        [
            new Review
            {
                Id = Guid.Parse("28f31c22-76d2-4b99-a3d7-420de3b0f527"),
                UserId = Guid.Parse("bdbf650b-5551-4aeb-b978-88f7d15c7bcf"),
                AlbumId = Guid.Parse("7b0d93e6-9e3d-4e58-9c7b-bc52e0a730af"),
                Rating = 5,
                Content = "Amazing album!",
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow
            },
            new Review
            {
                Id = Guid.Parse("ae89cce6-737c-4cb7-8c30-95ef1c3f4b4b"),
                UserId = Guid.Parse("bdbf650b-5551-4aeb-b978-88f7d15c7bcf"),
                AlbumId = Guid.Parse("285f6c88-dbf2-4dc0-8b82-30a06e125b8c"),
                Rating = 4,
                Content = "Great album but a bit overrated.",
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow
            }
        ];

        public static IEnumerable<AlbumPlaylist> AlbumPlaylists =>
        [
            new AlbumPlaylist
            {
                AlbumId = Guid.Parse("7b0d93e6-9e3d-4e58-9c7b-bc52e0a730af"),
                PlaylistId = Guid.Parse("439d8404-5bcb-4bd4-a91f-71f15a15a2d9")
            },
            new AlbumPlaylist
            {
                AlbumId = Guid.Parse("285f6c88-dbf2-4dc0-8b82-30a06e125b8c"),
                PlaylistId = Guid.Parse("bf09353e-89dc-4147-8a87-dce98783d6f8")
            }
        ];

        public static IEnumerable<ReviewReaction> ReviewReactions =>
        [
            new ReviewReaction
            {
                Id = Guid.Parse("ac869b99-3435-4e57-a34b-2d20ddfda4b1"),
                ReviewId = Guid.Parse("28f31c22-76d2-4b99-a3d7-420de3b0f527"),
                UserId = Guid.Parse("98c9c918-77f2-4b8b-8df1-55f3eecf74e3"),
                IsLike = true
            },
            new ReviewReaction
            {
                Id = Guid.Parse("f04e12e7-fe27-4121-899c-39cf7c89210e"),
                ReviewId = Guid.Parse("28f31c22-76d2-4b99-a3d7-420de3b0f527"),
                UserId = Guid.Parse("bdbf650b-5551-4aeb-b978-88f7d15c7bcf"),
                IsLike = false
            }
        ];

        public static IEnumerable<Comment> Comments =>
        [
            new Comment
            {
                Id = Guid.Parse("9a81e7a9-2bf8-4ef9-b2c2-81bbcd393908"),
                ReviewId = Guid.Parse("28f31c22-76d2-4b99-a3d7-420de3b0f527"),
                UserId = Guid.Parse("98c9c918-77f2-4b8b-8df1-55f3eecf74e3"),
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow,
                Content = "Great analysis!"
            },
            new Comment
            {
                Id = Guid.Parse("f3491566-6162-47b3-9f07-726d1a4e719e"),
                ReviewId = Guid.Parse("28f31c22-76d2-4b99-a3d7-420de3b0f527"),
                UserId = Guid.Parse("bdbf650b-5551-4aeb-b978-88f7d15c7bcf"),
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow,
                Content = "go touch some grass man"
            }
        ];
    }
}