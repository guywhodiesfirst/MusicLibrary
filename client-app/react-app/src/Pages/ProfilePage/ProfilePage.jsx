import { useParams, useNavigate } from "react-router-dom";
import { useContext, useState, useEffect } from "react";
import { Context } from "../../App";
import './ProfilePage.css'
import UserInfo from "../../Components/UserInfo/UserInfo";
import { UsersApi } from "../../API/UsersApi";
import UserAboutForm from "../../Components/UserAboutForm/UserAboutForm";
import { PlaylistApi } from "../../API/PlaylistsApi";
import PlaylistSection from "../../Components/PlaylistSection/PlaylistSection";

export default function ProfilePage() {
    const { id } = useParams();
    const { isAuthenticated, user } = useContext(Context)
    const [profile, setProfile] = useState(null)
    const [loading, setLoading] = useState(true)
    const [error, setError] = useState('')
    const [isCurrentUserPage, setIsCurrentUserPage] = useState(false)
    const [playlists, setPlaylists] = useState([])
    const navigate = useNavigate()

    const fetchUser = async () => {
        try {
            const response = await UsersApi.getUserDetails(id);
            if (!response.success) {
                setError(response.message);
            } else {
                setProfile(response.user);
                setIsCurrentUserPage(response.user.id === user.id);
            }
        } catch {
            setError("Unexpected error while fetching user profile. Try again.");
        }
    };
    

    const fetchPlaylists = async () => {
        try {
            const response = await PlaylistApi.getPlaylistsByUser(id);
            console.log(response)
            if (!response.success) {
                setError(response.message)
            } else {
                setPlaylists(response.playlists)
            }
        } catch {
            setError("Unexpected error while fetching user playlists. Try again.");
        }
    }

    const handlePlaylistCreate = async () => {
        navigate('/createPlaylist')
    }

    const handlePlaylistDelete = async (playlistId) => {
        try {
            const response = await PlaylistApi.deletePlaylist(playlistId);
            console.log(response)
            if (!response.success) {
                setError(response.message)
            } else {
                await fetchUser()
                await fetchPlaylists()
            }
        } catch {
            setError("Unexpected error while fetching user playlists. Try again.");
        }
    }

    useEffect(() => {
        const fetchData = async () => {
            setLoading(true);
            if (id) {
                await fetchUser();
                await fetchPlaylists();   
            }
            setLoading(false);
        };
        fetchData();
    }, [id]);

    const handleSubmitAbout = async (about) => {
        try {
            setLoading(true)
            const response = await UsersApi.updateUserAboutSection(id, about)
            if(!response.success) {
                alert(response.message)
            } else {
                fetchUser()
            } 
        } catch {
            alert(error)
        } finally {
            setLoading(false)
        }
    }

    if(loading) return(
        <div>
            Loading...
        </div>
    )

    return(
        <div className="user-page">
            <div className="user-wrapper">
                <div className="user-info-container">
                    <UserInfo user={profile} />
                    {isAuthenticated && isCurrentUserPage &&
                        <UserAboutForm onSubmit={handleSubmitAbout} />
                    }
                </div>
                <PlaylistSection
                    handlePlaylistCreate={handlePlaylistCreate}
                    handlePlaylistDelete={handlePlaylistDelete}
                    playlists={playlists}
                    isUserPlaylistsOwner={isCurrentUserPage}
                />
            </div>
        </div>
    )
}