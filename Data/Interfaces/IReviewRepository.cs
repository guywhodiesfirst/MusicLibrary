using Data.Entities;

namespace Data.Interfaces
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<Review> GetByUserAlbumIdAsync(Guid userId, Guid albumId);
        Task<Review> GetByIdWithDetailsAsync(Guid id);
    }
}