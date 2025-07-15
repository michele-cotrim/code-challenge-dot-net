// src/App.js
import React, { useState } from 'react';
import EventSelector from './components/EventSelector';
import EventDetails from './components/EventDetails.jsx';
import CheckinControls from './components/CheckinControls';

function App() {
    const [selectedCommunityId, setSelectedCommunityId] = useState(null);
    const [selectedPerson, setSelectedPerson] = useState(null); // Armazena o objeto da pessoa selecionada

    // Função para lidar com a seleção da pessoa
    const handleSelectPerson = (person) => {
        setSelectedPerson(person);
    };

    return (
        <div className="min-h-screen bg-gray-100 p-8">
            <div className="max-w-4xl mx-auto space-y-8">
                <h1 className="text-4xl font-extrabold text-gray-900 text-center mb-10">
                    Gerenciamento de Eventos
                </h1>

                {/* Parte 1: Seleção de Evento */}
                <EventSelector onSelectEvent={setSelectedCommunityId} />

                {/* Parte 2: Detalhes do Evento e Seleção de Pessoa */}
                {selectedCommunityId && (
                    <EventDetails
                        communityId={selectedCommunityId}
                        onSelectPerson={handleSelectPerson}
                    // onUpdateLastCheckinPerson REMOVIDA
                    />
                )}

                {/* Parte 3: Botões de Check-in/Check-out */}
                {selectedCommunityId && selectedPerson && ( // Verifica se selectedPerson existe
                    <CheckinControls
                        personId={selectedPerson.id} // Passa o ID da pessoa selecionada
                        personName={selectedPerson.fullName} // Passa o nome da pessoa selecionada
                        communityId={selectedCommunityId}
                    // lastCheckinPersonId e lastCheckinPersonName REMOVIDOS
                    />
                )}

                {!selectedCommunityId && (
                    <p className="text-center text-xl text-gray-600 mt-10">
                        Comece selecionando um evento acima.
                    </p>
                )}
            </div>
        </div>
    );
}

export default App;