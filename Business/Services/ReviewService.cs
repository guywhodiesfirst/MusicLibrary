using AutoMapper;
using Business.Exceptions;
using Business.Interfaces;
using Business.Models.Reviews;
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
        public async Task AddAsync(ReviewCreateDto model)
        {
            bool reviewExists = await DoesReviewExistByUserAlbumIdAsync(model.UserId, model.AlbumId);
            if (reviewExists)
                throw new MusicLibraryException("Review already exists!");

            var userInDb = await _unitOfWork.UserRepository.GetByIdAsync(model.UserId);
            if (userInDb == null)
                throw new MusicLibraryException("User not found");

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
            var album = await _unitOfWork.AlbumRepository.GetByIdAsync(model.AlbumId);
            var review = _mapper.Map<Review>(model);
            review.Id = Guid.NewGuid();
            review.CreatedAt = DateTime.Now;
            review.LastUpdatedAt = DateTime.Now;
            review.Likes = 0;
            review.Dislikes = 0;
            review.Album = _mapper.Map<Album>(album);

            await _unitOfWork.ReviewRepository.AddAsync(review);
            await _albumService.UpdateAlbumRatingByIdAsync(review.AlbumId);
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
            var reviews = await _unitOfWork.ReviewRepository.GetAllWithDetailsAsync();
            return reviews == null ? Enumerable.Empty<ReviewDto>() : 
                _mapper.Map<IEnumerable<ReviewDto>>(reviews.OrderBy(r => r.LastUpdatedAt));
        }

        public async Task<ReviewDto> GetByIdAsync(Guid id)
        {
            var reviewInDb = await _unitOfWork.ReviewRepository.GetByIdAsync(id);
            return reviewInDb == null ? null : _mapper.Map<ReviewDto>(reviewInDb);
        }

        public async Task<ReviewDetailsDto> GetByIdWithDetailsAsync(Guid modelId)
        {
            var reviewInDb = await _unitOfWork.ReviewRepository.GetByIdWithDetailsAsync(modelId);
            return reviewInDb == null ? null : _mapper.Map<ReviewDetailsDto>(reviewInDb);
        }

        public async Task UpdateAsync(ReviewUpdateDto model)
        {
            if (model == null)
                throw new ArgumentNullException("Model can't be null");

            var review = await _unitOfWork.ReviewRepository.GetByIdAsync(model.Id);
            if (review == null)
                throw new ArgumentNullException("Review does not exist");

            review.LastUpdatedAt = DateTime.UtcNow;
            review.Content = model.Content;
            review.Rating = model.Rating;

            await _unitOfWork.ReviewRepository.UpdateAsync(review);
            await _albumService.UpdateAlbumRatingByIdAsync(review.AlbumId);
            await _unitOfWork.SaveChangesAsync();
        }

        private async Task<bool> DoesReviewExistByUserAlbumIdAsync(Guid userId, Guid albumId)
        {
            var reviewInDb = await _unitOfWork.ReviewRepository.GetByUserAlbumIdAsync(userId, albumId);
            return reviewInDb != null;
        }
    }
}