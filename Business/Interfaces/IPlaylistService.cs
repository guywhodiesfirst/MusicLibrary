﻿namespace Business.Interfaces
{
    public interface IPlaylistService
    {
        Task AddAlbumToPlaylistById(Guid albumId, Guid playlistId);

    }
}