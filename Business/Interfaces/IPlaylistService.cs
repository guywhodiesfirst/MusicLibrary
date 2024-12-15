using Business.Models.Playlists;

namespace Business.Interfaces
{
    public interface IPlaylistService : IService<PlaylistDto>
    {
        Task AddAsync(PlaylistCreateDto model);
        Task AddAlbumToPlaylistByIdAsync(Guid albumId, Guid playlistId);
        Task RemoveAlbumFromPlaylistByIdAsync(Guid albumId, Guid playlistId);
        Task UpdateAsync(PlaylistUpdateDto model);
        Task<bool> IsUserPlaylistOwnerAsync(Guid userId, Guid playlistId);
        Task<PlaylistDetailsDto> GetByIdWithDetailsAsync(Guid id);
    }
}