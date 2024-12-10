using System.Text.Json.Serialization;

namespace Business.Models
{
    public class MusicBrainzReleaseGroup
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("first-release-date")]
        public string FirstReleaseDate { get; set; }
        [JsonPropertyName("artist-credit")]
        public IEnumerable<Credit> ArtistCredit { get; set; }
        [JsonPropertyName("tags")]
        public IEnumerable<Tag> Tags { get; set; }
    }

    public class Credit
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class Tag
    {
        public int Count { get; set; }
        public string Name { get; set; }
    }
}