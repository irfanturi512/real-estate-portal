import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import api from '../api';
import { Property } from '../types';

export default function Properties() {
  const [list, setList] = useState<Property[]>([]);
  const [filters, setFilters] = useState({ suburb: '', minPrice: '', maxPrice: '', listingType: '' });

  const load = async () => {
    const params: any = {};
    if (filters.suburb) params.suburb = filters.suburb;
    if (filters.minPrice) params.minPrice = filters.minPrice;
    if (filters.maxPrice) params.maxPrice = filters.maxPrice;
    if (filters.listingType) params.listingType = filters.listingType; 

    const res = await api.get('/properties', { params });
    setList(res.data);
  };

  useEffect(() => { load(); }, []);

  return (
    <div>
      <h2>Properties</h2>
      <div className="row mb-3 g-2">
        <input placeholder="Suburb" className="form-control col" value={filters.suburb} onChange={e => setFilters({ ...filters, suburb: e.target.value })} />
        <input placeholder="Min Price" className="form-control col" value={filters.minPrice} onChange={e => setFilters({ ...filters, minPrice: e.target.value })} />
        <input placeholder="Max Price" className="form-control col" value={filters.maxPrice} onChange={e => setFilters({ ...filters, maxPrice: e.target.value })} />
        <select
                  className="form-control col"
                  value={filters.listingType}
                  onChange={e => setFilters({ ...filters, listingType: e.target.value })}
              >
                  <option value="">All Types</option>
                  <option value="Sale">Sale</option>
                  <option value="Rent">Rent</option>
              </select>
      <button onClick={load} className="btn btn-secondary col">Search</button>
      </div>
      <div className="row">
        {list.map(p => (
          <div className="col-md-4 mb-3" key={p.id}>
            <div className="card">
              {p.imageUrls.length > 0 && <img src={p.imageUrls[0]} className="card-img-top" alt={p.title} />}
              <div className="card-body">
                <h5>{p.title}</h5>
                <p>{p.address}</p>
                <p>${p.price}</p>
                <p><strong>Type:</strong> {p.listingType}</p>
                <Link to={`/properties/${p.id}`} className="btn btn-primary">View</Link>
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}
