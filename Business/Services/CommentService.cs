using AutoMapper;
using Business.Exceptions;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services
{
    public class CommentService : ICommentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReviewService _reviewService;
        private readonly IUserService _userService;
        public CommentService(IMapper mapper, IUnitOfWork unitOfWork, IReviewService reviewService, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _reviewService = reviewService;
            _userService = userService;
        }
        public async Task AddAsync(CommentDto model)
        {
            bool reviewExists = await _reviewService.DoesReviewExistByIdAsync(model.Id);
            if (!reviewExists)
                throw new MusicLibraryException("Review does not exist");

            bool userExists = await _userService.DoesUserExistByIdAsync(model.Id);
            if (!userExists)
                throw new MusicLibraryException("User does not exist");

            model.Id = Guid.NewGuid();
            var comment = _mapper.Map<Comment>(model);
            await _unitOfWork.CommentRepository.AddAsync(comment);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid modelId)
        {
            bool commentExists = await _unitOfWork.CommentRepository.GetByIdAsync(modelId) != null;
            if (!commentExists)
                throw new MusicLibraryException("Comment does not exist");
            
            await _unitOfWork.CommentRepository.DeleteByIdAsync(modelId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<CommentDto>> GetAllAsync()
        {
            var comments = await _unitOfWork.ReviewRepository.GetAllAsync();
            return comments == null ? Enumerable.Empty<CommentDto>() : _mapper.Map<IEnumerable<CommentDto>>(comments);
        }

        public async Task<CommentDto> GetByIdAsync(Guid id)
        {
            var commentInDb = await _unitOfWork.CommentRepository.GetByIdAsync(id);
            return commentInDb == null ? throw new MusicLibraryException("Comment not found") : _mapper.Map<CommentDto>(commentInDb);
        }
    }
}