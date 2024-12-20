﻿namespace Data.Interfaces
{
    public interface IUnitOfWork
    {
        IAlbumRepository AlbumRepository { get; }
        IPlaylistRepository PlaylistRepository { get; }
        IReviewRepository ReviewRepository { get; }
        IUserRepository UserRepository { get; }
        ICommentRepository CommentRepository { get; }
        IReviewReactionRepository ReviewReactionRepository { get; }
        Task SaveChangesAsync();
    }
}