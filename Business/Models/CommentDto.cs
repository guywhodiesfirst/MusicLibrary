namespace Business.Models
{
    public class CommentDto : BaseDto
    {
        public Guid ReviewId { get; set; }
        public Guid UserId { get; set; }
        public string Content { get; set; }
        public string Username { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }
}