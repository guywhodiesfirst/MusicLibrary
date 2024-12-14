using System.Text.Json.Serialization;

namespace Business.Models.MusicBrainz
{
    public class MusicBrainzSearchResponse
    {
        [JsonPropertyName("release-groups")]
        public IEnumerable<MusicBrainzReleaseGroup> Albums { get; set; }
    }
}