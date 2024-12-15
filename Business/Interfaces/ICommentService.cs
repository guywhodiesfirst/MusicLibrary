using Business.Models.Comments;

namespace Business.Interfaces
{
    public interface ICommentService : IService<CommentDto>
    {
        Task AddAsync(CommentDto model);
        Task<bool> IsUserCommentOwnerAsync(Guid userId, Guid commentId);
        Task<IEnumerable<CommentDto>> GetAllByReviewIdAsync(Guid reviewId);
    }
}