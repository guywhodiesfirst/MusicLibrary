using Business.Models;

namespace Business.Interfaces
{
    public interface IPlaylistService : IService<PlaylistDto>
    {
        Task AddAsync(PlaylistDto model);
        Task AddAlbumToPlaylistByIdAsync(Guid albumId, Guid playlistId);
        Task RemoveAlbumFromPlaylistByIdAsync(Guid albumId, Guid playlistId);
    }
}