//using Data.Data;
//using Data.Entities;
//using Data.Repositories;

//namespace MusicLibrary.Tests.DataLayerTests
//{
//    [TestFixture]
//    public class GenreRepositoryTests
//    {
//        [TestCase("71bcd091-c2b8-4432-a482-1e3d25b62e4b")]
//        [TestCase("d9a6b1f4-7a5f-45d2-bb6e-fc3240ec6a4e")]
//        [TestCase("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")]
//        public async Task GenreRepositoryGetByIdAsyncReturnsEntity(Guid id)
//        {
//            //arrange
//            using var context = new MusicLibraryDataContext(UnitTestHelper.GetUnitTestDbOptions());
//            var genreRepository = new GenreRepository(context);
//            var expected = ExpectedEntities.Genres.FirstOrDefault(x => x.Id == id);

//            //action
//            var genre = await genreRepository.GetByIdAsync(id);

//            //assert
//            Assert.That(genre, Is.EqualTo(expected).Using(new GenreEqualityComparer()), message: "GetByIdAsync works incorrectly");
//        }

//        [Test]
//        public async Task GenreRepositoryGetAllAsyncReturnsAllEntities()
//        {
//            //arrange
//            using var context = new MusicLibraryDataContext(UnitTestHelper.GetUnitTestDbOptions());
//            var genreRepository = new GenreRepository(context);

//            //action
//            var genres = await genreRepository.GetAllAsync();

//            //assert
//            Assert.That(genres, Is.EqualTo(ExpectedEntities.Genres).Using(new GenreEqualityComparer()), message: "GetAllAsync works incorrectly");
//        }

//        [Test]
//        public async Task GenreRepositoryAddAsyncAddsNewEntity()
//        {
//            //arrange
//            using var context = new MusicLibraryDataContext(UnitTestHelper.GetUnitTestDbOptions());
//            var genreRepository = new GenreRepository(context);
//            var unitOfWork = new UnitOfWork(context);
//            var genre = new Genre
//            {
//                Id = Guid.NewGuid(),
//                Name = "Indie folk"
//            };

//            //action
//            await genreRepository.AddAsync(genre);
//            await unitOfWork.SaveChangesAsync();

//            //assert
//            Assert.That(context.Genres.Count(), Is.EqualTo(5), message: "AddAsync works incorrectly");
//        }

//        [Test]
//        public async Task GenreRepositoryUpdateAsyncUpdatesValue()
//        {
//            //arrange
//            using var context = new MusicLibraryDataContext(UnitTestHelper.GetUnitTestDbOptions());
//            var genreRepository = new GenreRepository(context);
//            var unitOfWork = new UnitOfWork(context);
//            var genre = ExpectedEntities.Genres.FirstOrDefault();
//            genre!.Name = "Pop punk";

//            //action
//            await genreRepository.UpdateAsync(genre);
//            await unitOfWork.SaveChangesAsync();
//            var genreInDb = await genreRepository.GetByIdAsync(genre.Id);

//            //assert
//            Assert.That(genre, Is.EqualTo(genreInDb).Using(new GenreEqualityComparer()), message: "UpdateAsync works incorrectly");
//        }

//        [TestCase("71bcd091-c2b8-4432-a482-1e3d25b62e4b")]
//        public async Task GenreRepositoryDeleteByIdAsyncDeletesValue(Guid id)
//        {
//            //arrange
//            using var context = new MusicLibraryDataContext(UnitTestHelper.GetUnitTestDbOptions());
//            var genreRepository = new GenreRepository(context);
//            var genre = await genreRepository.GetByIdAsync(id);

//            //action
//            await genreRepository.DeleteByIdAsync(id);
//            await context.SaveChangesAsync();
//            bool isGenreDeleted = (await genreRepository.GetByIdAsync(id) == null);

//            //assert
//            Assert.That(isGenreDeleted, Is.EqualTo(true), message: "The genre is still present in the database");
//        }
//    }
//}