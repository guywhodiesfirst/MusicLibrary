namespace Business.Models.Reviews
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
        public int CommentCount { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}