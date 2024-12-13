using Business.Models;

namespace Business.Interfaces
{
    public interface IUserService : IService<UserDto>
    {
        Task AddAsync(UserDto model);
        Task UpdateAsync(UserDto model);
        Task<UserDetailsDto> GetByIdWithDetailsAsync(Guid id);
        Task<bool> DoesUserExistByIdAsync(Guid id);
    }
}