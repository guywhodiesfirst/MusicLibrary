namespace API.Interfaces
{
    public interface IControllerHelper
    {
        Guid GetCurrentUserId();
        bool IsCurrentUserAdmin();
    }
}
