using Business.Interfaces;
using Business.Models;

namespace Business.Services
{
    public class AlbumService : IAlbumService
    {
        public Task AddAsync(AlbumDto model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid modelId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AlbumDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AlbumDetailsDto>> GetAllWithDetailsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AlbumDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<AlbumDetailsDto> GetByIdWithDetailsAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(AlbumDto model)
        {
            throw new NotImplementedException();
        }
    }
}