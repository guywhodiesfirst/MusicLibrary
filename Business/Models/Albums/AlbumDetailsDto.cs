﻿namespace Business.Models.Albums
{
    public class AlbumDetailsDto : BaseDto
    {
        public int ReviewCount { get; set; }
        public decimal AverageRating { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public ICollection<string> Artists { get; set; }
        public ICollection<Guid> PlaylistIds { get; set; }
        public ICollection<Guid> ReviewIds { get; set; }
    }
}