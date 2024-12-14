using Business.Interfaces;
using Business.Models.MusicBrainz;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Business.Services
{
    public class MusicBrainzQueryService : IMusicBrainzQueryService
    {
        private readonly HttpClient _httpClient;
        public MusicBrainzQueryService(HttpClient client)
        {
            _httpClient = client;
            _httpClient.DefaultRequestHeaders.UserAgent.Clear();
            _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Cassette", "1.0"));
        }
        public async Task<MusicBrainzReleaseGroup> GetAlbumByIdAsync(Guid albumId)
        {
            if (albumId == Guid.Empty)
            {
                throw new ArgumentException("Album ID cannot be empty", nameof(albumId));
            }

            var requestUrl = $"{_httpClient.BaseAddress}release-group/{albumId}?inc=artist-credits+tags&fmt=json";
            using var response = await _httpClient.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<MusicBrainzReleaseGroup>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result;
        }

        public async Task<MusicBrainzSearchResponse> GetAlbumsByNameAsync(string searchQuery)
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                throw new ArgumentException("Search query cannot be empty", nameof(searchQuery));
            }

            var nameQuery = Uri.EscapeDataString(searchQuery);
            var requestUrl = $"{_httpClient.BaseAddress}release-group/?query=name:{nameQuery}&fmt=json&limit=10";
            using var response = await _httpClient.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode(); // throws an exception if status code is not 200

            var responseContent = await response.Content.ReadAsStringAsync();
            var searchResult = JsonSerializer.Deserialize<MusicBrainzSearchResponse>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return searchResult;
        }
    }
}