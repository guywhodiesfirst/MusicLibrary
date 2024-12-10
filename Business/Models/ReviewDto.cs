namespace Business.Models
{
    public class ReviewDto : BaseDto
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public Guid AlbumId { get; set; }
        public string AlbumName { get; set; }
        public int Rating { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public string Content { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }
}