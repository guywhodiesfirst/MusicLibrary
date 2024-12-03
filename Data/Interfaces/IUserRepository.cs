using Data.Entities;

namespace Data.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetAllWithDetailsAsync();
        Task<User> GetByIdWithDetailsAsync(Guid id);
    }
}