using Business.Models;

namespace Business.Interfaces
{
    public interface ICommentService : IService<CommentDto>
    {
        Task AddAsync(CommentDto model);
    }
}