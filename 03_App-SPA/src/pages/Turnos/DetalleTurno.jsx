import { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import api from '../../api/axiosconfig';

function DetalleTurno() {
  const { id } = useParams();
  const [turno, setTurno] = useState(null);
  const [error, setError] = useState('');
  const [qrBase64, setQrBase64] = useState(null); // 👈 NUEVO
  const navigate = useNavigate();

  useEffect(() => {
    const obtenerTurno = async () => {
      try {
        const response = await api.get(`/Turnos/${id}`);
        setTurno(response.data);
      } catch (err) {
        console.error(err);
        setError('No se pudo cargar el turno.');
      }
    };

    obtenerTurno();
  }, [id]);

  const handleCancelar = async () => {
    const confirmacion = window.confirm("¿Estás seguro de cancelar este turno?");
    if (!confirmacion) return;

    try {
      await api.delete(`/Turnos/${id}`);
      alert("Turno cancelado correctamente.");
      navigate('/turnos');
    } catch (err) {
      console.error(err);
      setError('Error al cancelar el turno.');
    }
  };

  // 👇 NUEVO: función para generar QR
  const generarQr = async () => {
    try {
      const res = await api.get(`/Qr/generar/${id}`);
      setQrBase64(res.data);
    } catch (err) {
      console.error(err);
      setError("Error al generar el código QR.");
    }
  };

  if (error) return <p className="text-danger">{error}</p>;
  if (!turno) return <p>Cargando turno...</p>;

  return (
    <div className="container mt-4">
      <h2>Detalle del Turno</h2>
      <hr />
      <p><strong>ID:</strong> {turno.id}</p>
      <p><strong>Paciente:</strong> {turno.paciente.nombre} {turno.paciente.apellido}</p>
      <p><strong>Profesional:</strong> {turno.profesional.nombre} {turno.profesional.apellido}</p>
      <p><strong>Especialidad:</strong> {turno.profesional.especialidad}</p>
      <p><strong>Fecha:</strong> {turno.fechaHora?.substring(0, 10)}</p>
      <p><strong>Hora:</strong> {turno.fechaHora?.substring(11, 16)}</p>
      <p><strong>Duración:</strong> {turno.duracion} minutos</p>
      <p><strong>Estado:</strong> {turno.estado}</p>
      <p><strong>Observaciones:</strong> {turno.observaciones || 'Sin observaciones'}</p>

      <hr />

      <button className="btn btn-warning me-2" onClick={() => navigate(`/turnos/editar/${turno.id}`)}>
        Editar Turno
      </button>
      <button className="btn btn-danger me-2" onClick={handleCancelar}>
        Cancelar Turno
      </button>
      {/* 👇 NUEVO */}
      <button className="btn btn-primary me-2" onClick={generarQr}>
        Mostrar QR del Turno
      </button>

      {/* 👇 NUEVO: Mostrar QR si está disponible */}
      {qrBase64 && (
        <div className="mt-4">
          <h5>Código QR para confirmar:</h5>
          <img src={`data:image/png;base64,${qrBase64}`} alt="QR Turno" style={{ maxWidth: '250px' }} />
        </div>
      )}
    </div>
  );
}

export default DetalleTurno;
