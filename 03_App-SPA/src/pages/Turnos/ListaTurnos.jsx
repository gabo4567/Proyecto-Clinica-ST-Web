import { useEffect, useState } from 'react';
import api from '../../api/axiosconfig';
import { Link } from 'react-router-dom';


function ListaTurnos() {
  const [turnos, setTurnos] = useState([]);
  const [error, setError] = useState('');

  useEffect(() => {
    const obtenerTurnos = async () => {
      try {
        const response = await api.get('/Turnos');
        setTurnos(response.data);
      } catch (err) {
        console.error(err);
        setError('No se pudieron cargar los turnos.');
      }
    };

    obtenerTurnos();
  }, []);

  return (
    <div className="container mt-4">
      <h2 className="mb-4">Listado de Turnos</h2>

      {error && <p className="text-danger">{error}</p>}

      <table className="table table-bordered table-striped">
        <thead className="table-light">
          <tr>
            <th>ID</th>
            <th>Paciente</th>
            <th>Profesional</th>
            <th>Fecha</th>
            <th>Hora</th>
            <th>Motivo</th>
          </tr>
        </thead>
        <tbody>
          {turnos.map((turno) => (
            <tr key={turno.id}>
              <td>
                <Link to={`/turnos/${turno.id}`}>{turno.id}</Link>
              </td>
              <td>{turno.paciente?.nombre} {turno.paciente?.apellido}</td>
              <td>{turno.profesional?.nombre} {turno.profesional?.apellido}</td>
              <td>{turno.fechaHora?.substring(0, 10)}</td>
              <td>{turno.fechaHora?.substring(11, 16)}</td>
              <td>{turno.observaciones || 'Sin observaciones'}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default ListaTurnos;
