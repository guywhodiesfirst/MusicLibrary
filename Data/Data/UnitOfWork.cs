using Data.Interfaces;
using Data.Repositories;

namespace Data.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MusicLibraryDataContext _context;

        public UnitOfWork(MusicLibraryDataContext context)
        {
            _context = context;
        }

        private IAlbumRepository _albumRepository;
        public IAlbumRepository AlbumRepository =>
            _albumRepository ??= new AlbumRepository(_context);

        private IPlaylistRepository _playlistRepository;
        public IPlaylistRepository PlaylistRepository =>
            _playlistRepository ??= new PlaylistRepository(_context);

        private IGenreRepository _genreRepository;
        public IGenreRepository GenreRepository =>
            _genreRepository ??= new GenreRepository(_context);

        private IReviewRepository _reviewRepository;
        public IReviewRepository ReviewRepository =>
            _reviewRepository ??= new ReviewRepository(_context);

        private IUserRepository _userRepository;
        public IUserRepository UserRepository =>
            _userRepository ??= new UserRepository(_context);

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}