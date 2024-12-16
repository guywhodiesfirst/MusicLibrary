import React, { useState, useEffect } from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate, useLocation } from 'react-router-dom';
import './App.css';
import HomePage from './Pages/HomePage/HomePage';
import LoginPage from './Pages/Auth/LoginPage/LoginPage';
import AlbumSearchPage from './Pages/AlbumSearchPage/AlbumSearchPage';
import { UsersApi } from './API/UsersApi';
import RegistrationPage from './Pages/Auth/RegistrationPage/RegistrationPage';
import Navbar from './Components/UI/Navbar/Navbar';

export const Context = React.createContext();

export default function AppWrapper() {
  return (
    <Router>
      <App />
    </Router>
  );
}

function App() {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [user, setUser] = useState(null);
  const [loading, setLoading] = useState(true);
  const location = useLocation();

  useEffect(() => {
    const token = localStorage.getItem('access_token');
    if (token) {
      setIsAuthenticated(true);
      fetchUserProfile().finally(() => setLoading(false));
    } else {
      setIsAuthenticated(false);
      setLoading(false);
    }
  }, []);

  const fetchUserProfile = async () => {
    const token = localStorage.getItem('access_token');
    if (token) {
      const result = await UsersApi.getCurrentUser();
      if (!result.success) {
        console.error(result.message);
        localStorage.removeItem('access_token');
        setIsAuthenticated(false);
        setUser(null);
      } else {
        console.log(result.data);
        setUser(result.data);
        setIsAuthenticated(true);
      }
    }
  };

  const isHomePage = location.pathname === '/home';

  if (loading) return <div>Loading...</div>;

  return (
    <Context.Provider value={{ isAuthenticated, setIsAuthenticated, user }}>
      {!isHomePage && <Navbar />}
        <div className='wrapper'>
        <Routes>
          <Route path="/" element={<Navigate to="/home" />} />
          <Route path="*" element={<Navigate to="/home" />} />
          <Route path="/home" element={<HomePage />} />
          <Route path="/login" element={<LoginPage />} />
          <Route path="/register" element={<RegistrationPage />} />
          <Route path="/albums" element={<AlbumSearchPage />} />
        </Routes>
      </div>
    </Context.Provider>
  );
}
