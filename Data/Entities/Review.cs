using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Review : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid AlbumId { get; set; }
        public int Rating { get; set; }
        public string? Content { get; set; }
        public User User { get; set; }
        public Album Album { get; set; }
    }
}
