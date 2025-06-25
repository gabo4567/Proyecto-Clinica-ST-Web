# 🗃️ Base de Datos - Clínica Salud Total

Este módulo contiene la definición completa del esquema relacional para el sistema **Clínica Salud Total**. La base de datos fue diseñada para respaldar todas las funcionalidades del sistema, desde la gestión de turnos hasta la administración de usuarios y profesionales.

---

## 🧱 Modelo Entidad-Relación

La base de datos contiene las siguientes tablas principales:

| Tabla              | Descripción                                                                 |
|-------------------|-----------------------------------------------------------------------------|
| `estado`           | Estados comunes (Activo, Inactivo) y estados específicos para turnos y usuarios |
| `usuario`          | Almacena credenciales de acceso, rol y estado de los usuarios del sistema |
| `paciente`         | Información completa del paciente, incluyendo obra social y contacto       |
| `profesional`      | Profesionales médicos, vinculados a una especialidad y sus horarios        |
| `tutor`            | Representa al tutor legal de un paciente menor de edad                     |
| `turno`            | Información completa de cada turno: profesional, paciente, estado, etc.    |
| `especialidad`     | Especialidades médicas disponibles en la clínica                           |
| `horario_disponible` | Rangos horarios disponibles por profesional para la asignación de turnos |

---

## ⚙️ Requisitos técnicos

- Motor de base de datos: **Microsoft SQL Server**
- Scripts incluidos:
  - 🧱 Creación de tablas con relaciones, claves foráneas y checks
  - 📌 Inserción de datos mínimos (estados, especialidades, usuarios base)
  - 🕒 Horarios predefinidos para pruebas

---

## 🧾 Estructura del archivo

```

📦 /01\_Database
├── 🗃️ schema.sql          # Script completo de creación de la base de datos
├── 📄 README.md           # Descripción general y documentación técnica

```

---

## 🛠️ Cómo usar este script

1. Abrir **SQL Server Management Studio (SSMS)**
2. Ejecutar el archivo `schema.sql`
3. Confirmar que la base `clinica_salud_total` fue creada con todas sus tablas y relaciones correctamente

---

## 🧪 Datos precargados

- 7 estados comunes (Activo, Inactivo, Cancelado, Confirmado, etc.)
- 4 especialidades médicas
- 5 usuarios básicos con roles: Admin, Secretaria, Profesional, Paciente, Tutor
- Horarios predefinidos para el profesional ID 1

---

## 👥 Integrante del grupo encargado de desarrollar la base de datos

- 👨‍💻 **Juan Gabriel Pared**

---

## 📄 Licencia

Base de datos desarrollada exclusivamente para el trabajo práctico integrador.  
No se autoriza su distribución sin el consentimiento de sus autores.

```
