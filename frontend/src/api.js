const API_BASE_URL = 'https://localhost:7277/api'; 

export const fetchCommunities = async () => {
    const response = await fetch(`${API_BASE_URL}/Event/communities`);
    if (!response.ok) {
        throw new Error('Erro ao buscar comunidades.');
    }
    return response.json();
};

export const fetchPeople = async (communityId) => {
    const response = await fetch(`${API_BASE_URL}/Event/people/${communityId}`);
    if (!response.ok) {
        throw new Error('Erro ao buscar pessoas.');
    }
    return response.json();
};

export const checkInPerson = async (personId, communityId) => {
    const response = await fetch(`${API_BASE_URL}/Event/check-in/${personId}/${communityId}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
    });
    if (!response.ok) {
        throw new Error('Erro ao realizar check-in.');
    }
};

export const checkOutPerson = async (personId, communityId) => {
    const response = await fetch(`${API_BASE_URL}/Event/check-out/${personId}/${communityId}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
    });
    if (!response.ok) {
        throw new Error('Erro ao realizar check-out.');
    }
};

export const fetchEventSummary = async (communityId) => {
    const response = await fetch(`${API_BASE_URL}/Event/summary/${communityId}`);
    if (!response.ok) {
        throw new Error('Erro ao buscar resumo do evento.');
    }
    return response.json();
};

export const checkAllowCheckout = async (personId, communityId) => {
    const response = await fetch(`${API_BASE_URL}/Event/allow/check-out/${personId}/${communityId}`);
    if (!response.ok) {
        console.error(`Erro ao verificar permissão de checkout: ${response.status} - ${response.statusText}`);
        return false; // Assume que não permite em caso de erro
    }
    // A API deve retornar um booleano (true/false)
    return response.json();
};

export const checkAllowCheckin = async (personId, communityId) => {
    const response = await fetch(`${API_BASE_URL}/Event/allow/check-in/${personId}/${communityId}`);
    if (!response.ok) {
        console.error(`Erro ao verificar permissão de check-in: ${response.status} - ${response.statusText}`);
        // Se houver um erro na API, assumimos que não permite por segurança,
        // ou você pode tratar de forma diferente dependendo do tipo de erro.
        return false;
    }
    // A API deve retornar um booleano (true/false)
    return response.json();
};