using API.Interfaces;
using Business.Interfaces;
using Business.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IControllerHelper _controllerHelper;
        public UsersController(IUserService userService, IControllerHelper controllerHelper)
        {
            _userService = userService;
            _controllerHelper = controllerHelper;
        }

        // GET: api/users
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAllAsync();
            return Ok(result);
        }

        // GET: api/users/id
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _userService.GetByIdAsync(id);
            return result == null ? NotFound("User not found") : Ok(result);
        }

        // GET: api/users/id/details
        [AllowAnonymous]
        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetByIdWithDetails(Guid id)
        {
            var result = await _userService.GetByIdWithDetailsAsync(id);
            return result == null ? NotFound("User not found") : Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _userService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("{userId}")]
        public async Task<IActionResult> Update(Guid userId, UserDto model)
        {
            var currentUserId = _controllerHelper.GetCurrentUserId();
            if (currentUserId == Guid.Empty)
                return Unauthorized();

            if (currentUserId != userId)
                return Forbid();

            model.Id = currentUserId;

            try
            {
                await _userService.UpdateAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var currentUserId = _controllerHelper.GetCurrentUserId();
            if (currentUserId == Guid.Empty)
                return Unauthorized(new {success = false, message = "User not authenticated"});

            var user = await _userService.GetByIdAsync(currentUserId);
            if (user == null)
                return NotFound(new { success = false, message = "User not found" });

            return Ok(user);
        }
    }
}