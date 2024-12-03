using Data.Data;
using Data.Entities;
using Data.Repositories;

namespace MusicLibrary.Tests.DataLayerTests
{
    [TestFixture]
    public class AlbumRepositoryTests
    {
        [TestCase("7b0d93e6-9e3d-4e58-9c7b-bc52e0a730af")]
        [TestCase("285f6c88-dbf2-4dc0-8b82-30a06e125b8c")]
        [TestCase("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")]
        public async Task AlbumRepositoryGetByIdAsyncReturnsEntity(Guid id)
        {
            //arrange
            using var context = new MusicLibraryDataContext(UnitTestHelper.GetUnitTestDbOptions());
            var albumRepository = new AlbumRepository(context);
            var expected = ExpectedAlbums.FirstOrDefault(x => x.Id == id);
            
            //action
            var album = await albumRepository.GetByIdAsync(id);

            //assert
            Assert.That(album, Is.EqualTo(expected).Using(new AlbumEqualityComparer()), message: "GetByIdAsync works incorrectly");
        }

        [Test]
        public async Task AlbumRepositoryGetAllAsyncReturnsAllEntities()
        {
            //arrange
            using var context = new MusicLibraryDataContext(UnitTestHelper.GetUnitTestDbOptions());
            var albumRepository = new AlbumRepository(context);

            //action
            var albums = await albumRepository.GetAllAsync();

            //assert
            Assert.That(albums, Is.EqualTo(ExpectedAlbums).Using(new AlbumEqualityComparer()), message: "GetAllAsync works incorrectly");
        }

        [Test]
        public async Task AlbumRepositoryAddAsyncAddsNewEntity()
        {
            //arrange
            using var context = new MusicLibraryDataContext(UnitTestHelper.GetUnitTestDbOptions());
            var albumRepository = new AlbumRepository(context);
            var unitOfWork = new UnitOfWork(context);
            var album = new Album
            {
                Id = Guid.NewGuid(),
                Name = "American Idiot",
                Artists = new List<string> { "Green Day" },
                ReleaseDate = new DateTime(2004, 9, 21),
                GenreId = Guid.Parse("71bcd091-c2b8-4432-a482-1e3d25b62e4b")
            };

            //action
            await albumRepository.AddAsync(album);
            await unitOfWork.SaveChangesAsync();

            //assert
            Assert.That(context.Albums.Count(), Is.EqualTo(3), message: "AddAsync works incorrectly");
        }

        [Test]
        public async Task AlbumRepositoryUpdateAsyncUpdatesValue()
        {
            //arrange
            using var context = new MusicLibraryDataContext(UnitTestHelper.GetUnitTestDbOptions());
            var albumRepository = new AlbumRepository(context);
            var unitOfWork = new UnitOfWork(context);
            var album = ExpectedAlbums.FirstOrDefault();
            album!.Artists = new List<string> { "Metallica" };
            album.Name = "Master of Puppets";
            album.ReleaseDate = new DateTime(1986, 3, 3);

            //action
            await albumRepository.UpdateAsync(album);
            await unitOfWork.SaveChangesAsync();
            var albumInDb = await albumRepository.GetByIdAsync(album.Id);

            //assert
            Assert.That(album, Is.EqualTo(albumInDb).Using(new AlbumEqualityComparer()), message: "UpdateAsync works incorrectly");
        }

        private static IEnumerable<Album> ExpectedAlbums =>
        [
            new Album
            {
                Id = Guid.Parse("7b0d93e6-9e3d-4e58-9c7b-bc52e0a730af"),
                Name = "Dark Side of the Moon",
                Artists = new List<string> {"Pink Floyd"},
                ReleaseDate = new DateTime(1973, 3, 1),
                GenreId = Guid.Parse("71bcd091-c2b8-4432-a482-1e3d25b62e4b")
            },
            new Album
            {
                Id = Guid.Parse("285f6c88-dbf2-4dc0-8b82-30a06e125b8c"),
                Name = "Thriller",
                Artists = new List<string> {"Michael Jackson"},
                ReleaseDate = new DateTime(1982, 11, 30),
                GenreId = Guid.Parse("d9a6b1f4-7a5f-45d2-bb6e-fc3240ec6a4e")
            }
        ];
    }
}