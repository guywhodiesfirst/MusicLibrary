import { useParams } from "react-router-dom";
import { useContext, useState, useEffect } from "react";
import { Context } from "../../App";
import './ProfilePage.css'
import UserInfo from "../../Components/UserInfo/UserInfo";
import { UsersApi } from "../../API/UsersApi";

export default function ProfilePage() {
    const { id } = useParams();
    const [user, setUser] = useState(null)
    const [loading, setLoading] = useState(true)
    const [error, setError] = useState('')
    const fetchUser = async (userId) => {
        try {
        const response = await UsersApi.getUserDetails(id);
        if (!response.success) {
            setError(response.message);
        } else {
            console.log(response)
            setUser(response.user);
        }
        } catch {
        setError("Unexpected error while fetching album details. Try again.");
        }
    };
    useEffect(() => {
        const fetchData = async () => {
            setLoading(true);
            if (id) await fetchUser(id); // Передаємо id до fetchUser
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
        <>
            <UserInfo user={user} />
        </>
    )
}