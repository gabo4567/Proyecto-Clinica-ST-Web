import { useState, useContext } from 'react';
import api, { setAuthToken } from '../api/axiosconfig';
import { useNavigate } from 'react-router-dom';
import { AuthContext } from '../context/AuthContext';

function Login() {
  const [formData, setFormData] = useState({
    nombreUsuario: '',
    contrasena: ''
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

    try {
      const response = await api.post('/Auth/login', formData);
      const token = response.data.token;

      login(token); // usamos el contexto
      navigate('/');
    } catch (err) {
      console.error(err);
      setError('Usuario o contraseña incorrectos.');
    }
  };

  return (
    <div className="d-flex justify-content-center align-items-center vh-100 bg-dark">
      <div className="bg-white p-4 rounded shadow" style={{ width: '300px' }}>
        <h4 className="text-center mb-4">INICIAR SESIÓN</h4>

        <form onSubmit={handleSubmit}>
          <div className="mb-3">
            <input
              type="text"
              name="nombreUsuario"
              className="form-control"
              placeholder="Usuario o Email"
              value={formData.nombreUsuario}
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

          <div className="form-check mb-3">
            <input type="checkbox" className="form-check-input" id="recordarme" />
            <label className="form-check-label" htmlFor="recordarme">Recordarme</label>
          </div>

          <button type="submit" className="btn btn-primary w-100">INICIAR</button>
        </form>
         <div className="text-center mt-3">
            <a href="/registro">¿No tenés cuenta? Registrate acá</a>
         </div>


        {error && <p className="text-danger mt-3 text-center">{error}</p>}
      </div>
    </div>
  );
}

export default Login;
