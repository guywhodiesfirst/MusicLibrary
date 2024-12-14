using Business.Interfaces;
using Business.Models.Reviews;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // TODO: methods to get reviews by album and by user
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // GET: api/albums/reviews
        [HttpGet("api/reviews")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _reviewService.GetAllAsync();
            return Ok(result);
        }

        // GET: api/reviews/{id}
        [HttpGet("api/reviews/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _reviewService.GetByIdAsync(id);
            return result == null ? NotFound("Review not found") : Ok(result);
        }

        // GET: api/reviews/{id}/details
        [HttpGet("api/reviews/{id}/details")]
        public async Task<IActionResult> GetByIdWithDetails(Guid id)
        {
            var result = await _reviewService.GetByIdWithDetailsAsync(id);
            return result == null ? NotFound("Review not found") : Ok(result);
        }

        // POST: "api/albums/{albumId}/reviews"
        [HttpPost("api/albums/{albumId}/reviews")]
        public async Task<IActionResult> Add(Guid albumId, [FromBody] ReviewCreateDto model)
        {
            model.AlbumId = albumId;
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

        // DELETE: api/reviews/{id}
        [HttpDelete("api/reviews/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _reviewService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT: api/reviews/{id}
        [HttpPut("api/reviews/{id}")]
        public async Task<IActionResult> Update(Guid id, ReviewUpdateDto model)
        {
            model.Id = id;
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
        [HttpPost("api/reviews/{reviewId}/reactions")]
        public async Task<IActionResult> AddReaction(Guid reviewId, [FromBody] ReviewReactionDto model)
        {
            model.ReviewId = reviewId;
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
        [HttpPut("api/reviews/reactions/{reactionId}")]
        public async Task<IActionResult> UpdateReaction(Guid reactionId)
        {
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
        [HttpDelete("api/reviews/reactions/{reactionId}")]
        public async Task<IActionResult> DeleteReaction(Guid reactionId)
        {
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