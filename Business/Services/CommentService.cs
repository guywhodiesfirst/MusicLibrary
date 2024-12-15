using AutoMapper;
using Business.Exceptions;
using Business.Interfaces;
using Business.Models.Comments;
using Business.Models.Playlists;
using Data.Entities;
using Data.Interfaces;
using System.Xml.Linq;

namespace Business.Services
{
    public class CommentService : ICommentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public CommentService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task AddAsync(CommentDto model)
        {
            var review = await _unitOfWork.ReviewRepository.GetByIdAsync(model.ReviewId);
            if (review == null)
                throw new MusicLibraryException("Review does not exist");

            var user = await _unitOfWork.UserRepository.GetByIdAsync(model.UserId);
            if (user == null)
                throw new MusicLibraryException("User does not exist");

            model.Id = Guid.NewGuid();
            var comment = _mapper.Map<Comment>(model);
            comment.CreatedAt = DateTime.Now;
            comment.User = user;
            comment.Review = review;
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
            var comments = await _unitOfWork.CommentRepository.GetAllAsync();
            return comments == null ? Enumerable.Empty<CommentDto>() 
                : _mapper.Map<IEnumerable<CommentDto>>(comments);
        }

        public async Task<CommentDto> GetByIdAsync(Guid id)
        {
            var commentInDb = await _unitOfWork.CommentRepository.GetByIdAsync(id);
            return commentInDb == null ? null 
                : _mapper.Map<CommentDto>(commentInDb);
        }

        public async Task<IEnumerable<CommentDto>> GetAllByReviewIdAsync(Guid reviewId)
        {
            var comments = await _unitOfWork.CommentRepository.GetAllAsync();
            var commentsByReview = comments.Where(c => c.ReviewId == reviewId);
            return commentsByReview == null ? Enumerable.Empty<CommentDto>() 
                : _mapper.Map<IEnumerable<CommentDto>>(commentsByReview);
        }

        public async Task<bool> IsUserCommentOwnerAsync(Guid userId, Guid commentId)
        {
            var comment = await _unitOfWork.CommentRepository.GetByIdAsync(commentId);
            return comment != null && comment.UserId == userId;
        }
    }
}