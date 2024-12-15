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
            review.Album = album;

            await _unitOfWork.ReviewRepository.AddAsync(review);
            await _albumService.UpdateAlbumRatingByIdAsync(review.AlbumId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task AddReactionAsync(ReviewReactionDto reactionModel)
        {
            await ValidateReactionAsync(reactionModel.UserId, reactionModel.ReviewId);
            var reactionInDb = await _unitOfWork.ReviewReactionRepository
                .GetByUserReviewIdAsync(reactionModel.UserId, reactionModel.ReviewId);
            if (reactionInDb != null)
                throw new MusicLibraryException("Reaction already added");

            var reaction = _mapper.Map<ReviewReaction>(reactionModel);
            reaction.Id = Guid.NewGuid();
            await _unitOfWork.ReviewReactionRepository.AddAsync(reaction);

            var review = await _unitOfWork.ReviewRepository.GetByIdWithDetailsAsync(reactionModel.ReviewId);
            await UpdateReviewLikesDislikes(review);

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

        public async Task DeleteReactionAsync(Guid reactionId)
        {
            var reactionInDb = await _unitOfWork.ReviewReactionRepository
                .GetByIdAsync(reactionId);
            if (reactionInDb == null)
                throw new MusicLibraryException("Reaction not found");

            var review = reactionInDb.Review;
            await _unitOfWork.ReviewReactionRepository.DeleteByIdAsync(reactionId);
            await UpdateReviewLikesDislikes(review);
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
                _mapper.Map<IEnumerable<ReviewDto>>(reviews.OrderByDescending(r => r.LastUpdatedAt));
        }

        public async Task<ReviewDto> GetByIdAsync(Guid id)
        {
            var reviewInDb = await _unitOfWork.ReviewRepository.GetByIdAsync(id);
            return reviewInDb == null ? null : _mapper.Map<ReviewDto>(reviewInDb);
        }

        public async Task<ReviewDetailsDto> GetByIdWithDetailsAsync(Guid modelId)
        {
            var reviewInDb = await _unitOfWork.ReviewRepository.GetByIdWithDetailsAsync(modelId);
            return reviewInDb == null ? null 
                : _mapper.Map<ReviewDetailsDto>(reviewInDb);
        }

        public async Task<IEnumerable<ReviewDto>> GetAllByAlbumIdAsync(Guid albumId)
        {
            var reviews = await _unitOfWork.ReviewRepository.GetAllWithDetailsAsync();
            var reviewsByAlbum = reviews.Where(r => r.AlbumId == albumId);
            return reviewsByAlbum == null ? Enumerable.Empty<ReviewDto>() 
                : _mapper.Map<IEnumerable<ReviewDto>>(reviewsByAlbum.OrderByDescending(r => r.LastUpdatedAt));
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

        public async Task UpdateReactionAsync(Guid reactionId)
        {
            var reactionInDb = await _unitOfWork.ReviewReactionRepository
                .GetByIdAsync(reactionId);
            if (reactionInDb == null)
                throw new MusicLibraryException("Reaction not found");

            reactionInDb.IsLike = !reactionInDb.IsLike;

            var review = await _unitOfWork.ReviewRepository.GetByIdWithDetailsAsync(reactionInDb.ReviewId);
            await UpdateReviewLikesDislikes(review);

            await _unitOfWork.SaveChangesAsync();
        }

        private async Task UpdateReviewLikesDislikes(Review review)
        {
            review.Likes = review.Reactions.Where(rr => rr.IsLike == true).Count();
            review.Dislikes = review.Reactions.Where(rr => rr.IsLike == false).Count();
            await _unitOfWork.ReviewRepository.UpdateAsync(review);
        }

        private async Task<bool> DoesReviewExistByUserAlbumIdAsync(Guid userId, Guid albumId)
        {
            var reviewInDb = await _unitOfWork.ReviewRepository.GetByUserAlbumIdAsync(userId, albumId);
            return reviewInDb != null;
        }

        private async Task ValidateReactionAsync(Guid userId, Guid reviewId)
        {
            var review = await _unitOfWork.ReviewRepository.GetByIdAsync(reviewId);
            if (review == null)
                throw new MusicLibraryException("Review not found");

            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            if (user == null)
                throw new MusicLibraryException("User not found");
        }

        public async Task<bool> IsUserReviewOwnerAsync(Guid userId, Guid reviewId)
        {
            var review = await _unitOfWork.ReviewRepository.GetByIdAsync(reviewId);
            return review != null && review.UserId == userId;
        }

        public async Task<bool> IsUserReactionOwnerAsync(Guid userId, Guid reactionId)
        {
            var reaction = await _unitOfWork.ReviewReactionRepository.GetByIdAsync(reactionId);
            return reaction != null && reaction.UserId == userId;
        }
    }
}