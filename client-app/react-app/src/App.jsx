import React, { useState, useEffect } from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import './App.css';
import HomePage from './Pages/HomePage/HomePage';
import LoginPage from './Pages/Auth/LoginPage/LoginPage'
import AlbumSearchPage from './Pages/AlbumSearchPage/AlbumSearchPage'
import { UsersApi } from './API/UsersApi';

export const Context = React.createContext();

export default function App() {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [user, setUser] = useState(null);
  const [loading, setLoading] = useState(true);

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
        setUser(result.data);
        setIsAuthenticated(true);
      }
    }
  };

  if (loading) return <div>Loading...</div>;

  return (
    <Router>
      <Context.Provider value={{ isAuthenticated, setIsAuthenticated, user }}>
        <Routes>
          <Route path="/" element={<Navigate to="/home" />} />
          <Route path="*" element={<Navigate to="/home" />} />
          <Route path="/home" element={<HomePage />} />
          <Route path="/login" element={<LoginPage/>}/>
          <Route path="/albums" element={<AlbumSearchPage/>}/>
        </Routes>
      </Context.Provider>
    </Router>
  );
}