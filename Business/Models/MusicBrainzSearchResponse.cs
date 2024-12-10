using System.Text.Json.Serialization;

namespace Business.Models
{
    public class MusicBrainzSearchResponse
    {
        [JsonPropertyName("release-groups")]
        public IEnumerable<MusicBrainzReleaseGroup> Albums { get; set; }
    }
}