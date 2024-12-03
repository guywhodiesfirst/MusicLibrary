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
    public class GenreRepository : IGenreRepository
    {
        private readonly MusicLibraryDataContext _context;
        public GenreRepository(MusicLibraryDataContext context)
        {
            _context = context;
        }
        public Task AddAsync(Genre entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            var users = await _context.Genres.ToListAsync();
            return users;
        }

        public async Task<Genre> GetByIdAsync(Guid id)
        {
            var genre = await _context.Genres.FindAsync(id);
            return genre ?? null;
        }

        public Task UpdateAsync(Genre entity)
        {
            throw new NotImplementedException();
        }
    }
}
