using System.Text.Json.Serialization;

namespace Business.Models
{
    public class MusicBrainzReleaseGroup
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        [JsonPropertyName("first-release-date")]
        public string FirstReleaseDate { get; set; }
        [JsonPropertyName("artist-credit")]
        public IEnumerable<Credit> ArtistCredit { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }

    public class Credit
    {
        public string Name { get; set; }
    }

    public class Tag
    {
        public int Count { get; set; }
        public string Name { get; set; }
    }
}