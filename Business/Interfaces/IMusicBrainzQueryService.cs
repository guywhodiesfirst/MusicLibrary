using Business.Models.Albums;
using Business.Models.MusicBrainz;

namespace Business.Interfaces
{
    public interface IMusicBrainzQueryService
    {
        Task<IEnumerable<AlbumDto>> GetAlbumsByNameAsync(string searchQuery);
        Task<AlbumDto> GetAlbumByIdAsync(Guid albumId);
    }
}