namespace Data.Entities
{
    public class Review : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid AlbumId { get; set; }
        public int Rating { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public User User { get; set; }
        public Album Album { get; set; }
    }
}