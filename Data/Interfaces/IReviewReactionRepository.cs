using Data.Entities;

namespace Data.Interfaces
{
    public interface IReviewReactionRepository : IRepository<ReviewReaction>
    {
        Task<ReviewReaction> GetByUserReviewIdAsync(Guid userId, Guid reviewId);
    }
}
