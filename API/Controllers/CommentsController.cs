using API.Interfaces;
using Business.Interfaces;
using Business.Models.Comments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IControllerHelper _controllerHelper;
        public CommentsController(ICommentService commentService, IControllerHelper controllerHelper)
        {
            _commentService = commentService;
            _controllerHelper = controllerHelper;
        }

        [HttpGet("api/reviews/comments")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _commentService.GetAllAsync();
            return Ok(result);
        }

        // GET: api/reviews/{reviewId}/comments
        [AllowAnonymous]
        [HttpGet("api/reviews/{reviewId}/comments")]
        public async Task<IActionResult> GetAllByAlbumId(Guid reviewId)
        {
            var result = await _commentService.GetAllByReviewIdAsync(reviewId);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("api/reviews/comments/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _commentService.GetByIdAsync(id);
            return result == null ? NotFound("Comment not found") : Ok(result);
        }

        [Authorize]
        [HttpPost("api/reviews/{reviewId}/comments")]
        public async Task<IActionResult> Add(Guid reviewId, [FromBody] CommentDto model)
        {
            var currentUserId = _controllerHelper.GetCurrentUserId();
            if (currentUserId == Guid.Empty)
                return Unauthorized();

            model.ReviewId = reviewId;
            model.UserId = currentUserId;
            try
            {
                await _commentService.AddAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("api/reviews/comments/{commentId}")]
        public async Task<IActionResult> DeleteReaction(Guid commentId)
        {
            var currentUserId = _controllerHelper.GetCurrentUserId();
            if (currentUserId == Guid.Empty)
                return Unauthorized();

            if (!await _commentService.IsUserCommentOwnerAsync(currentUserId, commentId))
                return Forbid();

            try
            {
                await _commentService.DeleteAsync(commentId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}