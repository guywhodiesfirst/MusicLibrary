﻿using System.Text.Json.Serialization;

namespace Business.Models.Auth
{
    public class LoginResponseDto
    {
        public string Username { get; set; }
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
    }
}