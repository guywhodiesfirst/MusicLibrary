﻿namespace Business.Models
{
    public class ReviewReactionDto : BaseDto
    {
        public Guid ReviewId { get; set; }
        public Guid UserId { get; set; }
        public bool IsLike { get; set; }
    }
}