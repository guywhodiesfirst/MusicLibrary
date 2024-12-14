namespace Business.Models.Playlists
{
    public class PlaylistCreateDto
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
