namespace Data.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsBlocked { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Playlist> Playlists { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<ReviewReaction> Reactions { get; set; }
    }
}
