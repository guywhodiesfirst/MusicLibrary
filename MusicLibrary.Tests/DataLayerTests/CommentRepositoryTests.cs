using Data.Data;
using Data.Entities;
using Data.Repositories;

namespace MusicLibrary.Tests.DataLayerTests
{
    [TestFixture]
    public class CommentRepositoryTests
    {
        [TestCase("9a81e7a9-2bf8-4ef9-b2c2-81bbcd393908")]
        [TestCase("f3491566-6162-47b3-9f07-726d1a4e719e")]
        [TestCase("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")]
        public async Task CommentRepositoryGetByIdAsyncReturnsEntity(Guid id)
        {
            //arrange
            using var context = new MusicLibraryDataContext(UnitTestHelper.GetUnitTestDbOptions());
            var commentRepository = new CommentRepository(context);
            var expected = ExpectedEntities.Comments.FirstOrDefault(x => x.Id == id);

            //action
            var comment = await commentRepository.GetByIdAsync(id);

            //assert
            Assert.That(comment, Is.EqualTo(expected).Using(new CommentEqualityComparer()), message: "GetByIdAsync works incorrectly");
        }

        [Test]
        public async Task CommentRepositoryGetAllAsyncReturnsAllEntities()
        {
            //arrange
            using var context = new MusicLibraryDataContext(UnitTestHelper.GetUnitTestDbOptions());
            var commentRepository = new CommentRepository(context);

            //action
            var comments = await commentRepository.GetAllAsync();

            //assert
            Assert.That(comments, Is.EqualTo(ExpectedEntities.Comments).Using(new CommentEqualityComparer()), message: "GetAllAsync works incorrectly");
        }

        [Test]
        public async Task CommentRepositoryAddAsyncAddsNewEntity()
        {
            //arrange
            using var context = new MusicLibraryDataContext(UnitTestHelper.GetUnitTestDbOptions());
            var commentRepository = new CommentRepository(context);
            var unitOfWork = new UnitOfWork(context);
            var comment = new Comment
            {
                Id = Guid.NewGuid(),
                ReviewId = Guid.Parse("28f31c22-76d2-4b99-a3d7-420de3b0f527"),
                UserId = Guid.Parse("98c9c918-77f2-4b8b-8df1-55f3eecf74e3"),
                Content = "I don't completely agree with you but I respect your opinion",
                IsDeleted = false,
                CreatedAt = DateTime.Now,
            };

            //action
            await commentRepository.AddAsync(comment);
            await unitOfWork.SaveChangesAsync();

            //assert
            Assert.That(context.Comments.Count(), Is.EqualTo(3), message: "AddAsync works incorrectly");
        }

        [Test]
        public async Task CommentRepositoryUpdateAsyncUpdatesValue()
        {
            //arrange
            using var context = new MusicLibraryDataContext(UnitTestHelper.GetUnitTestDbOptions());
            var commentRepository = new CommentRepository(context);
            var unitOfWork = new UnitOfWork(context);
            var comment = ExpectedEntities.Comments.FirstOrDefault();
            comment!.Content = "You have a point";
            comment.IsDeleted = true;

            //action
            await commentRepository.UpdateAsync(comment);
            await unitOfWork.SaveChangesAsync();
            var commentInDb = await commentRepository.GetByIdAsync(comment.Id);

            //assert
            Assert.That(comment, Is.EqualTo(commentInDb).Using(new CommentEqualityComparer()), message: "UpdateAsync works incorrectly");
        }

        [TestCase("f3491566-6162-47b3-9f07-726d1a4e719e")]
        public async Task CommentRepositoryDeleteByIdAsyncDeletesValue(Guid id)
        {
            //arrange
            using var context = new MusicLibraryDataContext(UnitTestHelper.GetUnitTestDbOptions());
            var commentRepository = new CommentRepository(context);
            var comment = await commentRepository.GetByIdAsync(id);

            //action
            await commentRepository.DeleteByIdAsync(id);
            await context.SaveChangesAsync();
            bool isCommentDeleted = (await commentRepository.GetByIdAsync(id) == null);

            //assert
            Assert.That(isCommentDeleted, Is.EqualTo(true), message: "The comment is still present in the database");
        }
    }
}