import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../../api/axiosconfig';

function NuevoTurno() {
  const [dniPaciente, setDniPaciente] = useState('');
  const [paciente, setPaciente] = useState(null);
  const [profesionales, setProfesionales] = useState([]);
  const [form, setForm] = useState({
    idProfesional: '',
    fecha: '',
    hora: '',
    duracion: 30,
    observaciones: ''
  });
  const [error, setError] = useState('');
  const navigate = useNavigate();

  useEffect(() => {
  const cargarProfesionales = async () => {
    try {
      const response = await api.get('/Profesionales');

      // Filtramos profesionales con id > 0 y sin duplicados
      const unicos = response.data
        .filter(p => p.id > 0)
        .filter((p, index, self) =>
          self.findIndex(x => x.id === p.id) === index
        );

      setProfesionales(unicos);
    } catch (err) {
      console.error(err);
    }
  };

  cargarProfesionales();
}, []);

  const buscarPaciente = async () => {
    try {
      const response = await api.get(`/Pacientes/${dniPaciente}`);
      setPaciente(response.data);
      setError('');
    } catch (err) {
      setPaciente(null);
      setError('Paciente no encontrado');
    }
  };

  const handleChange = (e) => {
    setForm(prev => ({ ...prev, [e.target.name]: e.target.value }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');

    if (!paciente) {
      setError('Debe seleccionar un paciente válido');
      return;
    }

    const fechaHora = `${form.fecha}T${form.hora}`;
    console.log("Turno a enviar:", {
    idPaciente: paciente.id,
    idProfesional: form.idProfesional,
    fechaHora: `${form.fecha}T${form.hora}`,
    duracion: parseInt(form.duracion),
    observaciones: form.observaciones 
   });

    try {
      await api.post('/Turnos', {
        idPaciente: paciente.id,
       idProfesional: parseInt(form.idProfesional),
        fechaHora,
        duracion: parseInt(form.duracion),
        observaciones: form.observaciones
      });

      alert('Turno creado correctamente');
      navigate('/turnos');
    } catch (err) {
      console.error(err);
      setError('Error al crear el turno');
    }
  };

  return (
    <div className="container mt-4">
      <h2>Nuevo Turno</h2>
      <form onSubmit={handleSubmit} className="mt-4">

        <div className="mb-3 d-flex">
          <input
            type="text"
            className="form-control me-2"
            placeholder="DNI del paciente"
            value={dniPaciente}
            onChange={(e) => setDniPaciente(e.target.value)}
            required
          />
          <button type="button" className="btn btn-secondary" onClick={buscarPaciente}>
            Buscar
          </button>
        </div>

        {paciente && (
          <div className="mb-3">
            <strong>Paciente:</strong> {paciente.nombre} {paciente.apellido}
          </div>
        )}

        <div className="mb-3">
          <label>Profesional:</label>
          <select
            className="form-select"
            name="idProfesional"
            value={form.idProfesional}
            onChange={handleChange}
            required
          >
            <option value="">Seleccionar profesional</option>
            {profesionales.map(p => (
             <option key={`${p.id}-${p.nombre}`} value={p.id}>
             {p.nombre} {p.apellido} - {p.especialidad}
             </option>
            ))}

          </select>
        </div>

        <div className="mb-3">
          <label>Fecha:</label>
          <input
            type="date"
            name="fecha"
            className="form-control"
            value={form.fecha}
            onChange={handleChange}
            required
          />
        </div>

        <div className="mb-3">
          <label>Hora:</label>
          <input
            type="time"
            name="hora"
            className="form-control"
            value={form.hora}
            onChange={handleChange}
            required
          />
        </div>

        <div className="mb-3">
          <label>Duración (minutos):</label>
          <input
            type="number"
            name="duracion"
            className="form-control"
            value={form.duracion}
            onChange={handleChange}
            min="5"
            required
          />
        </div>

        <div className="mb-3">
          <label>Observaciones:</label>
          <textarea
            name="observaciones"
            className="form-control"
            value={form.observaciones}
            onChange={handleChange}
          ></textarea>
        </div>

        <button type="submit" className="btn btn-primary">Guardar Turno</button>

        {error && <p className="text-danger mt-3">{error}</p>}
      </form>
    </div>
  );
}

export default NuevoTurno;
