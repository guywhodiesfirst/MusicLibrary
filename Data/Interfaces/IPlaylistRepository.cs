using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IPlaylistRepository : IRepository<Playlist>
    {
        Task<IEnumerable<Playlist>> GetAllWithDetailsAsync();
        Task<Playlist> GetByIdWithDetailsAsync(Guid id);
    }
}