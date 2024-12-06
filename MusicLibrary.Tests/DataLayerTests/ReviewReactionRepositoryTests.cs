using Data.Data;
using Data.Entities;
using Data.Repositories;

namespace MusicLibrary.Tests.DataLayerTests
{
    [TestFixture]
    public class ReviewReactionRepositoryTests
    {
        [TestCase("ac869b99-3435-4e57-a34b-2d20ddfda4b1")]
        [TestCase("28f31c22-76d2-4b99-a3d7-420de3b0f527")]
        [TestCase("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")]
        public async Task ReviewReactionRepositoryGetByIdAsyncReturnsEntity(Guid id)
        {
            //arrange
            using var context = new MusicLibraryDataContext(UnitTestHelper.GetUnitTestDbOptions());
            var reviewReactionRepository = new ReviewReactionRepository(context);
            var expected = ExpectedEntities.ReviewReactions.FirstOrDefault(x => x.Id == id);

            //action
            var reviewReaction = await reviewReactionRepository.GetByIdAsync(id);

            //assert
            Assert.That(reviewReaction, Is.EqualTo(expected).Using(new ReviewReactionEqualityComparer()), message: "GetByIdAsync works incorrectly");
        }

        [Test]
        public async Task ReviewReactionRepositoryGetAllAsyncReturnsAllEntities()
        {
            //arrange
            using var context = new MusicLibraryDataContext(UnitTestHelper.GetUnitTestDbOptions());
            var reviewReactionRepository = new ReviewReactionRepository(context);

            //action
            var reviewReactions = await reviewReactionRepository.GetAllAsync();

            //assert
            Assert.That(reviewReactions, Is.EqualTo(ExpectedEntities.ReviewReactions).Using(new ReviewReactionEqualityComparer()), message: "GetAllAsync works incorrectly");
        }

        [Test]
        public async Task ReviewReactionRepositoryAddAsyncAddsNewEntity()
        {
            //arrange
            using var context = new MusicLibraryDataContext(UnitTestHelper.GetUnitTestDbOptions());
            var reviewReactionRepository = new ReviewReactionRepository(context);
            var unitOfWork = new UnitOfWork(context);
            var reviewReaction = new ReviewReaction
            {
                Id = Guid.NewGuid(),
                ReviewId = Guid.Parse("28f31c22-76d2-4b99-a3d7-420de3b0f527"),
                UserId = Guid.Parse("bdbf650b-5551-4aeb-b978-88f7d15c7bcf"),
                IsLike = true
            };

            //action
            await reviewReactionRepository.AddAsync(reviewReaction);
            await unitOfWork.SaveChangesAsync();

            //assert
            Assert.That(context.Reactions.Count(), Is.EqualTo(3), message: "AddAsync works incorrectly");
        }

        [Test]
        public async Task ReviewReactionRepositoryUpdateAsyncUpdatesValue()
        {
            //arrange
            using var context = new MusicLibraryDataContext(UnitTestHelper.GetUnitTestDbOptions());
            var reviewReactionRepository = new ReviewReactionRepository(context);
            var unitOfWork = new UnitOfWork(context);
            var reviewReaction = ExpectedEntities.ReviewReactions.FirstOrDefault();
            reviewReaction!.IsLike = false;

            //action
            await reviewReactionRepository.UpdateAsync(reviewReaction);
            await unitOfWork.SaveChangesAsync();
            var reactionInDb = await reviewReactionRepository.GetByIdAsync(reviewReaction.Id);

            //assert
            Assert.That(reviewReaction, Is.EqualTo(reactionInDb).Using(new ReviewReactionEqualityComparer()), message: "UpdateAsync works incorrectly");
        }

        [TestCase("ac869b99-3435-4e57-a34b-2d20ddfda4b1")]
        public async Task ReviewReactionRepositoryDeleteByIdAsyncDeletesValue(Guid id)
        {
            //arrange
            using var context = new MusicLibraryDataContext(UnitTestHelper.GetUnitTestDbOptions());
            var reviewReactionRepository = new ReviewReactionRepository(context);
            var reviewReaction = await reviewReactionRepository.GetByIdAsync(id);

            //action
            await reviewReactionRepository.DeleteByIdAsync(id);
            await context.SaveChangesAsync();
            bool isReactionDeleted = (await reviewReactionRepository.GetByIdAsync(id) == null);

            //assert
            Assert.That(isReactionDeleted, Is.EqualTo(true), message: "The genre is still present in the database");
        }
    }
}