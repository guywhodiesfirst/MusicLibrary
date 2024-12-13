using AutoMapper;
using Business.Exceptions;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAlbumService _albumService;
        public ReviewService(IMapper mapper, IUnitOfWork unitOfWork, IAlbumService albumService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _albumService = albumService;
        }
        public async Task AddAsync(ReviewDto model)
        {
            bool reviewExists = await DoesReviewExistByUserAlbumIdAsync(model.UserId, model.AlbumId);
            if (reviewExists)
                throw new MusicLibraryException("Review already exists!");

            bool albumExists = await _unitOfWork.AlbumRepository.GetByIdAsync(model.AlbumId) != null;
            if (!albumExists)
            {
                try
                {
                    await _albumService.AddByMusicBrainzIdAsync(model.AlbumId);
                }
                catch
                {
                    throw;
                }
            }

            model.Id = Guid.NewGuid();
            var review = _mapper.Map<Review>(model);
            await _unitOfWork.ReviewRepository.AddAsync(review);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid modelId)
        {
            bool reviewExists = await _unitOfWork.ReviewRepository.GetByIdAsync(modelId) != null;
            if (!reviewExists)
                throw new MusicLibraryException("Review does not exist");

            await _unitOfWork.ReviewRepository.DeleteByIdAsync(modelId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DoesReviewExistByIdAsync(Guid modelId)
        {
            var review = await _unitOfWork.ReviewRepository.GetByIdAsync(modelId);
            return review != null;
        }

        public async Task<IEnumerable<ReviewDto>> GetAllAsync()
        {
            var reviews = await _unitOfWork.ReviewRepository.GetAllAsync();
            return reviews == null ? Enumerable.Empty<ReviewDto>() : _mapper.Map<IEnumerable<ReviewDto>>(reviews);
        }

        public async Task<ReviewDto> GetByIdAsync(Guid id)
        {
            var reviewInDb = await _unitOfWork.ReviewRepository.GetByIdAsync(id);
            return reviewInDb == null ? throw new MusicLibraryException("Review not found") : _mapper.Map<ReviewDto>(reviewInDb);
        }

        public async Task UpdateAsync(ReviewDto model)
        {
            if (model == null)
                throw new ArgumentNullException("Model can't be null");
            var userInDb = await _unitOfWork.ReviewRepository.GetByIdAsync(model.Id);
            if (userInDb == null)
                throw new MusicLibraryException("User not found");
            var review = _mapper.Map<Review>(model);
            await _unitOfWork.ReviewRepository.UpdateAsync(review);
            await _unitOfWork.SaveChangesAsync();
        }

        private async Task<bool> DoesReviewExistByUserAlbumIdAsync(Guid userId, Guid albumId)
        {
            var reviewInDb = await _unitOfWork.ReviewRepository.GetByUserAlbumIdAsync(userId, albumId);
            return reviewInDb != null;
        }
    }
}