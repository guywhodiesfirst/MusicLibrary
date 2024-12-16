namespace Business.Models.Albums
{
    public class AlbumDto : BaseDto
    {
        public string Name { get; set; }
        public string Genre { get; set; }
        public decimal AverageRating { get; set; }
        public DateTime ReleaseDate { get; set; }
        public ICollection<string> Artists { get; set; }
    }
}