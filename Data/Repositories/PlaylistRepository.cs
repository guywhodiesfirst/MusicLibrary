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

        public async Task AddAlbumToPlaylistByIdAsync(Guid albumId, Guid playlistId)
        {
            var connection = new AlbumPlaylist
            {
                AlbumId = albumId,
                PlaylistId = playlistId
            };
            await _context.AlbumPlaylists.AddAsync(connection);
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

        public async Task DeleteConnectionsByPlaylistIdAsync(Guid playlistId)
        {
            var connections = await GetConnectionsByPlaylistIdAsync(playlistId);
            if (connections != null)
                _context.AlbumPlaylists.RemoveRange(connections);
        }

        private async Task<IEnumerable<AlbumPlaylist>> GetConnectionsByPlaylistIdAsync(Guid playlistId)
        {
            var connections = await _context.AlbumPlaylists.ToListAsync();
            var connectionsWithAlbum = connections.Where(c => c.PlaylistId == playlistId);
            return connectionsWithAlbum ?? null;
        }

        public async Task<IEnumerable<Playlist>> GetAllAsync()
        {
            var playlists = await _context.Playlists.ToListAsync();
            return playlists ?? null;
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

        public async Task RemoveAlbumFromPlaylistByIdAsync(Guid albumId, Guid playlistId)
        {
            var connection = await _context.AlbumPlaylists
                .FirstOrDefaultAsync(p => p.PlaylistId == playlistId && p.AlbumId == albumId);
            if(connection != null)
                _context.AlbumPlaylists.Remove(connection);
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

        public async Task<AlbumPlaylist> GetAlbumPlaylistConnectionAsync(Guid albumId, Guid playlistId)
        {
            var connection = await _context.AlbumPlaylists
                .FirstOrDefaultAsync(p => p.PlaylistId == playlistId && p.AlbumId == albumId);

            return connection ?? null;
        }
    }
}