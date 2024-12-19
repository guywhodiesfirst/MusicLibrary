import React from 'react';
import './PlaylistRow.css';
import { useNavigate } from 'react-router-dom';


export default function PlaylistRow({ playlist, isUserPlaylistOwner, onDelete }) {
  const navigate = useNavigate();
  const handleView = () => {
    navigate(`/playlists/${playlist.id}`)
  }
  const handleDelete = () => {
    onDelete(playlist.id)
  }
  return (
    <>
        <div className='playlist-row'>
            <div>
                <div className='playlist-title-container'>
                    <strong>{playlist.name}</strong>
                </div>
                <div className='playlist-info'>
                    <p>Albums: {playlist.albumCount} • 
                        Created at: {new Date(playlist.createdAt).toLocaleDateString()}
                    </p> 
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