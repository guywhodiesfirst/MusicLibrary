using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/albums")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly IAlbumService _albumService;
        public AlbumsController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        // GET: api/albums
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _albumService.GetAllAsync();
            return Ok(result);
        }

        // GET: api/albums/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _albumService.GetByIdAsync(id);
            return result == null ? NotFound("Album not found") : Ok(result);
        }

        // GET: api/albums/{id}/details
        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetByIdWithDetails(Guid id)
        {
            var result = await _albumService.GetByIdWithDetailsAsync(id);
            return result == null ? NotFound("Album not found") : Ok(result);
        }

        // DELETE: api/albums/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _albumService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}