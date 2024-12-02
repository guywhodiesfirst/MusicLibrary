using Data.Entities;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class PlaylistRepository : IPlaylistRepository
    {
        public Task AddAsync(Playlist entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Playlist>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Playlist>> GetAllWithDetailsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Playlist> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Playlist> GetByIdWithDetailsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Playlist entity)
        {
            throw new NotImplementedException();
        }
    }
}
