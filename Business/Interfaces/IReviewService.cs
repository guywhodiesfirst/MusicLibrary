using Business.Models.Reviews;

namespace Business.Interfaces
{
    public interface IReviewService : IService<ReviewDto>
    {
        Task AddAsync(ReviewCreateDto model);
        Task UpdateAsync(ReviewUpdateDto model);
        Task<bool> DoesReviewExistByIdAsync(Guid modelId);
        Task<bool> IsUserReviewOwnerAsync(Guid userId, Guid reviewId);
        Task<bool> IsUserReactionOwnerAsync(Guid userId, Guid reactionId);
        Task<ReviewDetailsDto> GetByIdWithDetailsAsync(Guid modelId);
        Task<ReviewDto> GetByAlbumUserIdAsync(Guid albumId, Guid userId);
        Task<IEnumerable<ReviewDto>> GetAllByAlbumIdAsync(Guid albumId);
        Task AddReactionAsync(ReviewReactionDto reactionModel);
        Task UpdateReactionAsync(Guid reactionId);
        Task DeleteReactionAsync(Guid reactionId);
    }
}