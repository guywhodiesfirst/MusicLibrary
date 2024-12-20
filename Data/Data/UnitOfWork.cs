﻿using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

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

        private IReviewRepository _reviewRepository;
        public IReviewRepository ReviewRepository =>
            _reviewRepository ??= new ReviewRepository(_context);

        private IUserRepository _userRepository;
        public IUserRepository UserRepository =>
            _userRepository ??= new UserRepository(_context);

        private IReviewReactionRepository _reviewReactionRepository;
        public IReviewReactionRepository ReviewReactionRepository =>
            _reviewReactionRepository ??= new ReviewReactionRepository(_context);

        private ICommentRepository _commentRepository;
        public ICommentRepository CommentRepository =>
            _commentRepository ??= new CommentRepository(_context);
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}