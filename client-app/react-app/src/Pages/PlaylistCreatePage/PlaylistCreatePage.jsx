import { useContext, useState, useEffect } from "react";
import { Context } from "../../App";
import { useNavigate } from "react-router-dom";
import './PlaylistCreatePage.css'
import { PlaylistApi } from "../../API/PlaylistsApi";
import PlaylistForm from "../../Components/PlaylistForm/PlaylistForm";

export default function PlaylistCreatePage() {
    const { isAuthenticated, user } = useContext(Context)
    const [profile, setProfile] = useState(null)
    const [loading, setIsLoading] = useState(false)
    const [error, setError] = useState('')
    const [isCurrentUserPage, setIsCurrentUserPage] = useState(false)
    const [playlists, setPlaylists] = useState([])
    const navigate = useNavigate()

    const handlePlaylistCreate = async (playlist) => {
        try {
            setIsLoading(true)
            const response = await PlaylistApi.createPlaylist(user.id, playlist);
            if (response.success) {
                navigate('/me')
            } else {
                alert(response.message);
            }
        } catch {
            alert("Unexpected error while trying to create playlist. Try again.");
        } finally {
            setIsLoading(false)
        }
    }
    useEffect(() => {
        if(!isAuthenticated) {
            navigate('/login')
        }
    }, []);

    if(loading) {
        return(
            <div>
                Loading...
            </div>
        )
    }

    return(
        <div className="playlist-create-page">
            <div className="playlist-create-wrapper">
                <div className="playlist-create-form-container ">
                    <PlaylistForm onSubmit={handlePlaylistCreate} infoText={"Create"}/>
                </div>
            </div>
        </div>
    )
}