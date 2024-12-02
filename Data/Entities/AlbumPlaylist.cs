﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class AlbumPlaylist
    {
        public Guid AlbumId { get; set; }
        public Guid PlaylistId { get; set; }
        public Album Album { get; set; }
        public Playlist Playlist { get; set; }
    }
}