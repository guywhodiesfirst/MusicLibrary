using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IUnitOfWork
    {
        IAlbumRepository AlbumRepository { get; }
        IGenreRepository GenreRepository { get; }
        IPlaylistRepository PlaylistRepository { get; }
        IReviewRepository ReviewRepository { get; }
        IUserRepository UserRepository { get; }
        Task SaveChangesAsync();
    }
}