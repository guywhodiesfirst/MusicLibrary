import React, { useState } from 'react';
import AlbumSearch from '../AlbumSearch/AlbumSearch'
import AlbumSearchRow from '../AlbumSearchRow/AlbumSearchRow';
import "./PlaylistAlbumSearch.css";

export default function PlaylistAlbumSearch({onAdd}) {
  const [albums, setAlbums] = useState([]);

  return (
    <div style={{ padding: '20px' }}>
      <h3>Album Search</h3>
      <AlbumSearch onAlbumsFetched={setAlbums} />
      <div className='search'>
        {albums.length === 0 ? (
          <p>Here your search results will be displayed</p>
        ) : (
          <div className='album-search-list'>
            {albums.map((album) => (
              <AlbumSearchRow key={album.id} album={album} onAdd={onAdd}/>
            ))}
          </div>
        )}
      </div>
    </div>
  );
}