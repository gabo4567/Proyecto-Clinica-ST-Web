import { createContext, useState, useEffect } from 'react';
import { setAuthToken } from '../api/axiosconfig';

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [usuario, setUsuario] = useState(null);

  useEffect(() => {
    const token = localStorage.getItem('token');
    if (token) {
      setUsuario({ token }); // podés guardar más info si querés
      setAuthToken(token);
    }
  }, []);

  const login = (token) => {
    localStorage.setItem('token', token);
    setAuthToken(token);
    setUsuario({ token });
  };

  const logout = () => {
    localStorage.removeItem('token');
    setAuthToken(null);
    setUsuario(null);
  };

  return (
    <AuthContext.Provider value={{ usuario, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};
