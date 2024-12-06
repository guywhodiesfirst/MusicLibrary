using Data.Data;
using Data.Entities;
using Data.Repositories;

namespace MusicLibrary.Tests.DataLayerTests
{
    [TestFixture]
    public class UserRepositoryTests
    {
        [TestCase("98c9c918-77f2-4b8b-8df1-55f3eecf74e3")]
        [TestCase("bdbf650b-5551-4aeb-b978-88f7d15c7bcf")]
        [TestCase("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")]
        public async Task UserRepositoryGetByIdAsyncReturnsEntity(Guid id)
        {
            //arrange
            using var context = new MusicLibraryDataContext(UnitTestHelper.GetUnitTestDbOptions());
            var userRepository = new UserRepository(context);
            var expected = ExpectedEntities.Users.FirstOrDefault(x => x.Id == id);

            //action
            var user = await userRepository.GetByIdAsync(id);

            //assert
            Assert.That(user, Is.EqualTo(expected).Using(new UserEqualityComparer()), message: "GetByIdAsync works incorrectly");
        }

        [Test]
        public async Task PlaylistRepositoryGetAllAsyncReturnsAllEntities()
        {
            //arrange
            using var context = new MusicLibraryDataContext(UnitTestHelper.GetUnitTestDbOptions());
            var userRepository = new UserRepository(context);

            //action
            var users = await userRepository.GetAllAsync();

            //assert
            Assert.That(users, Is.EqualTo(ExpectedEntities.Users).Using(new UserEqualityComparer()), message: "GetAllAsync works incorrectly");
        }

        [Test]
        public async Task PlaylistRepositoryAddAsyncAddsNewEntity()
        {
            //arrange
            using var context = new MusicLibraryDataContext(UnitTestHelper.GetUnitTestDbOptions());
            var userRepository = new UserRepository(context);
            var unitOfWork = new UnitOfWork(context);
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = "CorvoAttano",
                Password = "password",
                Email = "corvo@mail.com",
                IsAdmin = false,
                IsBlocked = false
            };

            //action
            await userRepository.AddAsync(user);
            await unitOfWork.SaveChangesAsync();

            //assert
            Assert.That(context.Users.Count(), Is.EqualTo(3), message: "AddAsync works incorrectly");
        }

        [Test]
        public async Task PlaylistRepositoryUpdateAsyncUpdatesValue()
        {
            //arrange
            using var context = new MusicLibraryDataContext(UnitTestHelper.GetUnitTestDbOptions());
            var userRepository = new UserRepository(context);
            var unitOfWork = new UnitOfWork(context);
            var user = ExpectedEntities.Users.FirstOrDefault();
            user!.Username = "Yoshi22";
            user!.Password = "shouldichangemyhat";
            user!.IsAdmin = true;

            //action
            await userRepository.UpdateAsync(user);
            await unitOfWork.SaveChangesAsync();
            var userInDb = await userRepository.GetByIdAsync(user.Id);

            //assert
            Assert.That(user, Is.EqualTo(userInDb).Using(new UserEqualityComparer()), message: "UpdateAsync works incorrectly");
        }

        [TestCase("98c9c918-77f2-4b8b-8df1-55f3eecf74e3")]
        [TestCase("bdbf650b-5551-4aeb-b978-88f7d15c7bcf")]
        [TestCase("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")]
        public async Task PlaylistRepositoryGetByIdWithDetailsAsyncReturnsEntity(Guid id)
        {
            //arrange
            using var context = new MusicLibraryDataContext(UnitTestHelper.GetUnitTestDbOptions());
            var userRepository = new UserRepository(context);
            var expected = ExpectedEntities.Users.FirstOrDefault(x => x.Id == id);

            //action
            var user = await userRepository.GetByIdWithDetailsAsync(id);

            //assert
            Assert.That(user, Is.EqualTo(expected).Using(new UserEqualityComparer()), message: "GetByIdWithDetailsAsync works incorrectly");

            if (user != null)
            {
                Assert.That(user.Reviews.ToList(),
                    Is.EqualTo(ExpectedEntities.Reviews.Where(x => x.UserId == user.Id).ToList())
                    .Using(new ReviewEqualityComparer()),
                    message: "GetByIdWithDetailsAsync doesn't include reviews");

                Assert.That(user.Playlists.ToList(),
                    Is.EqualTo(ExpectedEntities.Playlists.Where(x => x.UserId == user.Id).ToList())
                    .Using(new PlaylistEqualityComparer()),
                    message: "GetByIdWithDetailsAsync doesn't include playlists");

                Assert.That(user.Reactions.ToList(),
                    Is.EqualTo(ExpectedEntities.ReviewReactions.Where(x => x.UserId == user.Id).ToList())
                    .Using(new ReviewReactionEqualityComparer()),
                    message: "GetByIdWithDetailsAsync doesn't include review reactions");

                Assert.That(user.Comments.ToList(),
                    Is.EqualTo(ExpectedEntities.Comments.Where(x => x.UserId == user.Id).ToList())
                    .Using(new CommentEqualityComparer()),
                    message: "GetByIdWithDetailsAsync doesn't include review comments");
            }
        }

        [TestCase("bdbf650b-5551-4aeb-b978-88f7d15c7bcf")]
        public async Task PlaylistRepositoryDeleteByIdAsyncDeletesValue(Guid id)
        {
            //arrange
            using var context = new MusicLibraryDataContext(UnitTestHelper.GetUnitTestDbOptions());
            var userRepository = new UserRepository(context);
            var user = await userRepository.GetByIdWithDetailsAsync(id);

            //action
            await userRepository.DeleteByIdAsync(id);
            await context.SaveChangesAsync();
            bool isUserDeleted = (await userRepository.GetByIdWithDetailsAsync(id) == null);

            //assert
            Assert.That(isUserDeleted, Is.EqualTo(true), message: "The album is still present in the database");
        }
    }
}