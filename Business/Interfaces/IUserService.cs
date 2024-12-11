using Business.Models;

namespace Business.Interfaces
{
    public interface IUserService : IService<UserDto>
    {
        Task AddAsync(UserDto model);
    }
}