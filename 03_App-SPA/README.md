# ⚛️ Aplicación SPA - Clínica Salud Total

Este módulo corresponde a la **Aplicación Web SPA (Single Page Application)** del sistema **Clínica Salud Total**, desarrollada con tecnologías modernas del ecosistema frontend. Esta interfaz está pensada para pacientes o usuarios que desean realizar operaciones de forma rápida, fluida y responsiva desde el navegador.

---

## 🛠️ Tecnologías utilizadas

- ⚛️ React 18
- 📦 Vite
- 📡 Axios
- 🌐 React Router
- 🧠 React Context
- 💅 TailwindCSS (u otro framework de diseño)
- 🔐 JWT (para autenticación)

---

## 🧩 Funcionalidades implementadas

| Módulo                | Descripción                                                                |
|------------------------|----------------------------------------------------------------------------|
| 🔐 Login               | Inicio de sesión mediante usuario y contraseña (token JWT)                 |
| 🏠 Home                | Pantalla de inicio con navegación hacia secciones principales              |
| 📅 Turnos              | Listado de turnos del paciente, con paginación y búsqueda                  |
| 👁️ Detalle de turno    | Vista individual con toda la información del turno                         |
| ➕ Crear/Editar turnos | Formulario dinámico para crear o modificar turnos (con dropdowns)          |
| 📸 Código QR           | Visualización del QR generado desde la API para validar el turno           |
| 📥 Guardado del token  | Almacenamiento en `localStorage` para mantener la sesión activa            |
| ⚠️ Mensajes y errores  | Manejo amigable de errores y respuestas del servidor                       |

---

## 📁 Estructura general del proyecto

```

📦 /04\_App-SPA
├── public/
├── src/
│   ├── api/              # Conexión a la API (axios, interceptores)
│   ├── components/       # Componentes reutilizables (Botón, Loader, etc.)
│   ├── context/          # AuthContext o contextos globales
│   ├── pages/            # Páginas principales (Turnos, Login, Home, etc.)
│   ├── routes/           # Definición de rutas con React Router
│   ├── styles/           # Archivos CSS o Tailwind config
│   └── main.jsx
├── .env
├── index.html
└── package.json

````

---

## 🧪 Cómo correr la SPA localmente

1. Instalar dependencias:

```bash
npm install
````

2. Crear archivo `.env` con la URL base de la API:

```env
VITE_API_URL=http://localhost:8080/api
```

3. Iniciar el servidor de desarrollo:

```bash
npm run dev
```

📍 La app se abrirá en `http://localhost:5173`

---

## 👥 Integrantes del grupo encargados de desarrollar la App

* 👨‍💻 **Leonel Fernández**

---

## 📄 Licencia

Este proyecto es parte de un trabajo práctico integrador académico.
Todos los derechos reservados a sus autores.

```
