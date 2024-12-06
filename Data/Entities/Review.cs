namespace Data.Entities
{
    public class Review : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid AlbumId { get; set; }
        public int Rating { get; set; }
        public int NetVotes { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public User User { get; set; }
        public Album Album { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<ReviewReaction> Reactions { get; set; }
    }
}