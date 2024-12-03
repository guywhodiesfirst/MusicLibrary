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
        public async Task AddAsync(Genre entity)
        {
            await _context.Genres.AddAsync(entity);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre != null)
            {
                _context.Genres.Remove(genre);
            }
        }

        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            var genres = await _context.Genres.ToListAsync();
            return genres;
        }

        public async Task<Genre> GetByIdAsync(Guid id)
        {
            var genre = await _context.Genres.FindAsync(id);
            return genre ?? null;
        }

        public async Task UpdateAsync(Genre entity)
        {
            if(entity != null)
            {
                var genre = await _context.Genres.FindAsync(entity.Id);
                if (genre != null) 
                {
                    genre.Name = entity.Name;
                }
            }
        }
    }
}
