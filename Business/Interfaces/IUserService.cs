using Business.Models.Users;

namespace Business.Interfaces
{
    public interface IUserService : IService<UserDto>
    {
        Task UpdateAsync(UserDto model);
        Task<UserDetailsDto> GetByIdWithDetailsAsync(Guid id);
        Task<bool> DoesUserExistByIdAsync(Guid id);
    }
}