using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class ReviewReaction : BaseEntity
    {
        [ForeignKey(nameof(Review))]
        public Guid ReviewId { get; set; }
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public bool IsLike { get; set; }
        public Review Review { get; set; }
        public User User { get; set; }
    }
}