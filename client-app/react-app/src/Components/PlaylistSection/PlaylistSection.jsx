import React, { useState, useContext } from "react";
import { Context } from "../../App";
import './PlaylistSection.css'
import PlaylistRow from "../PlaylistRow/PlaylistRow";

export default function PlaylistSection({
  handlePlaylistCreate,
  handlePlaylistDelete,
  playlists,
  isUserPlaylistsOwner
}) {
    const { isAuthenticated } = useContext(Context);
    return (
        <div>
            <div>
                <h3>Playlists</h3>
                {isAuthenticated && isUserPlaylistsOwner && 
                    <button onClick={handlePlaylistCreate}>Create playlist</button>
                }
                <div className="playlists-list">
                    {playlists.length > 0 ? (
                        playlists.map((playlist) => 
                        <PlaylistRow
                            key={playlist.id} 
                            playlist={playlist}
                            onDelete={handlePlaylistDelete}
                            isUserPlaylistOwner={isUserPlaylistsOwner}
                        />)
                    ) : (
                        <p>No playlists yet</p>
                    )    
                    }
                </div>
            </div>
        </div>
    );
}