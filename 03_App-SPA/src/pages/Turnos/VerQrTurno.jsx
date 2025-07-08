import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import api from '../../api/axiosconfig';

function VerQrTurno() {
  const { id } = useParams();
  const [qrBase64, setQrBase64] = useState('');
  const [error, setError] = useState('');

  useEffect(() => {
    const obtenerQr = async () => {
      try {
        const response = await api.get(`/Qr/generar/${id}`);
        setQrBase64(response.data); // El backend devuelve un string base64
      } catch (err) {
        console.error(err);
        setError('Error al generar el código QR');
      }
    };

    obtenerQr();
  }, [id]);

  return (
    <div className="container mt-4">
      <h2>Código QR del Turno</h2>
      <p>Este código puede ser escaneado para confirmar el turno</p>

      {error && <p className="text-danger">{error}</p>}

      {qrBase64 && (
        <img
          src={`data:image/png;base64,${qrBase64}`}
          alt="Código QR del turno"
          className="img-fluid"
        />
      )}
    </div>
  );
}

export default VerQrTurno;
