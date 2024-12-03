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
    public class AlbumRepository : IAlbumRepository
    {
        private readonly MusicLibraryDataContext _context;
        public AlbumRepository(MusicLibraryDataContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Album entity)
        {
            await _context.Albums.AddAsync(entity);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var album = await _context.Albums.FindAsync(id);
            if (album != null)
            {
                _context.Albums.Remove(album);
            }
        }

        public async Task<IEnumerable<Album>> GetAllAsync()
        {
            var album = await _context.Albums.ToListAsync();
            return album ?? null;
        }

        public async Task<IEnumerable<Album>> GetAllWithDetailsAsync()
        {
            var albums = await _context.Albums
                              .Include(a => a.Genre)
                              .Include(a => a.Playlists)
                              .Include(a => a.Reviews)
                              .ToListAsync();

            return albums ?? null;
        }

        public async Task<Album> GetByIdAsync(Guid id)
        {
            var album = await _context.Albums.FindAsync(id);
            return album ?? null;
        }

        public async Task<Album> GetByIdWithDetailsAsync(Guid id)
        {
            var album = await _context.Albums
                              .Include(a => a.Genre)
                              .Include(a => a.Playlists)
                              .Include(a => a.Reviews)
                              .FirstOrDefaultAsync(a => a.Id == id);
            
            return album ?? null;
        }

        public async Task UpdateAsync(Album entity)
        {
            if (entity != null)
            {
                var album = await _context.Albums.FindAsync(entity.Id);
                if(album != null)
                {
                    album.Name = entity.Name;
                    album.Artists = entity.Artists;
                    album.Genre = entity.Genre;
                    album.GenreId = entity.GenreId;
                    album.ReleaseDate = entity.ReleaseDate;
                }
            }
        }
    }
}