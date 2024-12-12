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
        public ReviewService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task AddAsync(ReviewDto model)
        {
            bool reviewExists = await _unitOfWork.ReviewRepository.DoesReviewExistAsync(model.UserId, model.AlbumId);
            if (reviewExists)
                throw new MusicLibraryException("Review already exists!");

            model.Id = Guid.NewGuid();
            var review = _mapper.Map<Review>(model);
            await _unitOfWork.ReviewRepository.AddAsync(review);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid modelId)
        {
            var reviewInDb = _unitOfWork.ReviewRepository.GetByIdAsync(modelId);
            if (reviewInDb == null)
                throw new MusicLibraryException("Review does not exist");

            await _unitOfWork.ReviewRepository.DeleteByIdAsync(modelId);
            await _unitOfWork.SaveChangesAsync();
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
    }
}