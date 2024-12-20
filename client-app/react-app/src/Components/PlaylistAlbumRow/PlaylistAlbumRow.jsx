import React from 'react';
import './PlaylistAlbumRow.css';
import { useNavigate } from 'react-router-dom';


export default function PlaylistAlbumRow({ album, isUserPlaylistOwner, onDelete }) {
  const navigate = useNavigate();
  const handleView = () => {
    navigate(`/albums/${album.id}`)
  }
  const handleDelete = () => {
    onDelete(album.id)
  }
  return (
    <>
        <div className='album-row'>
            <div>
                <div className='album-title-container'>
                    <strong>{album.name}</strong>
                </div>
                <div className='album-row-info-container'>
                    <div className='album-row-info'>
                        {album.genre && <p>Genre: {album.genre}</p>}
                        {album.releaseDate &&
                        <p>
                            Released: {new Date(album.releaseDate).toLocaleDateString()}
                        </p>}
                        {album.artists && 
                        <p>
                            by {album.artists}
                        </p>}
                    </div>
                    <div className='btn-group'>
                        <button onClick={handleView}>View</button>
                        {isUserPlaylistOwner &&
                            <button onClick={handleDelete}>Delete</button>
                        }
                    </div>
                </div>
            </div>
        </div>
    </>
  );
}