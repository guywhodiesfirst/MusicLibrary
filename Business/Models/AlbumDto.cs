namespace Business.Models
{
    public class AlbumDto : BaseDto
    {
        public string Name { get; set; }
        public string Genre { get; set; }
        public decimal AverageRating { get; set; }
        public ICollection<string> Artists { get; set; }
    }
}