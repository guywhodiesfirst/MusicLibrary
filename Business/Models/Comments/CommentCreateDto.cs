namespace Business.Models.Comments
{
    public class CommentCreateDto
    {
        public Guid ReviewId { get; set; }
        public Guid UserId { get; set; }
        public string Content { get; set; }
    }
}