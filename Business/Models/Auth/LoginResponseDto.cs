namespace Business.Models.Auth
{
    public class LoginResponseDto
    {
        public string Username { get; set; }
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}