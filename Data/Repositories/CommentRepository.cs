using Data.Data;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly MusicLibraryDataContext _context;
        public CommentRepository(MusicLibraryDataContext context)
        {
            _context = context;
        }
        public Task AddAsync(Comment entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Comment>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Comment> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Comment entity)
        {
            throw new NotImplementedException();
        }
    }
}
