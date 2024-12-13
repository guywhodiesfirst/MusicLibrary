using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class Review : BaseEntity
    {
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        [ForeignKey(nameof(Album))]
        public Guid AlbumId { get; set; }
        public int Rating { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public virtual User User { get; set; }
        public virtual Album Album { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<ReviewReaction> Reactions { get; set; }
    }
}