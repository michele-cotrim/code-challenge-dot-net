import React, { useState, useEffect } from 'react';
import { fetchCommunities } from '../api';

function EventSelector({ onSelectEvent }) {
    const [communities, setCommunities] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const getCommunities = async () => {
            try {
                const data = await fetchCommunities();
                setCommunities(data);
            } catch (err) {
                setError(err.message);
            } finally {
                setLoading(false);
            }
        };
        getCommunities();
    }, []);

    if (loading) return <p className="text-center text-gray-600">Loading events...</p>;
    if (error) return <p className="text-center text-red-500">Erro: {error}</p>;

    return (
        <div className="mb-8 p-6 bg-white rounded-lg shadow-md">
            <label htmlFor="event-select" className="block text-lg font-semibold text-gray-700 mb-2">
                Select an event:
            </label>
            <select
                id="event-select"
                className="block w-full p-3 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-blue-500 focus:border-blue-500 sm:text-md"
                onChange={(e) => onSelectEvent(e.target.value ? parseInt(e.target.value) : null)}
                defaultValue=""
            >
                <option value="" disabled>
                    -- Select --
                </option>
                {communities.map((community) => (
                    <option key={community.id} value={community.id}>
                        {community.name}
                    </option>
                ))}
            </select>
        </div>
    );
}

export default EventSelector;