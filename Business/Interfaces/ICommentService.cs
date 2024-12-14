using Business.Models.Comments;

namespace Business.Interfaces
{
    public interface ICommentService : IService<CommentDto>
    {
        Task AddAsync(CommentDto model);
    }
}