using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class AlbumPlaylist
    {
        [ForeignKey(nameof(Album))]
        public Guid AlbumId { get; set; }
        [ForeignKey(nameof(Playlist))]
        public Guid PlaylistId { get; set; }
        public Album Album { get; set; }
        public Playlist Playlist { get; set; }
    }
}
