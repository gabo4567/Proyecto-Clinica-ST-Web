import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import api from '../../api/axiosconfig';

function EditarTurno() {
  const { id } = useParams();
  const navigate = useNavigate();

  const [form, setForm] = useState({
    idPaciente: '',
    idProfesional: '',
    fechaHora: '',
    duracion: 30,
    observaciones: ''
  });

  const [profesionales, setProfesionales] = useState([]);
  const [error, setError] = useState('');

  useEffect(() => {
    const cargarTurnoYProfesionales = async () => {
      try {
        const [turnoRes, profRes] = await Promise.all([
          api.get(`/Turnos/${id}`),
          api.get('/Profesionales')
        ]);

        const turno = turnoRes.data;

        setForm({
          idPaciente: turno.paciente.id,
          idProfesional: turno.profesional.id,
          fechaHora: turno.fechaHora.substring(0, 16),
          duracion: turno.duracion,
          observaciones: turno.observaciones || ''
        });

        setProfesionales(profRes.data);
      } catch (err) {
        console.error(err);
        setError('Error al cargar datos del turno.');
      }
    };

    cargarTurnoYProfesionales();
  }, [id]);

  const handleChange = (e) => {
    setForm(prev => ({
      ...prev,
      [e.target.name]: e.target.value
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');

    try {
      console.log('Turno a enviar:', form);

      await api.put(`/Turnos/${id}`, {
        idPaciente: parseInt(form.idPaciente), // ✅ ahora lo mandamos
        idProfesional: parseInt(form.idProfesional),
        fechaHora: form.fechaHora,
        duracion: parseInt(form.duracion),
        observaciones: form.observaciones
      });

      navigate(`/turnos/${id}`);
    } catch (err) {
      console.error(err);
      setError('Error al actualizar el turno.');
    }
  };

  return (
    <div className="container mt-4">
      <h2>Editar Turno</h2>
      {error && <p className="text-danger">{error}</p>}
      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label className="form-label">Profesional</label>
          <select
            className="form-select"
            name="idProfesional"
            value={form.idProfesional}
            onChange={handleChange}
            required
          >
            <option value="">Seleccione un profesional</option>
            {profesionales.map(p => (
              <option key={p.id} value={p.id}>
                {p.nombre} {p.apellido} - {p.especialidad}
              </option>
            ))}
          </select>
        </div>

        <div className="mb-3">
          <label className="form-label">Fecha y hora</label>
          <input
            type="datetime-local"
            className="form-control"
            name="fechaHora"
            value={form.fechaHora}
            onChange={handleChange}
            required
          />
        </div>

        <div className="mb-3">
          <label className="form-label">Duración (minutos)</label>
          <input
            type="number"
            className="form-control"
            name="duracion"
            value={form.duracion}
            onChange={handleChange}
            required
            min={10}
          />
        </div>

        <div className="mb-3">
          <label className="form-label">Observaciones</label>
          <textarea
            className="form-control"
            name="observaciones"
            value={form.observaciones}
            onChange={handleChange}
          ></textarea>
        </div>

        <button type="submit" className="btn btn-success">Guardar cambios</button>
      </form>
    </div>
  );
}

export default EditarTurno;
