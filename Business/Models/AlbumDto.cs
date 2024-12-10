namespace Business.Models
{
    public class AlbumDto : BaseDto
    {
        public int ReviewCount { get; set; }
        public string Name { get; set; }
        public string GenreName { get; set; }
        public decimal AverageRating { get; set; }
        public ICollection<string> Artists { get; set; }
    }
}