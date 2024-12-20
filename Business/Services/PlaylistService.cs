﻿using AutoMapper;
using Business.Exceptions;
using Business.Interfaces;
using Business.Models.Playlists;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services
{
    public class PlaylistService : IPlaylistService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAlbumService _albumService;
        public PlaylistService(IMapper mapper, IUnitOfWork unitOfWork, IAlbumService albumService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _albumService = albumService;
        }
        public async Task AddAlbumToPlaylistByIdAsync(Guid albumId, Guid playlistId)
        {
            bool playlistContainsAlbum = await DoesPlaylistContainAlbum(albumId, playlistId);
            if (playlistContainsAlbum)
                throw new MusicLibraryException("Playlist already contains album");

            bool playlistExists = await _unitOfWork.PlaylistRepository.GetByIdAsync(playlistId) != null;
            if (!playlistExists)
                throw new MusicLibraryException("Playlist does not exist");

            bool albumExists = await _unitOfWork.AlbumRepository.GetByIdAsync(albumId) != null;
            if(!albumExists)
            {
                try
                {
                    await _albumService.AddByMusicBrainzIdAsync(albumId);
                }
                catch
                {
                    throw;
                }
            }

            await _unitOfWork.PlaylistRepository.AddAlbumToPlaylistByIdAsync(albumId, playlistId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task AddAsync(PlaylistCreateDto model)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(model.UserId);
            if (user == null)
                throw new ArgumentNullException("User not found");
            
            var playlist = _mapper.Map<Playlist>(model);
            playlist.Id = Guid.NewGuid();
            playlist.CreatedAt = DateTime.Now;
            await _unitOfWork.PlaylistRepository.AddAsync(playlist);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid modelId)
        {
            var playlistInDb = _unitOfWork.PlaylistRepository.GetByIdAsync(modelId);
            if (playlistInDb == null)
                throw new MusicLibraryException("Playlist does not exist");

            await _unitOfWork.PlaylistRepository.DeleteConnectionsByPlaylistIdAsync(modelId);
            await _unitOfWork.PlaylistRepository.DeleteByIdAsync(modelId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<PlaylistDto>> GetAllAsync()
        {
            var playlists = await _unitOfWork.PlaylistRepository.GetAllWithDetailsAsync();
            return playlists == null ? Enumerable.Empty<PlaylistDto>() 
                : _mapper.Map<IEnumerable<PlaylistDto>>(playlists.OrderBy(p => p.CreatedAt));
        }

        public async Task<IEnumerable<PlaylistDto>> GetAllByUserIdAsync(Guid userId)
        {
            var playlists = await _unitOfWork.PlaylistRepository.GetAllWithDetailsAsync();
            var playlistsByUser = playlists.Where(p => p.UserId == userId);
            return playlistsByUser == null ? Enumerable.Empty<PlaylistDto>()
                : _mapper.Map<IEnumerable<PlaylistDto>>(playlistsByUser);
        }

        public async Task<PlaylistDto> GetByIdAsync(Guid id)
        {
            var playlistInDb = await _unitOfWork.PlaylistRepository.GetByIdWithDetailsAsync(id);
            return playlistInDb == null ? null
                : _mapper.Map<PlaylistDto>(playlistInDb);
        }

        public async Task<PlaylistDetailsDto> GetByIdWithDetailsAsync(Guid id)
        {
            var playlistInDb = await _unitOfWork.PlaylistRepository.GetByIdWithDetailsAsync(id);
            return playlistInDb == null ? null
                : _mapper.Map<PlaylistDetailsDto>(playlistInDb);
        }

        public async Task<bool> IsUserPlaylistOwnerAsync(Guid userId, Guid playlistId)
        {
            var playlist = await _unitOfWork.PlaylistRepository.GetByIdAsync(playlistId);
            return playlist != null && playlist.UserId == userId;
        }

        public async Task RemoveAlbumFromPlaylistByIdAsync(Guid albumId, Guid playlistId)
        {
            bool playlistContainsAlbum = await DoesPlaylistContainAlbum(albumId, playlistId);
            if (!playlistContainsAlbum)
                throw new MusicLibraryException("Playlist does not contain album");

            await _unitOfWork.PlaylistRepository.RemoveAlbumFromPlaylistByIdAsync(albumId, playlistId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(PlaylistUpdateDto model)
        {
            if (model == null)
                throw new ArgumentNullException("Model can't be null");
            var playlistInDb = await _unitOfWork.PlaylistRepository.GetByIdAsync(model.Id);
            if (playlistInDb == null)
                throw new MusicLibraryException("Playlist not found");
            var playlist = _mapper.Map<Playlist>(model);
            await _unitOfWork.PlaylistRepository.UpdateAsync(playlist);
            await _unitOfWork.SaveChangesAsync();
        }

        private async Task<bool> DoesPlaylistContainAlbum(Guid albumId, Guid playlistId)
        {
            var connection = await _unitOfWork.PlaylistRepository
                .GetAlbumPlaylistConnectionAsync(albumId, playlistId);

            return connection != null;
        }
    }
}