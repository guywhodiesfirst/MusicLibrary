import React from 'react';
import './AlbumSearchRow.css';
import { useNavigate } from 'react-router-dom';


export default function AlbumSearchRow({ album }) {
  const navigate = useNavigate();
  const handleView = () => {
    navigate(`/albums/${album.id}`)
  }
  return (
    <>
        <li className='search-row'>
            <div>
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
            <button onClick={handleView}>View</button>
        </li>
    </>
  );
}