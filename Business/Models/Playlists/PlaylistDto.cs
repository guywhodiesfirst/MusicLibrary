namespace Business.Models.Playlists
{
    public class PlaylistDto : BaseDto
    {
        public int AlbumCount { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Username { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}