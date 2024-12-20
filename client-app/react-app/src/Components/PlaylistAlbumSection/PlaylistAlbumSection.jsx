import React, { useState, useContext } from "react";
import { Context } from "../../App";
import './PlaylistAlbumSection.css'
import PlaylistAlbumRow from "../PlaylistAlbumRow/PlaylistAlbumRow";

export default function PlaylistSection({
  handleAlbumDelete,
  albums,
  isUserPlaylistOwner
}) {
    const { isAuthenticated } = useContext(Context);
    return (
        <div>
            <div>
                <h3>Albums</h3>
                <div className="albums-list">
                    {albums.length > 0 ? (
                        albums.map((album) => 
                        <PlaylistAlbumRow
                            key={album.id} 
                            album={album}
                            onDelete={handleAlbumDelete}
                            isUserPlaylistOwner={isUserPlaylistOwner}
                        />)
                    ) : (
                        <p>No Albums yet</p>
                    )    
                    }
                </div>
            </div>
        </div>
    );
}