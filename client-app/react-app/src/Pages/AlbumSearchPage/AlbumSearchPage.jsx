
import React, { useContext, useState } from 'react';
import { Context } from '../../App';
import { useNavigate } from "react-router-dom";
export default function AlbumSearchPage() {
    const {isAuthenticated, setIsAuthenticated} = useContext(Context);
    const navigate = useNavigate();

    const handleButtonClick = (path) => {
        navigate(path);
    };
    return(
       <h1>Album search</h1>
    )
}