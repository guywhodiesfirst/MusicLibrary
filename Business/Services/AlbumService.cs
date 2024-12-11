using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMusicBrainzQueryService _musicBrainzQueryService;
        private readonly IMapper _mapper;
        public AlbumService(IUnitOfWork unitOfWork, IMapper mapper, IMusicBrainzQueryService musicBrainzQueryService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _musicBrainzQueryService = musicBrainzQueryService;
        }
        public Task AddAsync(AlbumDto model)
        {
            throw new NotImplementedException();
        }

        public async Task AddByMusicBrainzIdAsync(Guid id)
        {
            var albumInDb = await _unitOfWork.AlbumRepository.GetByIdAsync(id);
            if (albumInDb == null)
            {
                try
                {
                    var musicBrainzAlbum = await _musicBrainzQueryService.GetAlbumByIdAsync(id);
                    var album = _mapper.Map<Album>(musicBrainzAlbum);
                    album.AverageRating = 0;
                    await _unitOfWork.AlbumRepository.AddAsync(album);
                }
                // TODO: implement own exception
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public Task DeleteAsync(Guid modelId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AlbumDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AlbumDetailsDto>> GetAllWithDetailsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AlbumDto> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<AlbumDetailsDto> GetByIdWithDetailsAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(AlbumDto model)
        {
            throw new NotImplementedException();
        }
    }
}