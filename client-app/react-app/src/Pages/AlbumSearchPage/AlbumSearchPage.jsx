import React, { useState } from 'react';
import AlbumSearchRow from '../../Components/AlbumSearchRow/AlbumSearchRow';
import "./AlbumSearchPage.css";
import { AlbumsApi } from '../../API/AlbumsApi';

export default function AlbumSearchPage() {
  const [query, setQuery] = useState('');
  const [albums, setAlbums] = useState([]);
  const [error, setError] = useState(null);
  const [isSearching, setIsSearching] = useState(false);

  const handleSearch = async () => {
    if (!query.trim()) {
      setError('Please enter a valid album name.');
      return;
    }

    setError(null);
    setIsSearching(true);

    try {
      const response = await AlbumsApi.searchAlbums(query)
      if(!response.success) {
        setError(response.message)
      } else if (response.albums.length === 0) {
        setError("No albums found by the given criteria")
        setAlbums([])
      } else {
        setAlbums(response.albums.map((album) => ({
          id: album.id,
          name: album.name,
          genre: album.genre || null,
          averageRating: album.averageRating,
          releaseDate: album.releaseDate ? new Date(album.releaseDate).toLocaleDateString() : 'Unknown',
          artists: album.artists ? album.artists.join(', ') : null,
        })))
      }
    } catch(error) {
      setError("Unexpected error. Try again")
    } finally {
      setIsSearching(false)
    }
  }

  return (
    <div style={{ padding: '20px' }}>
      <h1>Album Search</h1>
      <div style={{ marginBottom: '20px' }}>
        <input
          type="text"
          value={query}
          onChange={(e) => setQuery(e.target.value)}
          placeholder="Enter album name"
          className='search-input'
        />
        <button onClick={handleSearch} style={{ padding: '10px 20px' }}>
          Search
        </button>
      </div>

      {error && <p style={{ color: 'red' }}>{error}</p>}

      <div>
        {!isSearching && albums.length === 0 ? (
          <p>Here your search results will be displayed</p>
        ) : null}

        {isSearching ? (
          <p>Searching...</p>
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