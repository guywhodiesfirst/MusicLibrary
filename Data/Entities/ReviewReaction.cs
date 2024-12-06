namespace Data.Entities
{
    public class ReviewReaction : BaseEntity
    {
        public Guid ReviewId { get; set; }
        public Guid UserId { get; set; }
        public bool IsLike { get; set; }
        public Review Review { get; set; }
        public User User { get; set; }
    }
}