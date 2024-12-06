using Data.Data;
using Data.Entities;
using Data.Repositories;

namespace MusicLibrary.Tests.DataLayerTests
{
    [TestFixture]
    public class ReviewRepositoryTests
    {
        [TestCase("28f31c22-76d2-4b99-a3d7-420de3b0f527")]
        [TestCase("ae89cce6-737c-4cb7-8c30-95ef1c3f4b4b")]
        [TestCase("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")]
        public async Task ReviewRepositoryGetByIdAsyncReturnsEntity(Guid id)
        {
            //arrange
            using var context = new MusicLibraryDataContext(UnitTestHelper.GetUnitTestDbOptions());
            var reviewRepository = new ReviewRepository(context);
            var expected = ExpectedEntities.Reviews.FirstOrDefault(x => x.Id == id);

            //action
            var review = await reviewRepository.GetByIdAsync(id);

            //assert
            Assert.That(review, Is.EqualTo(expected).Using(new ReviewEqualityComparer()), message: "GetByIdAsync works incorrectly");
        }

        [Test]
        public async Task ReviewRepositoryGetAllAsyncReturnsAllEntities()
        {
            //arrange
            using var context = new MusicLibraryDataContext(UnitTestHelper.GetUnitTestDbOptions());
            var reviewRepository = new ReviewRepository(context);

            //action
            var reviews = await reviewRepository.GetAllAsync();

            //assert
            Assert.That(reviews, Is.EqualTo(ExpectedEntities.Reviews).Using(new ReviewEqualityComparer()), message: "GetAllAsync works incorrectly");
        }

        [Test]
        public async Task ReviewRepositoryAddAsyncAddsNewEntity()
        {
            //arrange
            using var context = new MusicLibraryDataContext(UnitTestHelper.GetUnitTestDbOptions());
            var reviewRepository = new ReviewRepository(context);
            var unitOfWork = new UnitOfWork(context);
            var review = new Review
            {
                Id = Guid.NewGuid(),
                AlbumId = Guid.Parse("7b0d93e6-9e3d-4e58-9c7b-bc52e0a730af"),
                UserId = Guid.Parse("bdbf650b-5551-4aeb-b978-88f7d15c7bcf"),
                Rating = 7,
                NetVotes = 0,
                Content = "Good. Very good. Pretty good.",
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow
            };

            //action
            await reviewRepository.AddAsync(review);
            await unitOfWork.SaveChangesAsync();

            //assert
            Assert.That(context.Reviews.Count(), Is.EqualTo(3), message: "AddAsync works incorrectly");
        }

        [Test]
        public async Task ReviewRepositoryUpdateAsyncUpdatesValue()
        {
            //arrange
            using var context = new MusicLibraryDataContext(UnitTestHelper.GetUnitTestDbOptions());
            var reviewRepository = new ReviewRepository(context);
            var unitOfWork = new UnitOfWork(context);
            var review = ExpectedEntities.Reviews.FirstOrDefault();
            review!.IsDeleted = false;
            review.Content = "Okay, maybe I was wrong about this album";
            review.Rating = 10;
            review.LastUpdatedAt = DateTime.UtcNow;

            //action
            await reviewRepository.UpdateAsync(review);
            await unitOfWork.SaveChangesAsync();
            var reviewInDb = await reviewRepository.GetByIdAsync(review.Id);

            //assert
            Assert.That(review, Is.EqualTo(reviewInDb).Using(new ReviewEqualityComparer()), message: "UpdateAsync works incorrectly");
        }

        [TestCase("28f31c22-76d2-4b99-a3d7-420de3b0f527")]
        [TestCase("ae89cce6-737c-4cb7-8c30-95ef1c3f4b4b")]
        public async Task ReviewRepositoryDeleteByIdAsyncDeletesValue(Guid id)
        {
            //arrange
            using var context = new MusicLibraryDataContext(UnitTestHelper.GetUnitTestDbOptions());
            var reviewRepository = new ReviewRepository(context);
            var review = await reviewRepository.GetByIdAsync(id);

            //action
            await reviewRepository.DeleteByIdAsync(id);
            await context.SaveChangesAsync();
            bool isReviewDeleted = (await reviewRepository.GetByIdAsync(id) == null);

            //assert
            Assert.That(isReviewDeleted, Is.EqualTo(true), message: "The genre is still present in the database");
        }
    }
}