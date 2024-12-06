using Data.Data;
using Data.Entities;
using Data.Repositories;

namespace MusicLibrary.Tests.DataLayerTests
{
    [TestFixture]
    public class PlaylistRepositoryTests
    {
        [TestCase("439d8404-5bcb-4bd4-a91f-71f15a15a2d9")]
        [TestCase("bf09353e-89dc-4147-8a87-dce98783d6f8")]
        [TestCase("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")]
        public async Task PlaylistRepositoryGetByIdAsyncReturnsEntity(Guid id)
        {
            //arrange
            using var context = new MusicLibraryDataContext(UnitTestHelper.GetUnitTestDbOptions());
            var playlistRepository = new PlaylistRepository(context);
            var expected = ExpectedEntities.Playlists.FirstOrDefault(x => x.Id == id);

            //action
            var playlist = await playlistRepository.GetByIdAsync(id);

            //assert
            Assert.That(playlist, Is.EqualTo(expected).Using(new PlaylistEqualityComparer()), message: "GetByIdAsync works incorrectly");
        }

        [Test]
        public async Task PlaylistRepositoryGetAllAsyncReturnsAllEntities()
        {
            //arrange
            using var context = new MusicLibraryDataContext(UnitTestHelper.GetUnitTestDbOptions());
            var playlistRepository = new PlaylistRepository(context);

            //action
            var playlists = await playlistRepository.GetAllAsync();

            //assert
            Assert.That(playlists, Is.EqualTo(ExpectedEntities.Playlists).Using(new PlaylistEqualityComparer()), message: "GetAllAsync works incorrectly");
        }

        [Test]
        public async Task PlaylistRepositoryAddAsyncAddsNewEntity()
        {
            //arrange
            using var context = new MusicLibraryDataContext(UnitTestHelper.GetUnitTestDbOptions());
            var playlistRepository = new PlaylistRepository(context);
            var unitOfWork = new UnitOfWork(context);
            var playlist = new Playlist
            {
                Id = Guid.NewGuid(),
                Name = "Videogame OSTs",
                Description = "Good stuff from videogames",
                UserId = Guid.Parse("98c9c918-77f2-4b8b-8df1-55f3eecf74e3"),
                CreatedAt = DateTime.UtcNow
            };

            //action
            await playlistRepository.AddAsync(playlist);
            await unitOfWork.SaveChangesAsync();

            //assert
            Assert.That(context.Playlists.Count(), Is.EqualTo(3), message: "AddAsync works incorrectly");
        }

        [Test]
        public async Task PlaylistRepositoryUpdateAsyncUpdatesValue()
        {
            //arrange
            using var context = new MusicLibraryDataContext(UnitTestHelper.GetUnitTestDbOptions());
            var playlistRepository = new PlaylistRepository(context);
            var unitOfWork = new UnitOfWork(context);
            var playlist = ExpectedEntities.Playlists.FirstOrDefault();
            playlist!.Name = "Cool metal albums";
            playlist!.Description = "hell yeah";

            //action
            await playlistRepository.UpdateAsync(playlist);
            await unitOfWork.SaveChangesAsync();
            var playlistInDb = await playlistRepository.GetByIdAsync(playlist.Id);

            //assert
            Assert.That(playlist, Is.EqualTo(playlistInDb).Using(new PlaylistEqualityComparer()), message: "UpdateAsync works incorrectly");
        }

        [TestCase("439d8404-5bcb-4bd4-a91f-71f15a15a2d9")]
        [TestCase("bf09353e-89dc-4147-8a87-dce98783d6f8")]
        [TestCase("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")]
        public async Task PlaylistRepositoryGetByIdWithDetailsAsyncReturnsEntity(Guid id)
        {
            //arrange
            using var context = new MusicLibraryDataContext(UnitTestHelper.GetUnitTestDbOptions());
            var playlistRepository = new PlaylistRepository(context);
            var expected = ExpectedEntities.Playlists.FirstOrDefault(x => x.Id == id);

            //action
            var playlist = await playlistRepository.GetByIdWithDetailsAsync(id);


            //assert
            Assert.That(playlist, Is.EqualTo(expected).Using(new PlaylistEqualityComparer()), message: "GetByIdWithDetailsAsync works incorrectly");

            if (playlist != null)
            {
                Assert.That(playlist.User,
                Is.EqualTo(ExpectedEntities.Users.Single(x => x.Id == expected?.UserId))
                    .Using(new UserEqualityComparer()),
                    message: "GetByIdWithDetailsAsync doesn't include user");
                Assert.That(playlist.Albums, Has.Count.GreaterThan(0));
                Assert.That(playlist.Albums.Any(album => album.Playlists.Contains(playlist)), Is.True, "The album should be present in the playlists.");
            }
        }

        [TestCase("bf09353e-89dc-4147-8a87-dce98783d6f8")]
        public async Task PlaylistRepositoryDeleteByIdAsyncDeletesValue(Guid id)
        {
            //arrange
            using var context = new MusicLibraryDataContext(UnitTestHelper.GetUnitTestDbOptions());
            var playlistRepository = new PlaylistRepository(context);
            var playlist = await playlistRepository.GetByIdWithDetailsAsync(id);

            //action
            await playlistRepository.DeleteByIdAsync(id);
            await context.SaveChangesAsync();
            bool isPlaylistDeleted = (await playlistRepository.GetByIdWithDetailsAsync(id) == null);

            //assert
            Assert.That(isPlaylistDeleted, Is.EqualTo(true), message: "The album is still present in the database");
        }
    }
}