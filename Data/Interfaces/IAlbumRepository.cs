using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IAlbumRepository : IRepository<Album>
    {
        Task<IEnumerable<Album>> GetAllWithDetailsAsync();
        Task<Album> GetByIdWithDetailsAsync(Guid id);
    }
}
