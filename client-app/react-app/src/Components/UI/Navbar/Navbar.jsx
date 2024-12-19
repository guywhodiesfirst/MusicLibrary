import { useContext } from 'react'
import './Navbar.css'
import { Context } from '../../../App'
import { useNavigate } from 'react-router-dom'

export default function Navbar() {
    const { isAuthenticated, setIsAuthenticated, user } = useContext(Context);
    const navigate = useNavigate()

    const handleSignOut = () => {
        if(isAuthenticated) {
            localStorage.removeItem("access_token")
            setIsAuthenticated(false)
            navigate('/home')
        }
    }

    return (
        <nav className="navbar">
            <h1 className='navbar--logo' onClick={() => navigate("/home")}>cassette</h1>
            <ul className="navbar--menu">
                <li><a href="/albums">Albums</a></li>
                {isAuthenticated && <li><a href="/me">Account</a></li>}
                {/*user && user.is_admin && <li><a href="/admin">Admin panel</a></li>*/}
                {isAuthenticated ? <li onClick={handleSignOut}>Sign out</li> : <li><a href="/login">Sign in</a></li>}
            </ul>
        </nav>
    );
}