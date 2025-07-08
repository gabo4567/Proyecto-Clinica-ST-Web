import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { useContext } from 'react';
import { AuthContext } from './context/AuthContext';

import Navbar from './components/Navbar';
import PrivateRoute from './routes/PrivateRoute';

import Home from './pages/Home';
import Login from './pages/Login';
import Registro from './pages/Registro';
import ListaTurnos from './pages/Turnos/ListaTurnos';
import DetalleTurno from './pages/Turnos/DetalleTurno';
import NuevoTurno from './pages/Turnos/NuevoTurno';
import EditarTurno from './pages/Turnos/EditarTurno';
import VerQrTurno from './pages/Turnos/VerQrTurno';
import ConfirmarTurno from './pages/Turnos/ConfirmarTurno';

function App() {
  const { usuario } = useContext(AuthContext);

  return (
    <Router>
      {usuario && <Navbar />} {/* ✅ Solo se muestra si hay usuario logueado */}

      <Routes>
        {/* Ruta protegida: Home */}
        <Route
          path="/"
          element={
            <PrivateRoute>
              <div className="p-4">
                <h1>Salud Total SPA</h1>
                <Home />
              </div>
            </PrivateRoute>
          }
        />

        {/* Ruta protegida: Listado de turnos */}
        <Route
          path="/turnos"
          element={
            <PrivateRoute>
              <ListaTurnos />
            </PrivateRoute>
          }
        />

        {/* Ruta protegida: Detalle de un turno */}
        <Route
          path="/turnos/:id"
          element={
            <PrivateRoute>
              <DetalleTurno />
            </PrivateRoute>
          }
        />
        <Route
          path="/turnos/nuevo"
          element={
            <PrivateRoute>
              <NuevoTurno />
            </PrivateRoute>
          }
        />
        <Route path="/turnos/editar/:id" element={<EditarTurno />} />
        <Route path="/turnos/qr/:id" element={<VerQrTurno />} />
        <Route path="/confirmar-turno/:token" element={<ConfirmarTurno />} />




        {/* Rutas públicas */}
        <Route path="/login" element={<Login />} />
        <Route path="/registro" element={<Registro />} />
      </Routes>
    </Router>
  );
}

export default App;
