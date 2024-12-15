using API.Interfaces;
using System.Security.Claims;

public class ControllerHelper : IControllerHelper
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ControllerHelper(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetCurrentUserId()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        return userId == null ? Guid.Empty : Guid.Parse(userId);
    }
}