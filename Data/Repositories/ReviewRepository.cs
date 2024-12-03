using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly MusicLibraryDataContext _context;
        public ReviewRepository(MusicLibraryDataContext context)
        {
            _context = context;
        }
        public Task AddAsync(Review entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
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

        public Task UpdateAsync(Review entity)
        {
            throw new NotImplementedException();
        }
    }
}
