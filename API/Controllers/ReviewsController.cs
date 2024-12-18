using API.Interfaces;
using Business.Interfaces;
using Business.Models.Reviews;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly IControllerHelper _controllerHelper;
        public ReviewsController(IReviewService reviewService, IControllerHelper controllerHelper)
        {
            _reviewService = reviewService;
            _controllerHelper = controllerHelper;
        }

        // GET: api/reviews
        [AllowAnonymous]
        [HttpGet("api/reviews")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _reviewService.GetAllAsync();
            return Ok(result);
        }

        // GET: api/albums/{albumId}/reviews
        [AllowAnonymous]
        [HttpGet("api/albums/{albumId}/reviews")]
        public async Task<IActionResult> GetAllByAlbumId(Guid albumId)
        {
            var result = await _reviewService.GetAllByAlbumIdAsync(albumId);
            return Ok(new {success = true, reviews = result});
        }

        // GET: api/reviews/{id}
        [AllowAnonymous]
        [HttpGet("api/reviews/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _reviewService.GetByIdAsync(id);
            return result == null ? NotFound("Review not found") : Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("api/reviews/by-album-user")]
        public async Task<IActionResult> GetByAlbumUserId([FromQuery] Guid albumId, [FromQuery] Guid userId)
        {
            var result = await _reviewService.GetByAlbumUserIdAsync(albumId, userId);
            return result == null ? NotFound() : Ok(result);
        }

        // GET: api/reviews/{id}/details
        [AllowAnonymous]
        [HttpGet("api/reviews/{id}/details")]
        public async Task<IActionResult> GetByIdWithDetails(Guid id)
        {
            var result = await _reviewService.GetByIdWithDetailsAsync(id);
            return result == null ? NotFound() 
                : Ok(result);
        }

        // POST: "api/albums/{albumId}/reviews"
        [Authorize]
        [HttpPost("api/albums/{albumId}/reviews")]
        public async Task<IActionResult> Add(Guid albumId, [FromBody] ReviewCreateDto model)
        {
            var currentUserId = _controllerHelper.GetCurrentUserId();
            if (currentUserId == Guid.Empty)
            {
                return Unauthorized();
            }
            model.AlbumId = albumId;
            model.UserId = currentUserId;
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
            var currentUserId = _controllerHelper.GetCurrentUserId();
            if (currentUserId == Guid.Empty)
                return Unauthorized();
            
            if(!await _reviewService.IsUserReviewOwnerAsync(currentUserId, reviewId))
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
            var currentUserId = _controllerHelper.GetCurrentUserId();
            if (currentUserId == Guid.Empty)
                return Unauthorized();

            if (!await _reviewService.IsUserReviewOwnerAsync(currentUserId, reviewId))
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
            var currentUserId = _controllerHelper.GetCurrentUserId();
            if (currentUserId == Guid.Empty)
                return Unauthorized();

            model.ReviewId = reviewId;
            model.UserId = currentUserId;
            try
            {
                await _reviewService.AddReactionAsync(model);

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("api/reviews/reactions/by-review-user")]
        public async Task<IActionResult> GetReaction([FromQuery] Guid reviewId, [FromQuery] Guid userId)
        {
            var result = await _reviewService.GetReactionByReviewUserIdAsync(reviewId, userId);
            return result == null ? NotFound() : Ok(result);
        }

        // PUT: api/reviews/reactions/{reactionId}
        [Authorize]
        [HttpPut("api/reviews/reactions/{reactionId}")]
        public async Task<IActionResult> UpdateReaction(Guid reactionId)
        {
            var currentUserId = _controllerHelper.GetCurrentUserId();
            if (currentUserId == Guid.Empty)
                return Unauthorized();

            if (!await _reviewService.IsUserReactionOwnerAsync(currentUserId, reactionId))
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
            var currentUserId = _controllerHelper.GetCurrentUserId();
            if (currentUserId == Guid.Empty)
                return Unauthorized();

            if (!await _reviewService.IsUserReactionOwnerAsync(currentUserId, reactionId))
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
    }
}