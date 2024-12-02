using Data.Entities;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        public Task AddAsync(Album entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Album>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Album>> GetAllWithDetailsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Album> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Album> GetByIdWithDetailsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Album entity)
        {
            throw new NotImplementedException();
        }
    }
}
