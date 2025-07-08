import { useState, useContext } from 'react';
import api from '../api/axiosconfig';
import { useNavigate } from 'react-router-dom';
import { AuthContext } from '../context/AuthContext';

function Registro() {
  const [formData, setFormData] = useState({
    nombreUsuario: '',
    email: '',
    contrasena: '',
    rol: 'Admin'
  });

  const [error, setError] = useState('');
  const navigate = useNavigate();
  const { login } = useContext(AuthContext);

  const handleChange = (e) => {
    setFormData(prev => ({
      ...prev,
      [e.target.name]: e.target.value
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');
    
    console.log('Enviando a Auth/registro →', formData);

    try {
      const response = await api.post('/Auth/registro', formData);
      const token = response.data.token;
      login(token);
      navigate('/');
    } catch (err) {
      console.error(err);
      setError('Error al registrarse. Verificá los datos ingresados.');
    }
  };

  return (
    <div className="d-flex justify-content-center align-items-center vh-100 bg-light">
      <div className="bg-white p-4 rounded shadow" style={{ width: '350px' }}>
        <h4 className="text-center mb-4">Registro de Usuario</h4>

        <form onSubmit={handleSubmit}>
          <div className="mb-3">
            <input
              type="text"
              name="nombreUsuario"
              className="form-control"
              placeholder="Usuario"
              value={formData.nombreUsuario}
              onChange={handleChange}
              required
            />
          </div>

          <div className="mb-3">
            <input
              type="email"
              name="email"
              className="form-control"
              placeholder="Correo electrónico"
              value={formData.email}
              onChange={handleChange}
              required
            />
          </div>

          <div className="mb-3">
            <input
              type="password"
              name="contrasena"
              className="form-control"
              placeholder="Contraseña"
              value={formData.contrasena}
              onChange={handleChange}
              required
            />
          </div>

          <button type="submit" className="btn btn-primary w-100">Registrarme</button>
        </form>

        {error && <p className="text-danger text-center mt-3">{error}</p>}
      </div>
    </div>
  );
}

export default Registro;
