using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class Playlist : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User User { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Album> Albums { get; set; }
    }
}