using Business.Models;

namespace Business.Interfaces
{
    public interface IReviewService : IService<ReviewDto>
    {
        Task AddAsync(ReviewDto model);
        Task UpdateAsync(ReviewDto model);
        Task<bool> DoesReviewExistByIdAsync(Guid modelId);
        // TODO: add reaction methods 
    }
}