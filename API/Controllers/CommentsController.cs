using Business.Interfaces;
using Business.Models.Comments;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("api/reviews/comments")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _commentService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("api/reviews/comments/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _commentService.GetByIdAsync(id);
            return result == null ? NotFound("Comment not found") : Ok(result);
        }

        [HttpPost("api/reviews/{reviewId}/comments")]
        public async Task<IActionResult> Add(Guid reviewId, [FromBody] CommentDto model)
        {
            model.ReviewId = reviewId;
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
        [HttpDelete("api/reviews/comments/{id}")]
        public async Task<IActionResult> DeleteReaction(Guid id)
        {
            try
            {
                await _commentService.DeleteAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}