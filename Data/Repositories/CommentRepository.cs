using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly MusicLibraryDataContext _context;
        public CommentRepository(MusicLibraryDataContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Comment entity)
        {
            await _context.Comments.AddAsync(entity);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
            }
        }

        public async Task<IEnumerable<Comment>> GetAllAsync()
        {
            var comments = await _context.Comments.ToListAsync();
            return comments ?? null;
        }

        public async Task<Comment> GetByIdAsync(Guid id)
        {
            var comment = await _context.Comments.FindAsync(id);
            return comment ?? null;
        }

        public async Task UpdateAsync(Comment entity)
        {
            if (entity != null)
            {
                var comment = await _context.Comments.FindAsync(entity.Id);
                if (comment != null)
                {
                    comment.Content = entity.Content;
                    comment.IsDeleted = entity.IsDeleted;
                    comment.LastUpdatedAt = entity.LastUpdatedAt;
                }
            }
        }
    }
}
