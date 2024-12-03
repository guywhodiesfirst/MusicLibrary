namespace Data.Entities
{
    public class Album : BaseEntity
    {
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Guid GenreId { get; set; }
        public Genre Genre { get; set; }
        public ICollection<string> Artists { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Playlist> Playlists { get; set; }
    }
}