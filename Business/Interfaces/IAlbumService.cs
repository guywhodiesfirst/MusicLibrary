using Business.Models;

namespace Business.Interfaces
{
    public interface IAlbumService : IService<AlbumDto>
    {
        Task<IEnumerable<AlbumDetailsDto>> GetAllWithDetailsAsync();
        Task<AlbumDetailsDto> GetByIdWithDetailsAsync();
    }
}