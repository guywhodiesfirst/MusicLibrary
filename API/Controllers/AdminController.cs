using API.Interfaces;
using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IControllerHelper _controllerHelper;

        public AdminController(IAdminService adminService, IControllerHelper controllerHelper)
        {
            _adminService = adminService;
            _controllerHelper = controllerHelper;
        }

        [Authorize]
        [HttpPut("users/{userId}/block")]
        public async Task<IActionResult> BlockUser(Guid userId)
        {
            if (!_controllerHelper.IsCurrentUserAdmin())
                return Forbid();

            try
            {
                await _adminService.BlockUserAsync(userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("reviews/{reviewId}/block")]
        public async Task<IActionResult> BlockReview(Guid reviewId)
        {
            if (!_controllerHelper.IsCurrentUserAdmin())
                return Forbid("User is not admin");

            try
            {
                await _adminService.BlockReviewAsync(reviewId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("comments/{commentId}/block")]
        public async Task<IActionResult> BlockComment(Guid commentId)
        {
            if (!_controllerHelper.IsCurrentUserAdmin())
                return Forbid("User is not admin");

            try
            {
                await _adminService.BlockCommentAsync(commentId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("users/{userId}/unblock")]
        public async Task<IActionResult> UnblockUser(Guid userId)
        {
            if (!_controllerHelper.IsCurrentUserAdmin())
                return Forbid("User is not admin");

            try
            {
                await _adminService.UnblockUserAsync(userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("reviews/{reviewId}/unblock")]
        public async Task<IActionResult> UnblockReview(Guid reviewId)
        {
            if (!_controllerHelper.IsCurrentUserAdmin())
                return Forbid("User is not admin");

            try
            {
                await _adminService.UnblockReviewAsync(reviewId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("comments/{commentId}/unblock")]
        public async Task<IActionResult> UnblockComment(Guid commentId)
        {
            if (!_controllerHelper.IsCurrentUserAdmin())
                return Forbid("User is not admin");

            try
            {
                await _adminService.UnblockCommentAsync(commentId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}