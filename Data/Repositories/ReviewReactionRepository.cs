using Data.Data;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories
{
    public class ReviewReactionRepository : IReviewReactionRepository
    {
        private readonly MusicLibraryDataContext _context;
        public ReviewReactionRepository(MusicLibraryDataContext context)
        {
            _context = context;
        }
        public Task AddAsync(ReviewReaction entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ReviewReaction>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ReviewReaction> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ReviewReaction entity)
        {
            throw new NotImplementedException();
        }
    }
}
