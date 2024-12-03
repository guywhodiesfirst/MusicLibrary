using Data.Entities;

namespace Data.Interfaces
{
    public interface IPlaylistRepository : IRepository<Playlist>
    {
        Task<IEnumerable<Playlist>> GetAllWithDetailsAsync();
        Task<Playlist> GetByIdWithDetailsAsync(Guid id);
    }
}