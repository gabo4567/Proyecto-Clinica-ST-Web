# 🩺 API RESTful - Clínica Salud Total

Este repositorio contiene la API RESTful del sistema **Clínica Salud Total**, desarrollada con ASP.NET Core. Esta API será consumida por las distintas aplicaciones del proyecto: la Web MVC, la SPA y la App Mobile.

---

## ⚙️ Tecnologías utilizadas

- ✅ ASP.NET Core 8.0
- ✅ C#
- ✅ Entity Framework Core
- ✅ SQL Server
- ✅ JWT (JSON Web Tokens) para autenticación
- ✅ Swagger / Swashbuckle
- ✅ RESTful API Design

---

## 📌 Funcionalidades principales

- 🔐 **Autenticación de usuarios** con tokens JWT
- 👥 **Gestión de usuarios**: Paciente, Profesional, Tutor, Secretaria, Admin
- 📅 **Manejo de turnos**: Alta, baja, modificación, cancelación
- 🧑‍⚕️ **Consulta de profesionales** por especialidad
- 💉 **Gestión de especialidades**
- 📆 **Horarios disponibles** según profesional y fecha
- 👨‍👧 **Asociación de tutores a pacientes**
- 🧾 **Códigos QR** generados para turnos (opcional)

---

## 🔗 Endpoints clave

| Módulo         | Método | Ruta                             | Descripción                                 |
|----------------|--------|----------------------------------|---------------------------------------------|
| 🔐 Auth        | POST   | `/api/usuarios/login`            | Login de usuario, devuelve token JWT        |
| 👤 Usuario     | POST   | `/api/usuarios`                  | Registro de nuevo usuario (Paciente/Tutor)  |
| 📅 Turnos      | GET    | `/api/turnos`                    | Listado de turnos                           |
| 📅 Turnos      | POST   | `/api/turnos`                    | Crear nuevo turno                           |
| 📅 Turnos      | PUT    | `/api/turnos/{id}`               | Editar turno                                |
| 📅 Turnos      | DELETE | `/api/turnos/{id}`               | Cancelar turno                              |
| 🧑‍⚕️ Profesionales | GET   | `/api/profesionales`              | Obtener listado de profesionales            |
| 💉 Especialidades | GET | `/api/especialidades`             | Listar especialidades disponibles           |
| 🕒 Horarios    | GET    | `/api/horarios-disponibles`      | Consultar horarios de un profesional        |
| 👨‍👧 Tutores    | GET    | `/api/tutores`                   | Obtener tutores registrados                 |
| 🧍 Pacientes   | GET    | `/api/pacientes`                 | Obtener pacientes registrados               |

---

## 📁 Estructura del proyecto

```

📦 /03\_API
├── Controllers/
├── DTOs/
├── Models/
├── Services/
├── Repositories/
├── Mappings/
├── Program.cs
└── appsettings.json

````

---

## 🧪 Requisitos para correr localmente

1. Tener instalado **.NET SDK 8.0+**
2. Tener una instancia de **SQL Server**
3. Configurar la cadena de conexión en `appsettings.json`
4. Ejecutar migraciones de base de datos (opcional)
5. Iniciar el proyecto:

```bash
dotnet run
````

Una vez iniciado, podés acceder a la documentación interactiva de la API:

📘 Swagger UI: `https://localhost:5001/swagger`

---

## 👥 Integrantes del grupo

* 👨 Juan Francisco Barlett
* 👨 Leonel Fernández
* 👨 Enzo Ríos
* 👨 Juan Gabriel Pared

---

## 📄 Licencia

Proyecto académico desarrollado como parte del trabajo integrador.
Todos los derechos reservados a sus autores.

---

