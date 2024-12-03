using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MusicLibraryDataContext _context;
        public UserRepository(MusicLibraryDataContext context)
        {
            _context = context;
        }
        public async Task AddAsync(User entity)
        {
            await _context.Users.AddAsync(entity);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public Task<IEnumerable<User>> GetAllWithDetailsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            return user ?? null;
        }

        public async Task<User> GetByIdWithDetailsAsync(Guid id)
        {
            var user = await _context.Users
                              .Include(u => u.Reviews)
                                .ThenInclude(r => r.Album)
                              .Include(u => u.Playlists)
                              .FirstOrDefaultAsync(x => x.Id == id);

            return user ?? null;
        }

        public async Task UpdateAsync(User entity)
        {
            if (entity != null)
            {
                var user = await _context.Users.FindAsync(entity.Id);
                if (user != null)
                {
                    user.Username = entity.Username;
                    user.IsAdmin = entity.IsAdmin;
                    user.IsBlocked = entity.IsBlocked;
                    user.Password = entity.Password;
                }
            }
        }
    }
}
