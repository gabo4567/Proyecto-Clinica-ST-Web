import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import api from "../../api/axiosconfig";

function ConfirmarTurno() {
  const { token } = useParams();
  const [mensaje, setMensaje] = useState("Validando...");
  const [ok, setOk] = useState(false);

  useEffect(() => {
    const confirmar = async () => {
      try {
        const res = await api.get(`/Qr/confirmar/${token}`);
        setMensaje(res.data.mensaje || "Turno confirmado correctamente.");
        setOk(true);
      } catch (err) {
        setMensaje("El enlace es inválido o expiró.");
        setOk(false);
      }
    };

    confirmar();
  }, [token]);

  return (
    <div className="container mt-4">
      <h2>{ok ? "✅ Confirmación exitosa" : "❌ Error de confirmación"}</h2>
      <p className={ok ? "text-success" : "text-danger"}>{mensaje}</p>
    </div>
  );
}

export default ConfirmarTurno;
