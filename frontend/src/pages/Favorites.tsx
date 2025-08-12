import { useEffect, useState } from 'react';
import api from '../api';

export default function Favorites() {
  const [list, setList] = useState<any[]>([]);

  const load = async () => {
    const res = await api.get('/favorites');
    setList(res.data);
  };

  useEffect(() => { load(); }, []);

  return (
    <div>
      <h2>My Favorites</h2>
      <div className="row">
        {list.map(p => (
          <div className="col-md-4 mb-3" key={p.id}>
            <div className="card">
              <div className="card-body">
                <h5>{p.title}</h5>
                <p>{p.address}</p>
                <p>${p.price}</p>
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}
