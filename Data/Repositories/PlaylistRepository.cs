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
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly MusicLibraryDataContext _context;
        public PlaylistRepository(MusicLibraryDataContext context)
        {
            _context = context;
        }
        public Task AddAsync(Playlist entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Playlist>> GetAllAsync()
        {
            var users = await _context.Playlists.ToListAsync();
            return users;
        }

        public Task<IEnumerable<Playlist>> GetAllWithDetailsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Playlist> GetByIdAsync(Guid id)
        {
            var playlist = await _context.Playlists.FindAsync(id);
            return playlist ?? null;
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
