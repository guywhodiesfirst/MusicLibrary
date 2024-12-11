using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;

namespace Business.Services
{
    public class PlaylistService : IPlaylistService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public PlaylistService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Task AddAlbumToPlaylistByIdAsync(Guid albumId, Guid playlistId)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(PlaylistDto model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid modelId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PlaylistDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PlaylistDto> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAlbumFromPlaylistByIdAsync(Guid albumId, Guid playlistId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(PlaylistDto model)
        {
            throw new NotImplementedException();
        }
    }
}