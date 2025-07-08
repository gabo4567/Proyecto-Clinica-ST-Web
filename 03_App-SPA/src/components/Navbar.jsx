import { Link, useNavigate } from 'react-router-dom';
import { useContext } from 'react';
import { AuthContext } from '../context/AuthContext';

function Navbar() {
  const { usuario, logout } = useContext(AuthContext);
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate('/login');
  };

  if (!usuario) return null; // No mostrar navbar si no está logueado

  return (
    <nav className="navbar navbar-expand-lg navbar-light bg-light px-4">
      <Link className="navbar-brand" to="/">Salud Total</Link>
      <div className="ml-auto">
        <Link to="/turnos" className="btn btn-link me-2">Turnos</Link>
        <Link to="/turnos/nuevo" className="btn btn-success me-2">
          Nuevo Turno
        </Link>

        <button onClick={handleLogout} className="btn btn-outline-danger">
          Cerrar sesión
        </button>
      </div>
    </nav>
  );
}

export default Navbar;
