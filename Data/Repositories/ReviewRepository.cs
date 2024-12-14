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
                    review.Likes = entity.Likes;
                    review.Dislikes = entity.Dislikes;
                    review.Rating = entity.Rating;
                    review.Content = entity.Content;
                    review.LastUpdatedAt = entity.LastUpdatedAt;
                }
            }
        }

        public async Task<Review> GetByUserAlbumIdAsync(Guid userId, Guid albumId)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(x => x.UserId == userId && x.AlbumId == albumId);
            return review ?? null;
        }

        public async Task<Review> GetByIdWithDetailsAsync(Guid id)
        {
            var review = await _context.Reviews
                              .Include(r => r.User)
                              .Include(r => r.Album)
                              .Include(r => r.Comments)
                                .ThenInclude(c => c.User)
                              .Include(r => r.Reactions)
                              .FirstOrDefaultAsync(r => r.Id == id);

            return review ?? null;
        }

        public async Task<IEnumerable<Review>> GetAllWithDetailsAsync()
        {
            var reviews = await _context.Reviews
                                .Include(r => r.User)
                                .Include(r => r.Album)
                                .Include(r => r.Comments)
                                    .ThenInclude(c => c.User)
                                .Include(r => r.Reactions)
                                .ToListAsync();

            return reviews ?? null;
        }
    }
}
