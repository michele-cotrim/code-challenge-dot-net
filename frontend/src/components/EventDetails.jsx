// src/components/EventDetails.js
import React, { useState, useEffect } from 'react';
import { fetchEventSummary, fetchPeople } from '../api';

function EventDetails({ communityId, onSelectPerson }) { // onUpdateLastCheckinPerson REMOVIDA
    const [summary, setSummary] = useState(null);
    const [people, setPeople] = useState([]);
    const [loadingSummary, setLoadingSummary] = useState(false);
    const [loadingPeople, setLoadingPeople] = useState(false);
    const [errorSummary, setErrorSummary] = useState(null);
    const [errorPeople, setErrorPeople] = useState(null);

    // Atualiza o resumo do evento a cada 2 segundos
    useEffect(() => {
        let intervalId;
        if (communityId) {
            const getSummary = async () => {
                setLoadingSummary(true);
                try {
                    const data = await fetchEventSummary(communityId);
                    setSummary(data);
                    setErrorSummary(null);
                    // CHAMADA DE onUpdateLastCheckinPerson REMOVIDA AQUI
                } catch (err) {
                    setErrorSummary(err.message);
                } finally {
                    setLoadingSummary(false);
                }
            };

            getSummary();
            intervalId = setInterval(getSummary, 2000);
        } else {
            setSummary(null);
            // LIMPEZA DE onUpdateLastCheckinPerson REMOVIDA AQUI
        }

        return () => {
            if (intervalId) {
                clearInterval(intervalId);
            }
        };
    }, [communityId]); // onUpdateLastCheckinPerson REMOVIDA DAS DEPENDÊNCIAS

    // Busca a lista de pessoas quando o communityId é alterado
    useEffect(() => {
        const getPeople = async () => {
            if (communityId) {
                setLoadingPeople(true);
                try {
                    const data = await fetchPeople(communityId);
                    setPeople(data);
                    setErrorPeople(null);
                } catch (err) {
                    setErrorPeople(err.message);
                } finally {
                    setLoadingPeople(false);
                }
            } else {
                setPeople([]);
            }
        };
        getPeople();
    }, [communityId]);

    if (!communityId) {
        return <p className="text-center text-gray-500">Selecione um evento para ver os detalhes.</p>;
    }

    const handlePersonSelect = (e) => {
        const personId = e.target.value ? parseInt(e.target.value) : null;
        const selectedPersonObject = people.find(p => p.id === personId);
        onSelectPerson(selectedPersonObject);
    };

    return (
        <div className="mb-8 p-6 bg-white rounded-lg shadow-md">
            <h2 className="text-2xl font-bold text-gray-800 mb-4">Event information</h2>

            {loadingSummary && <p className="text-center text-gray-600">Loading event summary...</p>}
            {errorSummary && <p className="text-center text-red-500">Fail to load summary: {errorSummary}</p>}

            {summary && (
                <>
                    <p className="text-gray-700 mb-2">
                        <span className="font-semibold">People not checked-in:</span> {summary.peopleNotCheckedIn}
                    </p>
                    <p className="text-gray-700 mb-2">
                        <span className="font-semibold">Currente Attendee:</span> {summary.currentAttendeeCount}
                    </p>
                    {summary.lastCheckinPersonName && (
                        <p className="text-gray-700 mb-2">
                            <span className="font-semibold">Time of last check-in:</span> {summary.lastCheckinPersonName}
                            {summary.lastCheckin && ` (${new Date(summary.lastCheckin).toLocaleTimeString()})`}
                        </p>
                    )}

                    <h3 className="text-xl font-semibold text-gray-700 mt-6 mb-3">Companies details</h3>
                    {summary.companyBreakdown && summary.companyBreakdown.length > 0 ? (
                        <ul className="list-disc list-inside text-gray-700">
                            {summary.companyBreakdown.map((item, index) => (
                                <li key={index}>
                                    {item.company}: {item.count}
                                </li>
                            ))}
                        </ul>
                    ) : (
                        <p className="text-gray-500">None company detail available.</p>
                    )}
                </>
            )}

            <hr className="my-6 border-gray-200" />

            <label htmlFor="person-select" className="block text-lg font-semibold text-gray-700 mb-2">
                Select someone to Check-in/Check-out:
            </label>
            {loadingPeople && <p className="text-center text-gray-600">Loading guest list...</p>}
            {errorPeople && <p className="text-center text-red-500">Fail to load guest list: {errorPeople}</p>}
            <select
                id="person-select"
                className="block w-full p-3 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-blue-500 focus:border-blue-500 sm:text-md"
                onChange={handlePersonSelect}
                defaultValue=""
            >
                <option value="" disabled>
                    -- Select someone --
                </option>
                {people.map((person) => (
                    <option key={person.id} value={person.id}>
                        {person.fullName} ({person.companyName})
                    </option>
                ))}
            </select>
        </div>
    );
}

export default EventDetails;