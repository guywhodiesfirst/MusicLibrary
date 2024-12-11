using Business.Models;

namespace Business.Interfaces
{
    public interface IReviewService : IService<ReviewDto>
    {
        Task AddAsync(ReviewDto model);
    }
}