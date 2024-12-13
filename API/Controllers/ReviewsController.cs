using Business.Interfaces;
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // GET: api/reviews
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _reviewService.GetAllAsync();
            return Ok(result);
        }

        // GET: api/reviews/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _reviewService.GetByIdAsync(id);
            return result == null ? NotFound("Review not found") : Ok(result);
        }

        // GET: api/reviews/{id}/details
        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetByIdWithDetails(Guid id)
        {
            var result = await _reviewService.GetByIdWithDetailsAsync(id);
            return result == null ? NotFound("Review not found") : Ok(result);
        }

        // POST: api/reviews
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ReviewDto model)
        {
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
        [HttpDelete("{id}")]
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
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(ReviewDto model)
        {
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