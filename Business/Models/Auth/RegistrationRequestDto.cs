namespace Business.Models.Auth
{
    public class RegistrationRequestDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}