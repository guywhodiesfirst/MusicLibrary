import React, { useState } from 'react';
import AlbumSearch from '../../Components/AlbumSearch/AlbumSearch'
import AlbumSearchRow from '../../Components/AlbumSearchRow/AlbumSearchRow';
import "./AlbumSearchPage.css";

export default function AlbumSearchPage() {
  const [albums, setAlbums] = useState([]);

  return (
    <div style={{ padding: '20px' }}>
      <h1>Album Search</h1>
      <AlbumSearch onAlbumsFetched={setAlbums} />
      <div>
        {albums.length === 0 ? (
          <p>Here your search results will be displayed</p>
        ) : (
          <ul style={{ listStyle: 'none', padding: 0 }}>
            {albums.map((album) => (
              <AlbumSearchRow key={album.id} album={album} />
            ))}
          </ul>
        )}
      </div>
    </div>
  );
}