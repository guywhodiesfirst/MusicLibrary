import React, { useContext, useState } from 'react';
import "./LoginPage.css";
import "../Auth.css"
import { Context } from '../../../App';
import { useNavigate } from 'react-router-dom';
import { AuthApi } from '../../../API/AuthApi';

export default function Login() {
    const [formData, setFormData] = useState({
        email: '',
        password: '',
    });

    const {isAuthenticated, setIsAuthenticated} = useContext(Context);
    const navigate = useNavigate();

    const handleChange = (event) => {
        const { name, value } = event.target;
        setFormData((prevData) => ({
            ...prevData,
            [name]: value,
        }));
    };

    const handleSubmit = async (event) => {
        event.preventDefault()
    
        const result = await AuthApi.login(formData)
    
        if (!result.success) {
            alert(result.message)
        } else {
            localStorage.setItem("access_token", result.data.access_token);
            alert("You have successfully authorized")
        }
    };

    const allFieldsFilled = Object.values(formData).every((field) => field.trim() !== "")
    
    return (
        <>
            {isAuthenticated && navigate('/albums')}
            <div className="auth--page">
                <div className="auth--wrapper">
                    <div className="auth--form-container">
                        <h2 className="auth--form-header">Sign in</h2>
                        <form className="auth--form" autoComplete="off" onSubmit={handleSubmit}>
                            <input
                                className="text-input"
                                type="email"
                                name="email"
                                placeholder="E-mail"
                                onChange={handleChange}
                                required
                            />
                            <input
                                className="text-input"
                                type="password"
                                name="password"
                                placeholder="Password"
                                onChange={handleChange}
                                required
                            />

                            <div className="auth--btn-container">
                                <button type="submit" className="auth--btn" disabled={!allFieldsFilled}>
                                    Sign in
                                </button>
                            </div>
                        </form>
                        <p className="auth--paragraph">
                            Don't have an account? <a href="/register">Sign up!</a>
                        </p>
                    </div>
                </div>
            </div>
        </>
    );
}