using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly MusicLibraryDataContext _context;
        public ReviewRepository(MusicLibraryDataContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Review entity)
        {
            await _context.Reviews.AddAsync(entity);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
            }
        }

        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            var users = await _context.Reviews.ToListAsync();
            return users;
        }

        public async Task<Review> GetByIdAsync(Guid id)
        {
            var review = await _context.Reviews.FindAsync(id);
            return review ?? null;
        }

        public async Task UpdateAsync(Review entity)
        {
            if (entity != null)
            {
                var review = await _context.Reviews.FindAsync(entity.Id);
                if (review != null)
                {
                    review.Rating = entity.Rating;
                    review.Content = entity.Content;
                }
            }
        }
    }
}
