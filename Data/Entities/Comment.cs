using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class Comment : BaseEntity
    {
        [ForeignKey(nameof(Review))]
        public Guid ReviewId { get; set; }
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public Review Review { get; set; }
        public User User { get; set; }
    }
}
