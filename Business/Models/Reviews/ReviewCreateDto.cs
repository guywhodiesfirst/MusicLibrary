namespace Business.Models.Reviews
{
    public class ReviewCreateDto
    {
        public Guid AlbumId { get; set; }
        public Guid UserId { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
    }
}