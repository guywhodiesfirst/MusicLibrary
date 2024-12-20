using Business.Exceptions;
using Business.Interfaces;
using Data.Interfaces;

namespace Business.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task BlockCommentAsync(Guid commentId)
        {
            var commentInDb = await _unitOfWork.CommentRepository.GetByIdAsync(commentId);
            if (commentInDb == null)
                throw new MusicLibraryException("Comment not found");
            if (commentInDb.IsDeleted)
                throw new MusicLibraryException("Comment already blocked");
            commentInDb.IsDeleted= true;
            await _unitOfWork.CommentRepository.UpdateAsync(commentInDb);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task BlockReviewAsync(Guid reviewId)
        {
            var reviewInDb = await _unitOfWork.ReviewRepository.GetByIdAsync(reviewId);
            if (reviewInDb == null)
                throw new MusicLibraryException("Review not found");
            if (reviewInDb.IsDeleted)
                throw new MusicLibraryException("Review already blocked");
            reviewInDb.IsDeleted = true;
            await _unitOfWork.ReviewRepository.UpdateAsync(reviewInDb);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task BlockUserAsync(Guid userId)
        {
            var userInDb = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            if (userInDb == null)
                throw new MusicLibraryException("User not found");
            if (userInDb.IsBlocked)
                throw new MusicLibraryException("User already blocked");
            userInDb.IsBlocked = true;
            await _unitOfWork.UserRepository.UpdateAsync(userInDb);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UnblockCommentAsync(Guid commentId)
        {
            var commentInDb = await _unitOfWork.CommentRepository.GetByIdAsync(commentId);
            if (commentInDb == null)
                throw new MusicLibraryException("Comment not found");
            if (!commentInDb.IsDeleted)
                throw new MusicLibraryException("Comment is not blocked");
            commentInDb.IsDeleted = false;
            await _unitOfWork.CommentRepository.UpdateAsync(commentInDb);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UnblockReviewAsync(Guid reviewId)
        {
            var reviewInDb = await _unitOfWork.ReviewRepository.GetByIdAsync(reviewId);
            if (reviewInDb == null)
                throw new MusicLibraryException("Review not found");
            if (!reviewInDb.IsDeleted)
                throw new MusicLibraryException("Review is not blocked");
            reviewInDb.IsDeleted = false;
            await _unitOfWork.ReviewRepository.UpdateAsync(reviewInDb);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UnblockUserAsync(Guid userId)
        {
            var userInDb = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            if (userInDb == null)
                throw new MusicLibraryException("User not found");
            if (!userInDb.IsBlocked)
                throw new MusicLibraryException("User is not blocked");
            userInDb.IsBlocked = false;
            await _unitOfWork.UserRepository.UpdateAsync(userInDb);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}