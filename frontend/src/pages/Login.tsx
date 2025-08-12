import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../api';

export default function Login({ onLogin }) {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const navigate = useNavigate();

  const submit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const res = await api.post('/auth/login', { email, password });
        localStorage.setItem('jwt', res.data.token);
        onLogin(res.data.token);
      navigate('/');
    } catch (err: any) {
      setError(err.response?.data?.error || 'Login failed');
    }
  };

  return (
    <form onSubmit={submit} className="col-md-4 offset-md-4">
      <h2>Login</h2>
      {error && <div className="alert alert-danger">{error}</div>}
      <input className="form-control mb-2" placeholder="Email" value={email} onChange={e => setEmail(e.target.value)} />
      <input type="password" className="form-control mb-2" placeholder="Password" value={password} onChange={e => setPassword(e.target.value)} />
      <button className="btn btn-primary w-100">Login</button>
    </form>
  );
}
