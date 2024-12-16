import "./HomePage.css"
import React, { useContext, useState } from 'react';
import { Context } from '../../App';
import { useNavigate } from "react-router-dom";
export default function HomePage() {
    const {isAuthenticated, setIsAuthenticated} = useContext(Context);
    const navigate = useNavigate();

    const handleButtonClick = (path) => {
        navigate(path);
    };
    return(
        <div className="home-page">
            <h1>Welcome to the Cassette!</h1>
            <img className="logo" src="logo.png" alt="logo"/>
            <div className="button-container">
                {!isAuthenticated && 
                    <button onClick={() => handleButtonClick('/login')}>
                        Log in
                    </button>
                }
                <button onClick={() => handleButtonClick('/albums')}>
                        Browse albums
                </button>
            </div>
        </div>
    )
}