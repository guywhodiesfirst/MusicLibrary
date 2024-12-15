namespace Business.Models.Playlists
{
    public class PlaylistUpdateDto : BaseDto
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}