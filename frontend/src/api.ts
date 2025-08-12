import axios from 'axios';

const api = axios.create({
    baseURL: 'http://localhost:50928',
});

api.interceptors.request.use((config) => {
  const token = localStorage.getItem('jwt');
  if (token && config.headers) config.headers.Authorization = `Bearer ${token}`;
  return config;
});

export default api;
