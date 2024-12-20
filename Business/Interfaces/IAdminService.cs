namespace Business.Interfaces
{
    public interface IAdminService
    {
        Task BlockUserAsync(Guid userId);
        Task UnblockUserAsync(Guid userId);
        Task BlockReviewAsync(Guid reviewId);
        Task UnblockReviewAsync(Guid reviewId);
        Task BlockCommentAsync(Guid commentId);
        Task UnblockCommentAsync(Guid commentId);
    }
}
