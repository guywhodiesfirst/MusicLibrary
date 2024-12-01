using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Album : BaseEntity
    {
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public IEnumerable<Artist> Artists { get; set; }
        public Guid GenreId { get; set; }
        public Genre? Genre { get; set; }
    }
}
