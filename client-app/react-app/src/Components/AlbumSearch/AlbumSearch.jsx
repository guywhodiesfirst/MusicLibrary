import React, { useState } from 'react';
import { AlbumsApi } from '../../API/AlbumsApi'
import AlbumSearchRow from '../AlbumSearchRow/AlbumSearchRow'

export default function AlbumSearch({ onAlbumsFetched }) {
  const [query, setQuery] = useState('');
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
      const response = await AlbumsApi.searchAlbums(query);
      if (!response.success) {
        setError(response.message);
      } else if (response.albums.length === 0) {
        setError("No albums found by the given criteria");
        onAlbumsFetched([]);
      } else {
        const albums = response.albums.map((album) => ({
          id: album.id,
          name: album.name,
          genre: album.genre || null,
          averageRating: album.averageRating,
          releaseDate: album.releaseDate
            ? new Date(album.releaseDate).toLocaleDateString()
            : 'Unknown',
          artists: album.artists ? album.artists.join(', ') : null,
        }));
        onAlbumsFetched(albums);
      }
    } catch (error) {
      setError("Unexpected error. Try again");
    } finally {
      setIsSearching(false);
    }
  };

  return (
    <div>
      <div style={{ marginBottom: '20px' }}>
        <input
          type="text"
          value={query}
          onChange={(e) => setQuery(e.target.value)}
          placeholder="Enter album name"
          className="search-input"
        />
        <button onClick={handleSearch} style={{ padding: '10px 20px' }}>
          Search
        </button>
      </div>
      {error && <p style={{ color: 'red' }}>{error}</p>}
      {isSearching && <p>Searching...</p>}
    </div>
  );
}