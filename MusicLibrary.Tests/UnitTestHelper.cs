using Data.Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MusicLibrary.Tests
{
    internal static class UnitTestHelper
    {
        public static DbContextOptions<MusicLibraryDataContext> GetUnitTestDbOptions()
        {
            var options = new DbContextOptionsBuilder<MusicLibraryDataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using(var context = new MusicLibraryDataContext(options))
            {
                SeedData(context);
            }
            return options;
        }
        public static void SeedData(MusicLibraryDataContext context)
        {
            //var genres = new List<Genre>
            //{
            //    new Genre { Id = Guid.Parse("71bcd091-c2b8-4432-a482-1e3d25b62e4b"), Name = "Rock" },
            //    new Genre { Id = Guid.Parse("d9a6b1f4-7a5f-45d2-bb6e-fc3240ec6a4e"), Name = "Pop" },
            //    new Genre { Id = Guid.Parse("4a7cd7b9-2a53-4e47-b87b-e486aba6f3d3"), Name = "Jazz" },
            //    new Genre { Id = Guid.Parse("fc8d5de4-4a15-4a25-a2ad-5078432fd16d"), Name = "Classical" }
            //};
            //context.Genres.AddRange(genres);

            var albums = new List<Album>
            {
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
            };
            context.Albums.AddRange(albums);

            var users = new List<User>
            {
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
            };
            context.Users.AddRange(users);

            var playlists = new List<Playlist>
            {
                new Playlist
                {
                    Id = Guid.Parse("439d8404-5bcb-4bd4-a91f-71f15a15a2d9"),
                    Name = "My Favorites",
                    Description = "Best of the best",
                    UserId = users[1].Id
                },
                new Playlist
                {
                    Id = Guid.Parse("bf09353e-89dc-4147-8a87-dce98783d6f8"),
                    Name = "Relaxing Tunes",
                    Description = "Chill and unwind",
                    UserId = users[1].Id
                }
            };
            context.Playlists.AddRange(playlists);

            var reviews = new List<Review>
            {
                new Review
                {
                    Id = Guid.Parse("28f31c22-76d2-4b99-a3d7-420de3b0f527"),
                    UserId = users[1].Id,
                    AlbumId = albums[0].Id,
                    Rating = 5,
                    Content = "Amazing album!",
                    CreatedAt = DateTime.UtcNow
                },
                new Review
                {
                    Id = Guid.Parse("ae89cce6-737c-4cb7-8c30-95ef1c3f4b4b"),
                    UserId = users[1].Id,
                    AlbumId = albums[1].Id,
                    Rating = 4,
                    Content = "Great album but a bit overrated.",
                    CreatedAt = DateTime.UtcNow
                }
            };
            context.Reviews.AddRange(reviews);

            var albumPlaylists = new List<AlbumPlaylist>
            {
                new AlbumPlaylist
                {
                    AlbumId = albums[0].Id,
                    PlaylistId = playlists[0].Id
                },
                new AlbumPlaylist
                {
                    AlbumId = albums[1].Id,
                    PlaylistId = playlists[1].Id
                }
            };
            context.AlbumPlaylists.AddRange(albumPlaylists);

            var reviewReactions = new List<ReviewReaction>
            {
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
            };
            context.Reactions.AddRange(reviewReactions);

            var comments = new List<Comment>
            {
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
            };
            context.Comments.AddRange(comments);

            context.SaveChanges();
        }
    }
}