# 🌐 Aplicación Web MVC - Clínica Salud Total

Este módulo corresponde a la aplicación web desarrollada en **ASP.NET MVC con Razor Pages** para el sistema **Clínica Salud Total**. Esta aplicación permite a usuarios autenticados (como secretarias, pacientes y administradores) interactuar con el sistema mediante una interfaz web moderna y responsiva.

---

## 🛠️ Tecnologías utilizadas

- ✅ ASP.NET Core MVC
- ✅ Razor Pages
- ✅ C#
- ✅ Entity Framework Core
- ✅ Bootstrap 5
- ✅ HttpClient
- ✅ JWT (para consumir la API)
- ✅ Validaciones con Data Annotations

---

## 🧩 Funcionalidades implementadas

| Módulo                | Descripción                                                                 |
|------------------------|-----------------------------------------------------------------------------|
| 🔐 Login               | Inicio de sesión con token JWT desde la API                                 |
| 👤 Registro            | Registro de nuevos usuarios (solo Pacientes/Tutores)                        |
| 📅 Turnos              | Listado, detalle, creación y edición de turnos                              |
| 👩‍⚕️ Profesionales     | Gestión de profesionales (listar, crear, editar, eliminar)                  |
| 🧍 Pacientes           | Gestión de pacientes (listar, crear, editar, eliminar)                      |
| 📋 Estados y Especialidades | Vista de estados disponibles y especialidades médicas                 |
| 📂 Home                | Página de bienvenida con enlaces a las secciones principales                |
| ⚠️ Errores y alertas   | Soporte para mostrar mensajes del servidor y validaciones                   |
| 📸 QR (opcional)       | Visualización de QR generado desde la API para turnos                       |

---

## 🧭 Estructura general del proyecto

```

📦 /02\_App-Web-MVC
├── Controllers/
├── Models/
├── Pages/
│   ├── Turnos/
│   ├── Pacientes/
│   ├── Profesionales/
│   ├── Usuarios/
│   └── Shared/
├── Services/
├── Program.cs
└── appsettings.json

````

---

## 🔗 Comunicación con la API

Todas las operaciones de la app se conectan a la API RESTful utilizando `HttpClient`. El token JWT recibido al hacer login se almacena en sesión y se incluye en cada request autenticado.

---

## 🧪 Requisitos para correr localmente

1. Tener instalado **.NET SDK 8.0 o superior**
2. Configurar la URL base de la API en `appsettings.json`
3. Ejecutar el proyecto desde Visual Studio o por consola:

```bash
dotnet run
````

Luego ingresar a:

📍 `https://localhost:5002`

---

## 👥 Integrantes encargados de desarrollar la App

* 👨‍💻 **Juan Francisco Barlett**  
* 👨‍💻 **Enzo Ríos**

---

## 📄 Licencia

Este proyecto forma parte de un trabajo práctico integrador académico.
Todos los derechos reservados a sus autores.

```

---

¿Querés que siga con el README para `01_App-SPA` o te gustaría personalizar algo primero en este?
```
