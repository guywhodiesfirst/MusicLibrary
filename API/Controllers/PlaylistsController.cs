using Business.Interfaces;
using Business.Models.Playlists;
using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    public class PlaylistsController : ControllerBase
    {
        private readonly IPlaylistService _playlistService;
        public PlaylistsController(IPlaylistService playlistService)
        {
            _playlistService = playlistService;
        }

        // GET: api/playlists
        [HttpGet("api/playlists")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _playlistService.GetAllAsync();
            return Ok(result);
        }

        // GET: api/playlists/{id}
        [HttpGet("api/playlists/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _playlistService.GetByIdAsync(id);
            return result == null ? NotFound("Playlist not found") : Ok(result);
        }

        // GET: api/playlists/{id}/details
        [HttpGet("api/playlists/{id}/details")]
        public async Task<IActionResult> GetByIdWithDetails(Guid id)
        {
            var result = await _playlistService.GetByIdWithDetailsAsync(id);
            return result == null ? NotFound("Playlist not found") : Ok(result);
        }

        // POST: "api/users/{userId}/playlists"
        [HttpPost("api/users/{userId}/playlists")]
        public async Task<IActionResult> Add(Guid userId, [FromBody] PlaylistCreateDto model)
        {
            model.UserId = userId;
            try
            {
                await _playlistService.AddAsync(model);

                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE: api/playlists/{id}
        [HttpDelete("api/playlists/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _playlistService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/playlists/{playlistId}/albums/{albumId}
        [HttpPost("api/playlists/{playlistId}/albums/{albumId}")]
        public async Task<IActionResult> AddAlbumToPlaylist(Guid albumId, Guid playlistId)
        {
            try
            {
                await _playlistService.AddAlbumToPlaylistByIdAsync(albumId, playlistId);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE: api/playlists/{playlistId}/albums/{albumId}
        [HttpDelete("api/playlists/{playlistId}/albums/{albumId}")]
        public async Task<IActionResult> RemoveAlbumFromPlaylist(Guid albumId, Guid playlistId)
        {
            try
            {
                await _playlistService.RemoveAlbumFromPlaylistByIdAsync(albumId, playlistId);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}