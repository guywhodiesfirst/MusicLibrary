namespace Business.Models
{
    public class AlbumDetailsDto : BaseDto
    {
        public int ReviewCount { get; set; }
        public decimal AverageRating { get; set; }
        public string Name { get; set; }
        public string GenreName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Guid GenreId { get; set; }
        public ICollection<string> Artists { get; set; }
        public ICollection<Guid> PlaylistIds { get; set; }
        public ICollection<Guid> ReviewIds { get; set; }
    }
}