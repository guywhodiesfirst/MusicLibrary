namespace Business.Models
{
    public class GenreDto : BaseDto
    {
        public string Name { get; set; }
        public ICollection<AlbumDto> Albums { get; set; }
    }
}