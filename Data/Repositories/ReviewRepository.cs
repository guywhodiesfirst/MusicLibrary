using Data.Entities;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        public Task AddAsync(Review entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Review>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Review> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Review entity)
        {
            throw new NotImplementedException();
        }
    }
}
