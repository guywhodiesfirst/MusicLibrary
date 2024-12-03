using Data.Entities;

namespace Data.Interfaces
{
    public interface IAlbumRepository : IRepository<Album>
    {
        Task<IEnumerable<Album>> GetAllWithDetailsAsync();
        Task<Album> GetByIdWithDetailsAsync(Guid id);
    }
}
