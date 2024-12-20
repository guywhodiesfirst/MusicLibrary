import React from 'react';
import './AlbumSearchRow.css';
import { useNavigate } from 'react-router-dom';


export default function AlbumSearchRow({ album, onAdd }) {
  const navigate = useNavigate();
  const handleView = () => {
    navigate(`/albums/${album.id}`)
  }

  const handleAdd = () => {
    if(onAdd) {
      onAdd(album.id)
    }
  }

  return (
    <>
        <li className='search-row'>
            <div className='search-row-info'>
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
            <div className='btn-group'>
              <button onClick={handleView}>View</button>
              {onAdd &&
                <button onClick={handleAdd}>Add</button>
              }
            </div>
        </li>
    </>
  );
}