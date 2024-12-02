using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MusicLibraryDataContext _context;
        public UserRepository(MusicLibraryDataContext context)
        {
            _context = context;
        }
        public Task AddAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
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

        public Task<User> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdWithDetailsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
