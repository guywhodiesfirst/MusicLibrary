using Business.Interfaces;
using Business.Models.Reviews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    // TODO: methods to get reviews by album and by user
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        public ReviewsController(IReviewService reviewService) =>
            _reviewService = reviewService;

        // GET: api/albums/reviews
        [AllowAnonymous]
        [HttpGet("api/reviews")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _reviewService.GetAllAsync();
            return Ok(result);
        }

        // GET: api/reviews/{id}
        [AllowAnonymous]
        [HttpGet("api/reviews/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _reviewService.GetByIdAsync(id);
            return result == null ? NotFound("Review not found") : Ok(result);
        }

        // GET: api/reviews/{id}/details
        [AllowAnonymous]
        [HttpGet("api/reviews/{id}/details")]
        public async Task<IActionResult> GetByIdWithDetails(Guid id)
        {
            var result = await _reviewService.GetByIdWithDetailsAsync(id);
            return result == null ? NotFound("Review not found") : Ok(result);
        }

        // POST: "api/albums/{albumId}/reviews"
        [Authorize]
        [HttpPost("api/albums/{albumId}/reviews")]
        public async Task<IActionResult> Add(Guid albumId, [FromBody] ReviewCreateDto model)
        {
            var userId = GetCurrentUserId();
            if (userId == Guid.Empty)
            {
                return Unauthorized();
            }
            model.AlbumId = albumId;
            model.UserId = userId;
            try
            {
                await _reviewService.AddAsync(model);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/reviews/{reviewId}
        [Authorize]
        [HttpDelete("api/reviews/{reviewId}")]
        public async Task<IActionResult> Delete(Guid reviewId)
        {
            var userId = GetCurrentUserId();
            if (userId == Guid.Empty)
                return Unauthorized();
            
            if(!await _reviewService.IsUserReviewOwnerAsync(userId, reviewId))
                return Forbid();
            
            try
            {
                await _reviewService.DeleteAsync(reviewId);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT: api/reviews/{id}
        [Authorize]
        [HttpPut("api/reviews/{reviewId}")]
        public async Task<IActionResult> Update(Guid reviewId, ReviewUpdateDto model)
        {
            var userId = GetCurrentUserId();
            if (userId == Guid.Empty)
                return Unauthorized();

            if (!await _reviewService.IsUserReviewOwnerAsync(userId, reviewId))
                return Forbid();

            model.Id = reviewId;
            try
            {
                await _reviewService.UpdateAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/reviews/{reviewId}/reactions
        [Authorize]
        [HttpPost("api/reviews/{reviewId}/reactions")]
        public async Task<IActionResult> AddReaction(Guid reviewId, [FromBody] ReviewReactionDto model)
        {
            var userId = GetCurrentUserId();
            if (userId == Guid.Empty)
                return Unauthorized();

            model.ReviewId = reviewId;
            model.UserId = userId;
            try
            {
                await _reviewService.AddReactionAsync(model);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/reviews/reactions/{reactionId}
        [Authorize]
        [HttpPut("api/reviews/reactions/{reactionId}")]
        public async Task<IActionResult> UpdateReaction(Guid reactionId)
        {
            var userId = GetCurrentUserId();
            if (userId == Guid.Empty)
                return Unauthorized();

            if (!await _reviewService.IsUserReactionOwnerAsync(userId, reactionId))
                return Forbid();

            try
            {
                await _reviewService.UpdateReactionAsync(reactionId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/reviews/reactions/{reactionId}
        [Authorize]
        [HttpDelete("api/reviews/reactions/{reactionId}")]
        public async Task<IActionResult> DeleteReaction(Guid reactionId)
        {
            var userId = GetCurrentUserId();
            if (userId == Guid.Empty)
                return Unauthorized();

            if (!await _reviewService.IsUserReactionOwnerAsync(userId, reactionId))
                return Forbid();

            try
            {
                await _reviewService.DeleteReactionAsync(reactionId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private Guid GetCurrentUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return userId == null ? Guid.Empty : Guid.Parse(userId);
        }
    }
}