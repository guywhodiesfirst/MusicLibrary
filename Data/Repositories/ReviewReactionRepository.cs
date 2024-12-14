using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class ReviewReactionRepository : IReviewReactionRepository
    {
        private readonly MusicLibraryDataContext _context;
        public ReviewReactionRepository(MusicLibraryDataContext context)
        {
            _context = context;
        }
        public async Task AddAsync(ReviewReaction entity)
        {
            await _context.Reactions.AddAsync(entity);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var reaction = await _context.Reactions.FindAsync(id);
            if (reaction != null)
            {
                _context.Reactions.Remove(reaction);
            }
        }

        public async Task<IEnumerable<ReviewReaction>> GetAllAsync()
        {
            var reactions = await _context.Reactions.ToListAsync();
            return reactions ?? null;
        }

        public async Task<ReviewReaction> GetByIdAsync(Guid id)
        {
            var reaction = await _context.Reactions
                                    .Include(rr => rr.User)
                                    .Include(rr => rr.Review)
                                    .FirstOrDefaultAsync(rr => rr.Id == id);
            return reaction ?? null;
        }

        public async Task<ReviewReaction> GetByUserReviewIdAsync(Guid userId, Guid reviewId)
        {
            var reaction = await _context.Reactions.FirstOrDefaultAsync(rr => rr.UserId == userId && rr.ReviewId == reviewId);
            return reaction ?? null;
        }

        public async Task UpdateAsync(ReviewReaction entity)
        {
            var reaction = await _context.Reactions.FindAsync(entity.Id);
            if (reaction != null)
            {
                reaction.IsLike = entity.IsLike;
            }
        }
    }
}