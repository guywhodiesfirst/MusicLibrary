using Business.Interfaces;
using Business.Models.Reviews;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
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
    }
}