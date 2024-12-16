using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class MusicBrainzController : ControllerBase
    {
        private readonly IMusicBrainzQueryService _musicBrainzQueryService;

        public MusicBrainzController(IMusicBrainzQueryService musicBrainzQueryService)
        {
            _musicBrainzQueryService = musicBrainzQueryService;
        }

        // GET: api/MusicBrainz/albums/search
        [AllowAnonymous]
        [HttpGet("albums/search")]
        public async Task<IActionResult> GetAlbumsByName([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest("Query parameter cannot be empty.");
            }

            try
            {
                var result = await _musicBrainzQueryService.GetAlbumsByNameAsync(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex.Message}");
            }
        }

        // GET: api/MusicBrainz/albums/{id}
        [AllowAnonymous]
        [HttpGet("albums/{id}")]
        public async Task<IActionResult> GetAlbumById(Guid id)
        {
            try
            {
                var result = await _musicBrainzQueryService.GetAlbumByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex.Message}");
            }
        }
    }
}