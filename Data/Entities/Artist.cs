using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Artist : BaseEntity
    {
        public Guid MusicBrainzId { get; set; }
        public string Name { get; set; }
        public string? Country { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }
        public IEnumerable<Album> Albums { get; set; }
    }
}