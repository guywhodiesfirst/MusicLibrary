using Business.Models.Comments;
using Business.Models.Playlists;
using Business.Models.Reviews;

namespace Business.Models.Users
{
    public class UserDetailsDto : BaseDto
    {
        public int ReviewCount { get; set; }
        public int PlaylistCount { get; set; }
        public int CommentCount { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsBlocked { get; set; }
        public string About { get; set; }
        public ICollection<ReviewDto> Reviews { get; set; }
        public ICollection<PlaylistDto> Playlists { get; set; }
        public ICollection<CommentDto> Comments { get; set; }
    }
}