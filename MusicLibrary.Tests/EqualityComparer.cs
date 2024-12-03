using Data.Entities;
using System.Diagnostics.CodeAnalysis;
namespace MusicLibrary.Tests
{
    internal class AlbumEqualityComparer : IEqualityComparer<Album>
    {
        public bool Equals([AllowNull] Album x, [AllowNull] Album y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id &&
                    x.Name == y.Name &&
                    x.ReleaseDate == y.ReleaseDate &&
                    x.GenreId == y.GenreId;
        }

        public int GetHashCode([DisallowNull] Album obj)
        {
            return obj.Id.GetHashCode();
        }
    }

    internal class AlbumPlaylistEqualityComparer : IEqualityComparer<AlbumPlaylist>
    {
        public bool Equals([AllowNull] AlbumPlaylist x, [AllowNull] AlbumPlaylist y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.AlbumId == y.AlbumId && x.PlaylistId == y.PlaylistId;
        }

        public int GetHashCode([DisallowNull] AlbumPlaylist obj)
        {
            return HashCode.Combine(obj.AlbumId, obj.PlaylistId);
        }
    }

    internal class GenreEqualityComparer : IEqualityComparer<Genre>
    {
        public bool Equals([AllowNull] Genre x, [AllowNull] Genre y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id && x.Name == y.Name;
        }

        public int GetHashCode([DisallowNull] Genre obj)
        {
            return obj.Id.GetHashCode();
        }
    }

    internal class PlaylistEqualityComparer : IEqualityComparer<Playlist>
    {
        public bool Equals([AllowNull] Playlist x, [AllowNull] Playlist y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id &&
                    x.Name == y.Name &&
                    x.Description == y.Description &&
                    x.UserId == y.UserId;
        }

        public int GetHashCode([DisallowNull] Playlist obj)
        {
            return obj.Id.GetHashCode();
        }
    }

    internal class ReviewEqualityComparer : IEqualityComparer<Review>
    {
        public bool Equals([AllowNull] Review x, [AllowNull] Review y)
        {
            if (x == null || y == null) return x == y;
            return x.Id == y.Id &&
                   x.UserId == y.UserId &&
                   x.AlbumId == y.AlbumId &&
                   x.Rating == y.Rating &&
                   x.Content == y.Content;
        }

        public int GetHashCode([DisallowNull] Review obj)
        {
            return obj.Id.GetHashCode();
        }
    }

    internal class UserEqualityComparer : IEqualityComparer<User>
    {
        public bool Equals([AllowNull] User x, [AllowNull] User y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id &&
                    x.Username == y.Username &&
                    x.Password == y.Password &&
                    x.Email == y.Email &&
                    x.IsAdmin == y.IsAdmin &&
                    x.IsBlocked == y.IsBlocked;
        }

        public int GetHashCode([DisallowNull] User obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}