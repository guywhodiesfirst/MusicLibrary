using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Genre : BaseEntity
    {
        public Guid MusicBrainzId { get; set; }
        public string Name { get; set; }
        public IEnumerable<Album> Albums { get; set; }
    }
}
