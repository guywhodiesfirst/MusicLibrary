using Data.Entities;

namespace Data.Interfaces
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<bool> DoesReviewExistAsync(Guid userId, Guid albumId);
    }
}