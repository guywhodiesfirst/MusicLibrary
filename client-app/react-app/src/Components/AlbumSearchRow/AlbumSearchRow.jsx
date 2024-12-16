import React from 'react';
import './AlbumSearchRow.css';

export default function AlbumSearchRow({ album }) {
  return (
    <>
        <li className='search-row'>
            <div className='album-info'>
                <strong>{album.name}</strong>{' '}
                {album.artists && (
                <>
                    by {album.artists}
                </>
                )}
                {album.genre && (
                <span> | Genre: <em>{album.genre}</em></span>
                )} | 
                Released: {album.releaseDate}
            </div>
            <button>View</button>
        </li>
    </>
  );
}