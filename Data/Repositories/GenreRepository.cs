using Data.Entities;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        public Task AddAsync(Genre entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Genre>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Genre> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Genre entity)
        {
            throw new NotImplementedException();
        }
    }
}
