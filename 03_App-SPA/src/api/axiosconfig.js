import axios from 'axios';

const api = axios.create({
  baseURL: 'http://192.168.1.35:5276/api', // Cambiar si usás ngrok o publicás online
});

// Para incluir token en headers automáticamente
export const setAuthToken = (token) => {
  if (token) {
    api.defaults.headers.common['Authorization'] = `Bearer ${token}`;
  } else {
    delete api.defaults.headers.common['Authorization'];
  }
};

export default api;
