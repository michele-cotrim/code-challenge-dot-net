// src/components/CheckinControls.js
import React, { useState, useEffect, useRef } from 'react';
import { checkInPerson, checkOutPerson, checkAllowCheckout, checkAllowCheckin } from '../api';

function CheckinControls({
    personId,
    personName,
    communityId,
}) {
    const [canCheckout, setCanCheckout] = useState(false);
    const [canCheckin, setCanCheckin] = useState(false);
    const [checkinLoading, setCheckinLoading] = useState(false); // Mant�m o estado de loading
    const [checkoutLoading, setCheckoutLoading] = useState(false);
    const [checkinError, setCheckinError] = useState(null);
    const [checkoutError, setCheckoutError] = useState(null);
    const [successMessage, setSuccessMessage] = useState(null);

    const intervalRef = useRef(null);

    useEffect(() => {
        if (intervalRef.current) {
            clearInterval(intervalRef.current);
        }

        if (personId && communityId) {
            intervalRef.current = setInterval(async () => {
                try {
                    const allowed = await checkAllowCheckout(personId, communityId);
                    setCanCheckout(allowed);
                    setCheckoutError(null);
                } catch (err) {
                    console.error("Error to validate check-out permission", err);
                    setCheckoutError('Error to validate check-out permission.');
                    setCanCheckout(false);
                }
            }, 500);
        } else {
            setCanCheckout(false);
        }

        return () => {
            if (intervalRef.current) {
                clearInterval(intervalRef.current);
                intervalRef.current = null;
            }
        };
    }, [personId, communityId]);

    useEffect(() => {
        const validateCheckinPermission = async () => {
            setCheckinError(null);
            if (personId && communityId) {
                try {
                    const allowed = await checkAllowCheckin(personId, communityId);
                    setCanCheckin(allowed);
                    if (!allowed) {
                        setCheckinError('Check-in already done.');
                    }
                } catch (err) {
                    console.error("Erro ao validar permiss�o de check-in:", err);
                    setCheckinError('Falha ao verificar permiss�o de check-in.');
                    setCanCheckin(false);
                }
            } else {
                // Se n�o h� sele��o, o bot�o de check-in estar� desabilitado pela l�gica de `isCheckinDisabled`
                // Mas a permiss�o l�gica para a API ser� true, at� que uma sele��o seja feita.
                setCanCheckin(true);
            }
        };

        validateCheckinPermission();
    }, [personId, communityId]);

    useEffect(() => {
        setSuccessMessage(null);
        setCheckoutError(null);
    }, [personId, communityId]);


    const handleCheckin = async () => {
        // DESABILITA O BOT�O IMEDIATAMENTE AO CLICAR
        setCheckinLoading(true); // <--- MUDAN�A AQUI: MOVIDO PARA O IN�CIO

        if (!personId || !communityId) {
            setCheckinError('Please, you must select an event and a person to do the check-in');
            setCheckinLoading(false); // Reabilita se a valida��o inicial falhar
            return;
        }
        if (!canCheckin) {
            setCheckinError('Check-in already done.');
            setCheckinLoading(false); // Reabilita se a valida��o inicial falhar
            return;
        }

        setCheckinError(null);
        setSuccessMessage(null);

        try {
            await checkInPerson(personId, communityId);
            setSuccessMessage(`Check-in of ${personName} completed sucessfuly!`);
            setCanCheckin(false); // Impede novo check-in para esta pessoa ap�s sucesso
        } catch (err) {
            setCheckinError(err.message);
        } finally {
            setCheckinLoading(false); // Reabilita o bot�o (ou o deixa desabilitado por !canCheckin)
        }
    };

    const handleCheckout = async () => {
        if (!personId || !communityId) {
            setCheckoutError('You must select an event and a person to do the check-out.');
            return;
        }
        if (!canCheckout) {
            setCheckoutError('Checkout not available');
            return;
        }

        setCheckoutLoading(true);
        setCheckoutError(null);
        setSuccessMessage(null);

        try {
            await checkOutPerson(personId, communityId);
            setSuccessMessage(`Check-out de ${personName} realizado com sucesso!`);
            setCanCheckout(false);
        } catch (err) {
            setCheckoutError(err.message);
        } finally {
            setCheckoutLoading(false);
        }
    };

    const checkinButtonText = checkinLoading
        ? 'Realizando Check-in...'
        : `Check-in ${personName ? `de ${personName}` : ''}`;

    const checkoutButtonText = checkoutLoading
        ? 'Realizando Check-out...'
        : 'Check-out';

    const isCheckinDisabled = checkinLoading || !personId || !communityId || !canCheckin;
    const isCheckoutDisabled = checkoutLoading || !canCheckout || !personId || !communityId;

    return (
        <div className="p-6 bg-white rounded-lg shadow-md">
            <h2 className="text-2xl font-bold text-gray-800 mb-4">Controle de Check-in/Check-out</h2>

            {successMessage && (
                <div className="bg-green-100 border border-green-400 text-green-700 px-4 py-3 rounded relative mb-4" role="alert">
                    {successMessage}
                </div>
            )}

            {checkinError && (
                <div className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded relative mb-4" role="alert">
                    {checkinError}
                </div>
            )}

            {checkoutError && (
                <div className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded relative mb-4" role="alert">
                    {checkoutError}
                </div>
            )}

            <button
                onClick={handleCheckin}
                disabled={isCheckinDisabled}
                className={`w-full py-3 px-4 rounded-md text-white font-semibold transition duration-300 ${isCheckinDisabled
                        ? 'bg-gray-400 cursor-not-allowed'
                        : 'bg-blue-600 hover:bg-blue-700'
                    }`}
            >
                {checkinButtonText}
            </button>

            <button
                onClick={handleCheckout}
                disabled={isCheckoutDisabled}
                className={`mt-4 w-full py-3 px-4 rounded-md text-white font-semibold transition duration-300 ${isCheckoutDisabled
                        ? 'bg-gray-400 cursor-not-allowed'
                        : 'bg-red-600 hover:bg-red-700'
                    }`}
            >
                {checkoutButtonText}
            </button>
        </div>
    );
}

export default CheckinControls;