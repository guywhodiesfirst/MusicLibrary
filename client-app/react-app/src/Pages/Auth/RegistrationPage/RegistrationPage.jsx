import React, { useContext, useState } from 'react';
import "../Auth.css"
import "./RegistrationPage.css"
import { Context } from '../../../App';
import { useNavigate } from 'react-router-dom';
import { AuthApi } from '../../../API/AuthApi';

export default function RegistrationPage() {
    const [formData, setFormData] = useState({
        username: '',
        email: '',
        password: '',
        passwordConfirm: '',
    });

    const navigate = useNavigate();
    const { isAuthenticated } = useContext(Context);

    const handleChange = (event) => {
        const { name, value } = event.target;
        setFormData((prevData) => ({
            ...prevData,
            [name]: value,
        }));
    };

    const handleSubmit = async (event) => {
            event.preventDefault()
        
            const result = await AuthApi.register(formData)
        
            if (!result.success) {
                alert(result.message)
            } else {
                alert("Registration successful! You can now log into your account")
            }
        };

    const allFieldsFilled = Object.values(formData).every((field) => field.trim() !== '');

    return (
        <>
            {isAuthenticated && navigate('/catalog')}
            <div className="auth--page">
                <div className="auth--wrapper">
                    <div className="auth--form-container">
                        <h2 className="auth--form-header">Sign up</h2>
                        <form className="auth--form" autoComplete="off" onSubmit={handleSubmit}>
                            <input className="text-input" type="text" name="username" placeholder="Username" onChange={handleChange} required />
                            <input className="text-input" type="email" name="email" placeholder="E-mail" onChange={handleChange} required />
                            <div className="registration--form-block">
                                <input className="text-input" type="password" name="password" placeholder="Password" onChange={handleChange} required />
                                <input className="text-input" type="password" name="passwordConfirm" placeholder="Confirm password" onChange={handleChange} required />
                            </div>
                            <div className="auth--btn-container">
                                <button type="submit" className="btn auth--btn" disabled={!allFieldsFilled}>Sign up</button>
                            </div>
                        </form>
                        <p className='auth--paragraph'>Already have an account? <a href='/login'>Sign in!</a></p>
                    </div>
                </div>
            </div>
        </>
    );
}