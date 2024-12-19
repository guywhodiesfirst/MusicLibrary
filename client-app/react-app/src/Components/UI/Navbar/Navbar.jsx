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
            <div className='navbar--logo-container' onClick={() => navigate("/home")}>
                <img className="navbar--logo-img" src="/logo.png" alt="logo"/>
                <h1 className='navbar--logo'>cassette</h1>
            </div>
            <ul className="navbar--menu">
                <li><a href="/albums">Albums</a></li>
                {isAuthenticated && <li><a href="/me">Account</a></li>}
                {/*user && user.is_admin && <li><a href="/admin">Admin panel</a></li>*/}
                {isAuthenticated ? <li onClick={handleSignOut}>Sign out</li> : <li><a href="/login">Sign in</a></li>}
            </ul>
        </nav>
    );
}