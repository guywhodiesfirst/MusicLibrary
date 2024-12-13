using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly MusicLibraryDataContext _context;
        public AlbumRepository(MusicLibraryDataContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Album entity)
        {
            entity.AverageRating = 0;
            await _context.Albums.AddAsync(entity);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var album = await _context.Albums.FindAsync(id);
            if (album != null)
            {
                _context.Albums.Remove(album);
            }
        }

        public async Task DeleteConnectionsByAlbumIdAsync(Guid albumId)
        {
            var connections = await GetConnectionsByAlbumIdAsync(albumId);
            if (connections != null)
                _context.AlbumPlaylists.RemoveRange(connections);
        }

        public async Task<IEnumerable<Album>> GetAllAsync()
        {
            var album = await _context.Albums.ToListAsync();
            return album ?? null;
        }

        public async Task<IEnumerable<Album>> GetAllWithDetailsAsync()
        {
            var albums = await _context.Albums
                              .Include(a => a.Playlists)
                                .ThenInclude(p => p.Albums)
                              .Include(a => a.Reviews)
                              .ToListAsync();

            return albums ?? null;
        }

        public async Task<Album> GetByIdAsync(Guid id)
        {
            var album = await _context.Albums.FindAsync(id);
            return album ?? null;
        }

        public async Task<Album> GetByIdWithDetailsAsync(Guid id)
        {
            var album = await _context.Albums
                              .Include(a => a.Playlists)
                                .ThenInclude(p => p.Albums)
                              .Include(a => a.Reviews)
                              .FirstOrDefaultAsync(a => a.Id == id);
            
            return album ?? null;
        }

        private async Task<IEnumerable<AlbumPlaylist>> GetConnectionsByAlbumIdAsync(Guid albumId)
        {
            var connections = await _context.AlbumPlaylists.ToListAsync();
            var connectionsWithAlbum = connections.Where(c => c.AlbumId == albumId);
            return connectionsWithAlbum ?? null;
        }

        public async Task UpdateAsync(Album entity)
        {
            if (entity != null)
            {
                var album = await _context.Albums.FindAsync(entity.Id);
                if(album != null)
                {
                    album.Name = entity.Name;
                    album.Artists = entity.Artists;
                    album.Genre = entity.Genre;
                    album.ReleaseDate = entity.ReleaseDate;
                }
            }
        }
    }
}