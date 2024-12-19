import { useParams } from "react-router-dom";
import { useContext, useState, useEffect } from "react";
import { Context } from "../../App";
import './ProfilePage.css'
import UserInfo from "../../Components/UserInfo/UserInfo";
import { UsersApi } from "../../API/UsersApi";
import UserAboutForm from "../../Components/UserAboutForm/UserAboutForm";

export default function ProfilePage() {
    const { id } = useParams();
    const { isAuthenticated, user } = useContext(Context)
    const [profile, setProfile] = useState(null)
    const [loading, setLoading] = useState(true)
    const [error, setError] = useState('')
    const fetchUser = async (userId) => {
        try {
        const response = await UsersApi.getUserDetails(id);
        if (!response.success) {
            setError(response.message);
        } else {
            setProfile(response.user);
        }
        } catch {
        setError("Unexpected error while fetching album details. Try again.");
        }
    };
    useEffect(() => {
        const fetchData = async () => {
            setLoading(true);
            if (id) await fetchUser(id);
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
                    {isAuthenticated && user.id === profile.id &&
                        <UserAboutForm onSubmit={handleSubmitAbout} />
                    }
                </div>
            </div>
        </div>
    )
}