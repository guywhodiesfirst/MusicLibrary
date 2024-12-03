using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly MusicLibraryDataContext _context;
        public PlaylistRepository(MusicLibraryDataContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Playlist entity)
        {
            await _context.Playlists.AddAsync(entity);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var playlist = await _context.Playlists.FindAsync(id);
            if (playlist != null)
            {
                _context.Playlists.Remove(playlist);
            }
        }

        public async Task<IEnumerable<Playlist>> GetAllAsync()
        {
            var users = await _context.Playlists.ToListAsync();
            return users;
        }

        public async Task<IEnumerable<Playlist>> GetAllWithDetailsAsync()
        {
            var playlists = await _context.Playlists
                              .Include(p => p.User)
                              .Include(p => p.Albums)
                                .ThenInclude(a => a.Playlists)
                              .ToListAsync();

            return playlists ?? null;
        }

        public async Task<Playlist> GetByIdAsync(Guid id)
        {
            var playlist = await _context.Playlists.FindAsync(id);
            return playlist ?? null;
        }

        public async Task<Playlist> GetByIdWithDetailsAsync(Guid id)
        {
            var playlist = await _context.Playlists
                              .Include(p => p.User)
                              .Include(p => p.Albums)
                                .ThenInclude(a => a.Playlists)
                              .FirstOrDefaultAsync(x => x.Id == id);

            return playlist ?? null;
        }

        public async Task UpdateAsync(Playlist entity)
        {
            if (entity != null)
            {
                var playlist = await _context.Playlists.FindAsync(entity.Id);
                if (playlist != null)
                {
                    playlist.Name = entity.Name;
                    playlist.Description = entity.Description;
                }
            }
        }
    }
}
