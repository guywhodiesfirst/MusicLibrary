using Business.Models.MusicBrainz;

namespace Business.Interfaces
{
    public interface IMusicBrainzQueryService
    {
        Task<MusicBrainzSearchResponse> GetAlbumsByNameAsync(string searchQuery);
        Task<MusicBrainzReleaseGroup> GetAlbumByIdAsync(Guid albumId);
    }
}