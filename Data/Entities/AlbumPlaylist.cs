namespace Data.Entities
{
    public class AlbumPlaylist
    {
        public Guid AlbumId { get; set; }
        public Guid PlaylistId { get; set; }
        public Album Album { get; set; }
        public Playlist Playlist { get; set; }
    }
}
