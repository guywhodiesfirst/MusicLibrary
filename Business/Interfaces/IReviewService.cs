using Business.Models.Reviews;

namespace Business.Interfaces
{
    public interface IReviewService : IService<ReviewDto>
    {
        Task AddAsync(ReviewCreateDto model);
        Task UpdateAsync(ReviewUpdateDto model);
        Task<bool> DoesReviewExistByIdAsync(Guid modelId);
        Task<ReviewDetailsDto> GetByIdWithDetailsAsync(Guid modelId);
        // TODO: add reaction methods 
    }
}