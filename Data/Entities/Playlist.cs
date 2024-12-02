namespace Data.Entities
{
    public class Playlist : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<Album> Albums { get; set; }
    }
}
