using API.Interfaces;
using Business.Interfaces;
using Business.Models.Playlists;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // TODO: method to get playlists by user
    [ApiController]
    public class PlaylistsController : ControllerBase
    {
        private readonly IPlaylistService _playlistService;
        private readonly IControllerHelper _controllerHelper;
        public PlaylistsController(IPlaylistService playlistService, IControllerHelper controllerHelper)
        {
            _playlistService = playlistService;
            _controllerHelper = controllerHelper;
        }

        // GET: api/playlists
        [AllowAnonymous]
        [HttpGet("api/playlists")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _playlistService.GetAllAsync();
            return Ok(result);
        }

        // GET: api/playlists/{id}
        [AllowAnonymous]
        [HttpGet("api/playlists/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _playlistService.GetByIdAsync(id);
            return result == null ? NotFound("Playlist not found") : Ok(result);
        }

        // GET: api/playlists/{id}/details
        [AllowAnonymous]
        [HttpGet("api/playlists/{id}/details")]
        public async Task<IActionResult> GetByIdWithDetails(Guid id)
        {
            var result = await _playlistService.GetByIdWithDetailsAsync(id);
            return result == null ? NotFound("Playlist not found") : Ok(result);
        }

        // POST: "api/users/{userId}/playlists"
        [Authorize]
        [HttpPost("api/users/{userId}/playlists")]
        public async Task<IActionResult> Add(Guid userId, [FromBody] PlaylistCreateDto model)
        {
            var currentUserId = _controllerHelper.GetCurrentUserId();
            if (currentUserId == Guid.Empty)
                return Unauthorized();

            if (currentUserId != userId)
                return Forbid();

            model.UserId = currentUserId;
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
        [Authorize]
        [HttpDelete("api/playlists/{playlistId}")]
        public async Task<IActionResult> Delete(Guid playlistId)
        {
            var currentUserId = _controllerHelper.GetCurrentUserId();
            if (currentUserId == Guid.Empty)
                return Unauthorized();

            if (!await _playlistService.IsUserPlaylistOwnerAsync(currentUserId, playlistId))
                return Forbid();

            try
            {
                await _playlistService.DeleteAsync(playlistId);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/playlists/{playlistId}/albums/{albumId}
        [Authorize]
        [HttpPost("api/playlists/{playlistId}/albums/{albumId}")]
        public async Task<IActionResult> AddAlbumToPlaylist(Guid albumId, Guid playlistId)
        {
            var currentUserId = _controllerHelper.GetCurrentUserId();
            if (currentUserId == Guid.Empty)
                return Unauthorized();

            if (!await _playlistService.IsUserPlaylistOwnerAsync(currentUserId, playlistId))
                return Forbid();

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

        // PUT: api/playlists/{playlistId}
        [Authorize]
        [HttpPut("api/playlists/{playlistId}")]
        public async Task<IActionResult> Update(Guid playlistId, PlaylistUpdateDto model)
        {
            var userId = _controllerHelper.GetCurrentUserId();
            if (userId == Guid.Empty)
                return Unauthorized();

            if (!await _playlistService.IsUserPlaylistOwnerAsync(userId, playlistId))
                return Forbid();

            model.Id = playlistId;
            try
            {
                await _playlistService.UpdateAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/playlists/{playlistId}/albums/{albumId}
        [Authorize]
        [HttpDelete("api/playlists/{playlistId}/albums/{albumId}")]
        public async Task<IActionResult> RemoveAlbumFromPlaylist(Guid albumId, Guid playlistId)
        {
            var currentUserId = _controllerHelper.GetCurrentUserId();
            if (currentUserId == Guid.Empty)
                return Unauthorized();

            if (!await _playlistService.IsUserPlaylistOwnerAsync(currentUserId, playlistId))
                return Forbid();

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