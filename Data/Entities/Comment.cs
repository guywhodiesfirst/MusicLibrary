namespace Data.Entities
{
    public class Comment : BaseEntity
    {
        public Guid ReviewId { get; set; }
        public Guid UserId { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public Review Review { get; set; }
        public User User { get; set; }
    }
}
