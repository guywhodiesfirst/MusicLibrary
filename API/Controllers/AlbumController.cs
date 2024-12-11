using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/albums")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _albumService;
        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        // POST: api/albums/{id}
        [HttpPost("{id}")]
        public async Task<IActionResult> AddByMusicBrainzIdAsync(Guid id)
        {
            try
            {
                await _albumService.AddByMusicBrainzIdAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex.Message}");
            }
        }

        // GET: api/albums
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var result = await _albumService.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex.Message}");
            }
        }

        // GET: api/albums/details
        [HttpGet("details")]
        public async Task<IActionResult> GetAllWithDetailsAsync()
        {
            try
            {
                var result = await _albumService.GetAllWithDetailsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex.Message}");
            }
        }

        // GET: api/albums/{id}
        [HttpGet("id")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _albumService.GetByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex.Message}");
            }
        }

        // GET: api/albums/details/{id}
        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetByIdWithDetailsAsync(Guid id)
        {
            try
            {
                var result = await _albumService.GetByIdWithDetailsAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex.Message}");
            }
        }

        // PUT: api/albums/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(AlbumDto album)
        {
            try
            {
                await _albumService.UpdateAsync(album);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex.Message}");
            }
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            try
            {
                await _albumService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex.Message}");
            }
        }
    }
}