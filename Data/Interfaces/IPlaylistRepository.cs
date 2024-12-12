using Data.Entities;

namespace Data.Interfaces
{
    public interface IPlaylistRepository : IRepository<Playlist>
    {
        Task<IEnumerable<Playlist>> GetAllWithDetailsAsync();
        Task<Playlist> GetByIdWithDetailsAsync(Guid id);
        Task AddAlbumToPlaylistByIdAsync(Guid albumId, Guid playlistId);
        Task RemoveAlbumFromPlaylistByIdAsync(Guid albumId, Guid playlistId);
        Task<bool> DoesPlaylistContainAlbumAsync(Guid albumId, Guid playlistId);
        Task DeleteConnectionsByPlaylistIdAsync(Guid playlistId);
    }
}