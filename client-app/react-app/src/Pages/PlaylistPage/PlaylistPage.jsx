import { useParams, useNavigate } from "react-router-dom";
import { useContext, useState, useEffect } from "react";
import { Context } from "../../App";
import './PlaylistPage.css'
import { PlaylistApi } from "../../API/PlaylistsApi";
import PlaylistForm from "../../Components/PlaylistForm/PlaylistForm";
import PlaylistInfo from "../../Components/PlaylistInfo/PlaylistInfo";
import PlaylistAlbumSection from "../../Components/PlaylistAlbumSection/PlaylistAlbumSection";
import PlaylistAlbumSearch from "../../Components/PlaylistAlbumSearch/PlaylistAlbumSearch";

export default function PlaylistPage() {
    const { id } = useParams();
    const { isAuthenticated, user } = useContext(Context)
    const [playlist, setPlaylist] = useState(null)
    const [loading, setLoading] = useState(true)
    const [isUserPlaylistOwner, setIsUserPlaylistOwner] = useState(false)
    const [albums, setAlbums] = useState([])
    const [activeTab, setActiveTab] = useState("albums");
    const navigate = useNavigate()

    const fetchPlaylist = async () => {
        try {
            const response = await PlaylistApi.getByIdWithAlbums(id);
            if (!response.success) {
                setError(response.message);
            } else {
                const playlistData = response.playlist;
                const { albums: playlistAlbums, ...rest} = response.playlist;
                setAlbums(playlistAlbums);
                setPlaylist(rest);
                if(user) {
                    setIsUserPlaylistOwner(response.playlist.userId === user.id);
                } else {
                    setIsUserPlaylistOwner(false);
                }
            }
        } catch {
            alert("Unexpected error while fetching playlist. Try again.");
        }
    };

    const handleUpdate = async (updatedPlaylist) => {
        try {
            setLoading(true)
            const response = await PlaylistApi.updatePlaylist(id, updatedPlaylist);
            if (!response.success) {
                setError(response.message);
            } else {
                await fetchPlaylist()
            }
        } catch {
            alert("Unexpected error while updating playlist. Try again.");
        } finally {
            setLoading(false)
        }
    }

    const handleAddToPlaylist = async(albumId) => {
        try {
            setLoading(true)
            const response = await PlaylistApi.addAlbumToPlaylist(albumId, playlist.id);
            if (!response.success) {
                setError(response.message)
            } else {
                await fetchPlaylist()
            }
        } catch {
            alert("Unexpected error while adding album. Try again.");
        } finally {
            setLoading(false)
        }
    }

    const handleDeleteFromPlaylist = async (albumId) => {
        try {
            setLoading(true)
            const response = await PlaylistApi.removeAlbumFromPlaylist(albumId, playlist.id);
            if (!response.success) {
                setError(response.message)
            } else {
                await fetchPlaylist()
            }
        } catch {
            alert("Unexpected error while removing album. Try again.");
        } finally {
            setLoading(false)
        }
    }

    useEffect(() => {
        const fetchData = async () => {
            setLoading(true);
            if (id) {
                await fetchPlaylist();   
            }
            setLoading(false);
        };
        fetchData();
    }, [id]);

    if(loading) return(
        <div>
            Loading...
        </div>
    )

    return(
        <div className="playlist-page">
            <div className="playlist-wrapper">
                <div className="playlist-info-container">
                    <PlaylistInfo playlist={playlist} />
                    {isAuthenticated && isUserPlaylistOwner &&
                        <PlaylistForm onSubmit={handleUpdate} infoText={"Update"}/>
                    }
                </div>
                {isUserPlaylistOwner ?
                    <div className="tabs">
                        <h2>Manage Playlist</h2>
                        <div className="tab-btn-group">
                            <button
                                className={`tab-btn ${activeTab=="albums" ? "" : "not-active"}`}
                                onClick={() => setActiveTab("albums")}>
                                View Albums
                            </button>
                            <button
                                className={`tab-btn ${activeTab=="add" ? "" : "not-active"}`}
                                onClick={() => setActiveTab("add")}>
                                Add Albums
                            </button>
                        </div>
                        {activeTab==="albums" ?
                            <PlaylistAlbumSection
                                handleAlbumDelete={handleDeleteFromPlaylist}
                                albums={albums}
                                isUserPlaylistOwner={isUserPlaylistOwner}
                            /> :
                            <PlaylistAlbumSearch onAdd={handleAddToPlaylist}/>
                        }
                    </div>
                    :
                    <PlaylistAlbumSection
                        handleAlbumDelete={handleDeleteFromPlaylist}
                        albums={albums}
                        isUserPlaylistOwner={isUserPlaylistOwner}
                    />
                }
                
            </div>
        </div>
    )
}