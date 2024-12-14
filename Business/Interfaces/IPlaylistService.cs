﻿using Business.Models.Playlists;

namespace Business.Interfaces
{
    public interface IPlaylistService : IService<PlaylistDto>
    {
        Task AddAsync(PlaylistDto model);
        Task AddAlbumToPlaylistByIdAsync(Guid albumId, Guid playlistId);
        Task RemoveAlbumFromPlaylistByIdAsync(Guid albumId, Guid playlistId);
        Task UpdateAsync(PlaylistDto model);
        Task<PlaylistDetailsDto> GetByIdWithDetailsAsync(Guid id);
    }
}