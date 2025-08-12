import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import api from '../api';
import { Property } from '../types';

export default function PropertyDetail() {
  const { id } = useParams();
  const [prop, setProp] = useState<Property | null>(null);

  const load = async () => {
    const res = await api.get(`/properties/${id}`);
    setProp(res.data);
  };

  const toggleFavorite = async () => {
    try {
      await api.post(`/favorites/${id}`);
      alert('Toggled favorite');
    } catch (err: any) {
      alert(err.response?.data?.error || 'Action failed – make sure you are logged in.');
    }
  };

  useEffect(() => { load(); }, [id]);

  if (!prop) return <div>Loading...</div>;

  return (
    <div>
      <h2>{prop.title}</h2>
      <p>{prop.address}</p>
      <p>${prop.price}</p>
      <p>{prop.description}</p>
      <div className="d-flex">
        {prop.imageUrls.map((url, idx) => <img key={idx} src={url} alt="" style={{ width: '300px', marginRight: '10px' }} />)}
      </div>
      {localStorage.getItem('jwt') && <button className="btn btn-warning mt-3" onClick={toggleFavorite}>Toggle Favorite</button>}
    </div>
  );
}
