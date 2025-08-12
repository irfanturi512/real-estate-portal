import { BrowserRouter, Routes, Route, Navigate, Link } from 'react-router-dom';
import { useState, useEffect } from 'react';
import Login from './pages/Login';
import Register from './pages/Register';
import Properties from './pages/Properties';
import PropertyDetail from './pages/PropertyDetail';
import Favorites from './pages/Favorites';

export default function App() {
    const [token, setToken] = useState<string | null>(null);

    // Load token from localStorage on mount
    useEffect(() => {
        setToken(localStorage.getItem('jwt'));
    }, []);

    const handleLogout = () => {
        localStorage.removeItem('jwt');
        setToken(null);
    };

    return (
        <BrowserRouter>
            <nav className="navbar navbar-expand navbar-dark bg-dark">
                <div className="container-fluid">
                    <Link to="/" className="navbar-brand">RealEstate</Link>
                    <div className="navbar-nav">
                        <Link to="/" className="nav-link">Properties</Link>
                        {token && <Link to="/favorites" className="nav-link">Favorites</Link>}
                        {!token && <Link to="/login" className="nav-link">Login</Link>}
                        {!token && <Link to="/register" className="nav-link">Register</Link>}
                        {token && (
                            <button className="btn btn-link nav-link" onClick={handleLogout}>
                                Logout
                            </button>
                        )}
                    </div>
                </div>
            </nav>
            <div className="container mt-4">
                <Routes>
                    <Route path="/" element={<Properties />} />
                    <Route path="/properties/:id" element={<PropertyDetail />} />
                    <Route
                        path="/login"
                        element={<Login onLogin={(jwt) => { localStorage.setItem('jwt', jwt); setToken(jwt); }} />}
                    />
                    <Route path="/register" element={<Register />} />
                    <Route path="/favorites" element={token ? <Favorites /> : <Navigate to="/login" />} />
                </Routes>
            </div>
        </BrowserRouter>
    );
}
