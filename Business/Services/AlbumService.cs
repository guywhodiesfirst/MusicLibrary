using AutoMapper;
using Business.Exceptions;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMusicBrainzQueryService _musicBrainzQueryService;
        public AlbumService(IMapper mapper, IUnitOfWork unitOfWork, IMusicBrainzQueryService musicBrainzQueryService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _musicBrainzQueryService = musicBrainzQueryService;
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
                catch (Exception ex)
                {
                    throw new MusicLibraryException("Error while trying to add an album: ", ex);
                }
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid modelId)
        {
            var albumInDb = await _unitOfWork.AlbumRepository.GetByIdAsync(modelId);
            if (albumInDb == null)
                throw new MusicLibraryException("Album is not present in the database");

            await _unitOfWork.AlbumRepository.DeleteConnectionsByAlbumIdAsync(modelId);
            await _unitOfWork.AlbumRepository.DeleteByIdAsync(modelId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<AlbumDto>> GetAllAsync()
        {
            var albums = await _unitOfWork.AlbumRepository.GetAllAsync();
            return albums == null ? Enumerable.Empty<AlbumDto>() : _mapper.Map<IEnumerable<AlbumDto>>(albums);
        }

        public async Task<IEnumerable<AlbumDetailsDto>> GetAllWithDetailsAsync()
        {
            var albums = await _unitOfWork.AlbumRepository.GetAllWithDetailsAsync();
            return albums == null ? Enumerable.Empty<AlbumDetailsDto>() : _mapper.Map<IEnumerable<AlbumDetailsDto>>(albums);
        }

        public async Task<AlbumDto> GetByIdAsync(Guid id)
        {
            var albumInDb = await _unitOfWork.AlbumRepository.GetByIdAsync(id);
            return albumInDb == null ? throw new MusicLibraryException("Album not found") : _mapper.Map<AlbumDto>(albumInDb);
        }

        public async Task<AlbumDetailsDto> GetByIdWithDetailsAsync(Guid id)
        {
            var albumInDb = await _unitOfWork.AlbumRepository.GetByIdWithDetailsAsync(id);
            return albumInDb == null ? throw new MusicLibraryException("Album not found") : _mapper.Map<AlbumDetailsDto>(albumInDb);
        }

        public async Task UpdateAsync(AlbumDto model)
        {
            if (model == null)
                throw new ArgumentNullException("Model can't be null");
            var albumInDb = await _unitOfWork.AlbumRepository.GetByIdAsync(model.Id);
            if(albumInDb == null)
                throw new MusicLibraryException("Album not found");
            var album = _mapper.Map<Album>(model);
            await _unitOfWork.AlbumRepository.UpdateAsync(album);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}