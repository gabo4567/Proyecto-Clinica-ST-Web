-- 1. Terminar todas las conexiones activas a la base de datos
USE master;
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = N'clinica_salud_total')
BEGIN
    ALTER DATABASE clinica_salud_total SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE clinica_salud_total;
END
GO

-- 2. Crear la base de datos nuevamente
CREATE DATABASE clinica_salud_total;
GO

-- 3. Usar la base de datos
USE clinica_salud_total;
GO

CREATE TABLE estado (
    id_estado BIGINT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    descripcion VARCHAR(255) NULL
);
INSERT INTO estado (nombre, descripcion) VALUES
-- Estados comunes
('Activo', 'Entidad habilitada para operar en el sistema'),
('Inactivo', 'Entidad deshabilitada o eliminada lógicamente'),
-- Estados de turno
('Confirmado', 'Turno confirmado por el paciente'),
('Programado', 'Turno aceptado y agendado por la secretaría'),
('Reprogramado', 'Turno reubicado para otro día u horario'),
('Cancelado', 'Turno anulado por el paciente o la clínica'),
('Atendido', 'Turno finalizado y paciente fue atendido'),
-- Estado adicional para usuario
('Bloqueado', 'Usuario bloqueado por el sistema');


CREATE TABLE especialidad (
    id_especialidad BIGINT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL UNIQUE,
    descripcion VARCHAR(255),
    id_estado BIGINT NOT NULL,
    FOREIGN KEY (id_estado) REFERENCES estado(id_estado)
);
INSERT INTO especialidad (nombre, descripcion, id_estado) VALUES
('Clínica General', 'Especialidad general para atención primaria.', 1),
('Cardiología', 'Especialidad para enfermedades del corazón y sistema circulatorio.', 1),
('Pediatría', 'Especialidad para atención médica de niños y adolescentes.', 1),
('Ginecología', 'Especialidad para salud femenina y reproductiva.', 1);


CREATE TABLE paciente (
    id_paciente BIGINT IDENTITY(1,1) PRIMARY KEY,
    dni VARCHAR(20) NOT NULL UNIQUE,
    nombre VARCHAR(50) NOT NULL,
    apellido VARCHAR(50) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    telefono VARCHAR(20),
    direccion VARCHAR(255),
    genero VARCHAR(20) NOT NULL,
    fecha_nacimiento DATE NOT NULL,
	edad INT NULL,
	obra_social VARCHAR(50) NULL,
    id_estado BIGINT NOT NULL,
    
    CONSTRAINT chk_genero CHECK (genero IN ('Masculino', 'Femenino', 'Otro')),
    CONSTRAINT fk_estado FOREIGN KEY (id_estado) REFERENCES estado(id_estado)
);
INSERT INTO paciente (dni, nombre, apellido, email, telefono, direccion, genero, fecha_nacimiento, edad, obra_social, id_estado) VALUES
('40000001', 'Juan', 'Pérez', 'juan.perez1@mail.com', '1123456781', 'Calle Falsa 123', 'Masculino', '1990-05-15', 35, 'OSDE', 1),
('40000002', 'María', 'Gómez', 'maria.gomez2@mail.com', '1123456782', 'Av. Siempreviva 742', 'Femenino', '1985-08-20', 39, 'Swiss Medical', 1),
('40000003', 'Carlos', 'López', 'carlos.lopez3@mail.com', '1123456783', 'Calle 9 de Julio 456', 'Masculino', '2000-03-10', 25, 'Galeno', 1),
('40000004', 'Lucía', 'Martínez', 'lucia.martinez4@mail.com', '1123456784', 'San Martín 789', 'Femenino', '1995-11-12', 29, 'Medifé', 1),
('40000005', 'Pedro', 'Fernández', 'pedro.fernandez5@mail.com', '1123456785', 'Belgrano 100', 'Masculino', '1975-06-30', 49, 'IOMA', 1),
('40000006', 'Sofía', 'Díaz', 'sofia.diaz6@mail.com', '1123456786', 'Lavalle 321', 'Femenino', '2002-09-05', 22, 'PAMI', 1),
('40000007', 'Martín', 'García', 'martin.garcia7@mail.com', '1123456787', 'Alsina 654', 'Masculino', '1988-01-25', 36, 'Hospital Italiano', 1),
('40000008', 'Ana', 'Ruiz', 'ana.ruiz8@mail.com', '1123456788', 'Perón 888', 'Femenino', '1999-04-18', 26, 'OSDE', 1),
('40000009', 'Diego', 'Sosa', 'diego.sosa9@mail.com', '1123456789', 'Mitre 1010', 'Masculino', '1982-12-01', 42, 'Galeno', 1),
('40000010', 'Valeria', 'Torres', 'valeria.torres10@mail.com', '1123456790', 'Corrientes 432', 'Femenino', '1991-07-07', 33, 'Swiss Medical', 1),
('40000011', 'Leo', 'Romero', 'leo.romero11@mail.com', '1123456791', 'Catamarca 321', 'Masculino', '1993-09-09', 31, 'OMINT', 1),
('40000012', 'Camila', 'Alvarez', 'camila.alvarez12@mail.com', '1123456792', 'Salta 233', 'Femenino', '1980-06-01', 44, 'Medifé', 1),
('40000013', 'Fernando', 'Vega', 'fernando.vega13@mail.com', '1123456793', 'San Juan 454', 'Masculino', '2001-12-15', 23, 'Sancor Salud', 1),
('40000014', 'Julia', 'Acosta', 'julia.acosta14@mail.com', '1123456794', 'Formosa 121', 'Femenino', '1996-02-02', 28, 'PAMI', 1),
('40000015', 'Andrés', 'Paz', 'andres.paz15@mail.com', '1123456795', 'La Rioja 777', 'Masculino', '1998-10-10', 26, 'IOMA', 1),
('40000016', 'Micaela', 'Ibarra', 'micaela.ibarra16@mail.com', '1123456796', 'Sarmiento 142', 'Femenino', '1990-01-01', 35, 'Swiss Medical', 1),
('40000017', 'Pablo', 'Cruz', 'pablo.cruz17@mail.com', '1123456797', 'Ituzaingó 210', 'Masculino', '1984-05-25', 40, 'Galeno', 1),
('40000018', 'Brenda', 'Molina', 'brenda.molina18@mail.com', '1123456798', 'French 300', 'Femenino', '1997-11-03', 27, 'Accord Salud', 1),
('40000019', 'Lucas', 'Moreno', 'lucas.moreno19@mail.com', '1123456799', 'Viamonte 202', 'Masculino', '2003-08-08', 21, 'Hospital Italiano', 1),
('40000020', 'Agustina', 'Herrera', 'agustina.herrera20@mail.com', '1123456700', 'Junín 505', 'Femenino', '1992-03-14', 32, 'Medifé', 1),
-- Pacientes Menores a 18 años
('40000021', 'Tomás', 'Navarro', 'tomas.navarro21@mail.com', '1123456701', 'Rawson 606', 'Masculino', '2010-09-30', 13, 'OMINT', 1),
('40000022', 'Milagros', 'Castro', 'milagros.castro22@mail.com', '1123456702', 'Entre Ríos 78', 'Femenino', '2009-07-23', 14, 'Sancor Salud', 1),
('40000023', 'Javier', 'Silva', 'javier.silva23@mail.com', '1123456703', 'Chacabuco 41', 'Masculino', '2008-12-22', 15, 'IOMA', 1),
('40000024', 'Carolina', 'Bustos', 'carolina.bustos24@mail.com', '1123456704', 'Urquiza 91', 'Femenino', '2011-06-16', 13, 'Swiss Medical', 1),
('40000025', 'Emiliano', 'Arias', 'emiliano.arias25@mail.com', '1123456705', 'Santa Fe 404', 'Masculino', '2009-02-08', 15, 'Accord Salud', 1);


CREATE TABLE profesional (
    id_profesional BIGINT IDENTITY(1,1) PRIMARY KEY,
    dni VARCHAR(20) NOT NULL UNIQUE,
    nombre VARCHAR(50) NOT NULL,
    apellido VARCHAR(50) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    telefono VARCHAR(20),
    direccion VARCHAR(255),
    genero VARCHAR(20) NOT NULL,
    fecha_nacimiento DATE NOT NULL,
	edad INT NULL, 
	matricula VARCHAR(50) NOT NULL UNIQUE,
    id_especialidad BIGINT NOT NULL,
    id_estado BIGINT NOT NULL,

    CONSTRAINT chk_genero_prof CHECK (genero IN ('Masculino', 'Femenino', 'Otro')),
    CONSTRAINT fk_especialidad FOREIGN KEY (id_especialidad) REFERENCES especialidad(id_especialidad),
    CONSTRAINT fk_estado_prof FOREIGN KEY (id_estado) REFERENCES estado(id_estado)
);
INSERT INTO profesional (dni, nombre, apellido, email, telefono, direccion, genero, fecha_nacimiento, edad, matricula, id_especialidad, id_estado) VALUES
('30000001', 'Laura', 'Méndez', 'laura.mendez1@clinica.com', '1123456701', 'Av. San Martín 123', 'Femenino', '1985-04-12', 39, 'MAT-1001', 1, 1),
('30000002', 'Jorge', 'Luna', 'jorge.luna2@clinica.com', '1123456702', 'Calle 25 de Mayo 234', 'Masculino', '1979-06-08', 45, 'MAT-1002', 2, 1),
('30000003', 'Patricia', 'Salas', 'patricia.salas3@clinica.com', '1123456703', 'Belgrano 456', 'Femenino', '1990-10-20', 33, 'MAT-1003', 4, 1),
('30000004', 'Hernán', 'Delgado', 'hernan.delgado4@clinica.com', '1123456704', 'Mitre 321', 'Masculino', '1982-01-05', 42, 'MAT-1004', 1, 1),
('30000005', 'Carla', 'Figueroa', 'carla.figueroa5@clinica.com', '1123456705', 'Lavalle 101', 'Femenino', '1988-07-17', 35, 'MAT-1005', 2, 1),
('30000006', 'Pablo', 'Gutiérrez', 'pablo.gutierrez6@clinica.com', '1123456706', 'Viamonte 789', 'Masculino', '1975-11-30', 48, 'MAT-1006', 4, 1),
('30000007', 'Romina', 'Sánchez', 'romina.sanchez7@clinica.com', '1123456707', 'Rivadavia 654', 'Femenino', '1993-03-22', 31, 'MAT-1007', 1, 1),
('30000008', 'Gustavo', 'Herrera', 'gustavo.herrera8@clinica.com', '1123456708', 'Perón 998', 'Masculino', '1987-09-19', 36, 'MAT-1008', 2, 1),
('30000009', 'Mónica', 'Vera', 'monica.vera9@clinica.com', '1123456709', 'San Juan 221', 'Femenino', '1995-12-12', 29, 'MAT-1009', 4, 1),
('30000010', 'Eduardo', 'Paredes', 'eduardo.paredes10@clinica.com', '1123456710', 'Entre Ríos 875', 'Masculino', '1981-05-01', 43, 'MAT-1010', 1, 1),
('30000011', 'Sabrina', 'Molina', 'sabrina.molina11@clinica.com', '1123456711', 'Formosa 303', 'Femenino', '1991-02-16', 33, 'MAT-1011', 2, 1),
('30000012', 'Fernando', 'Acosta', 'fernando.acosta12@clinica.com', '1123456712', 'Catamarca 412', 'Masculino', '1994-06-25', 30, 'MAT-1012', 4, 1),
('30000013', 'Paula', 'Iglesias', 'paula.iglesias13@clinica.com', '1123456713', 'Santa Fe 222', 'Femenino', '1986-08-08', 37, 'MAT-1013', 1, 1),
('30000014', 'Ricardo', 'Ortega', 'ricardo.ortega14@clinica.com', '1123456714', 'Rawson 144', 'Masculino', '1978-04-04', 47, 'MAT-1014', 2, 1),
('30000015', 'Lucía', 'Reyes', 'lucia.reyes15@clinica.com', '1123456715', 'La Rioja 909', 'Femenino', '1990-01-01', 34, 'MAT-1015', 4, 1),
('30000016', 'Tomás', 'Morales', 'tomas.morales16@clinica.com', '1123456716', 'Urquiza 360', 'Masculino', '1989-10-18', 35, 'MAT-1016', 1, 1),
('30000017', 'Julieta', 'Campos', 'julieta.campos17@clinica.com', '1123456717', 'Chacabuco 601', 'Femenino', '1996-03-10', 28, 'MAT-1017', 2, 1),
('30000018', 'Diego', 'Bravo', 'diego.bravo18@clinica.com', '1123456718', 'Junín 700', 'Masculino', '1980-12-22', 43, 'MAT-1018', 4, 1),
('30000019', 'Camila', 'Lozano', 'camila.lozano19@clinica.com', '1123456719', 'Corrientes 818', 'Femenino', '1983-09-15', 40, 'MAT-1019', 1, 1),
('30000020', 'Matías', 'Aguirre', 'matias.aguirre20@clinica.com', '1123456720', 'Sarmiento 505', 'Masculino', '1992-07-07', 32, 'MAT-1020', 2, 1),
-- Pediatras
('30000021', 'Noelia', 'Rojas', 'noelia.rojas21@clinica.com', '1123456721', 'Ituzaingó 243', 'Femenino', '1999-11-11', 25, 'MAT-1021', 3, 1),
('30000022', 'Ezequiel', 'Martínez', 'ezequiel.martinez22@clinica.com', '1123456722', 'Alsina 1002', 'Masculino', '1995-06-14', 29, 'MAT-1022', 3, 1),
('30000023', 'Melina', 'Gómez', 'melina.gomez23@clinica.com', '1123456723', 'Salta 616', 'Femenino', '1997-05-05', 27, 'MAT-1023', 3, 1),
('30000024', 'Axel', 'Ruiz', 'axel.ruiz24@clinica.com', '1123456724', 'Jujuy 505', 'Masculino', '1984-08-30', 39, 'MAT-1024', 3, 1),
('30000025', 'Valeria', 'Paz', 'valeria.paz25@clinica.com', '1123456725', 'French 707', 'Femenino', '1993-12-01', 31, 'MAT-1025', 3, 1);


CREATE TABLE tutor (
    id_tutor BIGINT IDENTITY(1,1) PRIMARY KEY,
    dni VARCHAR(20) NOT NULL UNIQUE,
    nombre VARCHAR(50) NOT NULL,
    apellido VARCHAR(50) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    telefono VARCHAR(20),
    direccion VARCHAR(255),
    genero VARCHAR(20) NOT NULL,
    fecha_nacimiento DATE NOT NULL,
    edad INT NULL, 
    obra_social VARCHAR(50) NULL,
    parentesco VARCHAR(50) NULL,
    id_paciente BIGINT NOT NULL,
    id_estado BIGINT NOT NULL,

    CONSTRAINT chk_genero_tutor CHECK (genero IN ('Masculino', 'Femenino', 'Otro')),
    CONSTRAINT fk_tutor_paciente FOREIGN KEY (id_paciente) REFERENCES paciente(id_paciente),
    CONSTRAINT fk_tutor_estado FOREIGN KEY (id_estado) REFERENCES estado(id_estado)
);

INSERT INTO tutor (
    dni, nombre, apellido, email, telefono, direccion, genero, fecha_nacimiento, edad, obra_social, parentesco, id_paciente, id_estado
) VALUES
('50000001', 'Laura', 'Navarro', 'laura.navarro@mail.com', '1130000001', 'Rawson 606', 'Femenino', '1982-04-10', 43, 'OMINT', 'Madre', 21, 1),
('50000002', 'Andrés', 'Castro', 'andres.castro@mail.com', '1130000002', 'Entre Ríos 78', 'Masculino', '1980-12-01', 43, 'Sancor Salud', 'Padre', 22, 1),
('50000003', 'Claudia', 'Silva', 'claudia.silva@mail.com', '1130000003', 'Chacabuco 41', 'Femenino', '1975-05-15', 49, 'IOMA', 'Tía', 23, 1),
('50000004', 'Eduardo', 'Bustos', 'eduardo.bustos@mail.com', '1130000004', 'Urquiza 91', 'Masculino', '1985-09-09', 38, 'Swiss Medical', 'Padre', 24, 1),
('50000005', 'Patricia', 'Arias', 'patricia.arias@mail.com', '1130000005', 'Santa Fe 404', 'Femenino', '1987-02-12', 38, 'Accord Salud', 'Madre', 25, 1);


CREATE TABLE turno (
    id_turno BIGINT IDENTITY(1,1) PRIMARY KEY,
    comprobante VARCHAR(50) NOT NULL,
    id_paciente BIGINT NOT NULL,
    id_profesional BIGINT NOT NULL,
    id_tutor BIGINT NULL, -- Tutor opcional
    fecha_hora DATETIME2 NOT NULL,
    duracion INT NOT NULL,
    id_estado BIGINT NOT NULL,
    observaciones VARCHAR(MAX) NULL,
    fecha_creacion DATETIME2 DEFAULT SYSUTCDATETIME() NOT NULL,

    CONSTRAINT fk_turno_paciente FOREIGN KEY (id_paciente) REFERENCES paciente(id_paciente),
    CONSTRAINT fk_turno_profesional FOREIGN KEY (id_profesional) REFERENCES profesional(id_profesional),
    CONSTRAINT fk_turno_tutor FOREIGN KEY (id_tutor) REFERENCES tutor(id_tutor),
	CONSTRAINT fk_turno_estado FOREIGN KEY (id_estado) REFERENCES estado(id_estado)
);
INSERT INTO turno (comprobante, id_paciente, id_profesional, id_tutor, fecha_hora, duracion, id_estado, observaciones)
VALUES
-- Primeros 20 turnos (pacientes adultos y profesionales no pediatras)
('ST-20250616-000001', 1, 1, NULL, '2025-06-17 09:00:00', 30, 3, NULL),
('ST-20250616-000002', 2, 2, NULL, '2025-06-17 09:30:00', 30, 3, NULL),
('ST-20250616-000003', 3, 3, NULL, '2025-06-17 10:00:00', 30, 3, NULL),
('ST-20250616-000004', 4, 4, NULL, '2025-06-17 10:30:00', 30, 3, NULL),
('ST-20250616-000005', 5, 5, NULL, '2025-06-17 11:00:00', 30, 3, NULL),
('ST-20250616-000006', 6, 6, NULL, '2025-06-17 11:30:00', 30, 3, NULL),
('ST-20250616-000007', 7, 7, NULL, '2025-06-17 12:00:00', 30, 3, NULL),
('ST-20250616-000008', 8, 8, NULL, '2025-06-17 12:30:00', 30, 3, NULL),
('ST-20250616-000009', 9, 9, NULL, '2025-06-17 13:00:00', 30, 3, NULL),
('ST-20250616-000010', 10, 10, NULL, '2025-06-17 13:30:00', 30, 3, NULL),
('ST-20250616-000011', 11, 11, NULL, '2025-06-18 09:00:00', 30, 3, NULL),
('ST-20250616-000012', 12, 12, NULL, '2025-06-18 09:30:00', 30, 3, NULL),
('ST-20250616-000013', 13, 13, NULL, '2025-06-18 10:00:00', 30, 3, NULL),
('ST-20250616-000014', 14, 14, NULL, '2025-06-18 10:30:00', 30, 3, NULL),
('ST-20250616-000015', 15, 15, NULL, '2025-06-18 11:00:00', 30, 3, NULL),
('ST-20250616-000016', 16, 16, NULL, '2025-06-18 11:30:00', 30, 3, NULL),
('ST-20250616-000017', 17, 17, NULL, '2025-06-18 12:00:00', 30, 3, NULL),
('ST-20250616-000018', 18, 18, NULL, '2025-06-18 12:30:00', 30, 3, NULL),
('ST-20250616-000019', 19, 19, NULL, '2025-06-18 13:00:00', 30, 3, NULL),
('ST-20250616-000020', 20, 20, NULL, '2025-06-18 13:30:00', 30, 3, NULL),
-- Últimos 5 turnos (pacientes menores, profesionales pediatras, con tutor)
('ST-20250616-000021', 21, 21, 1, '2025-06-19 09:00:00', 30, 3, NULL),
('ST-20250616-000022', 22, 22, 2, '2025-06-19 09:30:00', 30, 3, NULL),
('ST-20250616-000023', 23, 23, 3, '2025-06-19 10:00:00', 30, 3, NULL),
('ST-20250616-000024', 24, 24, 4, '2025-06-19 10:30:00', 30, 3, NULL),
('ST-20250616-000025', 25, 25, 5, '2025-06-19 11:00:00', 30, 3, NULL);

CREATE TABLE usuario (
    id_usuario BIGINT IDENTITY(1,1) PRIMARY KEY,
    nombre_usuario VARCHAR(50) NOT NULL UNIQUE,
    contrasena VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    rol VARCHAR(20) NOT NULL,
    id_estado BIGINT NOT NULL,

    CONSTRAINT chk_usuario_rol CHECK (rol IN ('Admin', 'Secretaria', 'Profesional', 'Paciente', 'Tutor')),
    CONSTRAINT fk_usuario_estado FOREIGN KEY (id_estado) REFERENCES estado(id_estado)
);
-- Inserción con roles explícitos
INSERT INTO usuario (nombre_usuario, contrasena, email, rol, id_estado) VALUES
('admin', 'admin123', 'admin@saludtotal.com', 'Admin', 1),
('secretaria1', 'secretaria123', 'secretaria1@saludtotal.com', 'Secretaria', 1),
('profesional1', 'profesional123', 'profesional1@saludtotal.com', 'Profesional', 1),
('paciente1', 'paciente123', 'paciente1@saludtotal.com', 'Paciente', 1),
('tutor1', 'tutor123', 'tutor1@saludtotal.com', 'Tutor', 1);


CREATE TABLE Horario_Disponible (
    id_horario INT IDENTITY(1,1) PRIMARY KEY,
    id_profesional BIGINT NOT NULL,
    dia_semana INT NOT NULL, 
    hora_inicio TIME NOT NULL,
    hora_fin TIME NOT NULL,
    id_estado BIGINT NOT NULL,
    FOREIGN KEY (id_profesional) REFERENCES Profesional(id_profesional),
    FOREIGN KEY (id_estado) REFERENCES Estado(id_estado)
);

-- Horarios disponibles para Profesional 1
INSERT INTO Horario_Disponible (id_profesional, dia_semana, hora_inicio, hora_fin, id_estado) VALUES
(1, 1, '08:00', '12:30', 1),
(1, 1, '16:00', '20:30', 1),
(1, 2, '08:00', '12:30', 1),
(1, 2, '16:00', '20:30', 1),
(1, 3, '08:00', '12:30', 1),
(1, 3, '16:00', '20:30', 1),
(1, 4, '08:00', '12:30', 1),
(1, 4, '16:00', '20:30', 1),
(1, 5, '08:00', '12:30', 1),
(1, 5, '16:00', '20:30', 1),
(1, 6, '08:00', '12:30', 1);

-- Horarios disponibles para Profesional 2
INSERT INTO Horario_Disponible (id_profesional, dia_semana, hora_inicio, hora_fin, id_estado) VALUES
(2, 1, '08:00', '12:30', 1),
(2, 1, '16:00', '20:30', 1),
(2, 2, '08:00', '12:30', 1),
(2, 2, '16:00', '20:30', 1),
(2, 3, '08:00', '12:30', 1),
(2, 3, '16:00', '20:30', 1),
(2, 4, '08:00', '12:30', 1),
(2, 4, '16:00', '20:30', 1),
(2, 5, '08:00', '12:30', 1),
(2, 5, '16:00', '20:30', 1),
(2, 6, '08:00', '12:30', 1);

-- Horarios disponibles para Profesional 3
INSERT INTO Horario_Disponible (id_profesional, dia_semana, hora_inicio, hora_fin, id_estado) VALUES
(3, 1, '08:00', '12:30', 1),
(3, 1, '16:00', '20:30', 1),
(3, 2, '08:00', '12:30', 1),
(3, 2, '16:00', '20:30', 1),
(3, 3, '08:00', '12:30', 1),
(3, 3, '16:00', '20:30', 1),
(3, 4, '08:00', '12:30', 1),
(3, 4, '16:00', '20:30', 1),
(3, 5, '08:00', '12:30', 1),
(3, 5, '16:00', '20:30', 1),
(3, 6, '08:00', '12:30', 1);

-- Horarios disponibles para Profesional 4
INSERT INTO Horario_Disponible (id_profesional, dia_semana, hora_inicio, hora_fin, id_estado) VALUES
(4, 1, '08:00', '12:30', 1),
(4, 1, '16:00', '20:30', 1),
(4, 2, '08:00', '12:30', 1),
(4, 2, '16:00', '20:30', 1),
(4, 3, '08:00', '12:30', 1),
(4, 3, '16:00', '20:30', 1),
(4, 4, '08:00', '12:30', 1),
(4, 4, '16:00', '20:30', 1),
(4, 5, '08:00', '12:30', 1),
(4, 5, '16:00', '20:30', 1),
(4, 6, '08:00', '12:30', 1);

-- Horarios disponibles para Profesional 5
INSERT INTO Horario_Disponible (id_profesional, dia_semana, hora_inicio, hora_fin, id_estado) VALUES
(5, 1, '08:00', '12:30', 1),
(5, 1, '16:00', '20:30', 1),
(5, 2, '08:00', '12:30', 1),
(5, 2, '16:00', '20:30', 1),
(5, 3, '08:00', '12:30', 1),
(5, 3, '16:00', '20:30', 1),
(5, 4, '08:00', '12:30', 1),
(5, 4, '16:00', '20:30', 1),
(5, 5, '08:00', '12:30', 1),
(5, 5, '16:00', '20:30', 1),
(5, 6, '08:00', '12:30', 1);

-- Profesional 6
INSERT INTO Horario_Disponible (id_profesional, dia_semana, hora_inicio, hora_fin, id_estado) VALUES
(6, 1, '08:00', '12:30', 1), (6, 1, '16:00', '20:30', 1),
(6, 2, '08:00', '12:30', 1), (6, 2, '16:00', '20:30', 1),
(6, 3, '08:00', '12:30', 1), (6, 3, '16:00', '20:30', 1),
(6, 4, '08:00', '12:30', 1), (6, 4, '16:00', '20:30', 1),
(6, 5, '08:00', '12:30', 1), (6, 5, '16:00', '20:30', 1),
(6, 6, '08:00', '12:30', 1);

-- Profesional 7
INSERT INTO Horario_Disponible (id_profesional, dia_semana, hora_inicio, hora_fin, id_estado) VALUES
(7, 1, '08:00', '12:30', 1), (7, 1, '16:00', '20:30', 1),
(7, 2, '08:00', '12:30', 1), (7, 2, '16:00', '20:30', 1),
(7, 3, '08:00', '12:30', 1), (7, 3, '16:00', '20:30', 1),
(7, 4, '08:00', '12:30', 1), (7, 4, '16:00', '20:30', 1),
(7, 5, '08:00', '12:30', 1), (7, 5, '16:00', '20:30', 1),
(7, 6, '08:00', '12:30', 1);

-- Profesional 8
INSERT INTO Horario_Disponible (id_profesional, dia_semana, hora_inicio, hora_fin, id_estado) VALUES
(8, 1, '08:00', '12:30', 1), (8, 1, '16:00', '20:30', 1),
(8, 2, '08:00', '12:30', 1), (8, 2, '16:00', '20:30', 1),
(8, 3, '08:00', '12:30', 1), (8, 3, '16:00', '20:30', 1),
(8, 4, '08:00', '12:30', 1), (8, 4, '16:00', '20:30', 1),
(8, 5, '08:00', '12:30', 1), (8, 5, '16:00', '20:30', 1),
(8, 6, '08:00', '12:30', 1);

-- Profesional 9
INSERT INTO Horario_Disponible (id_profesional, dia_semana, hora_inicio, hora_fin, id_estado) VALUES
(9, 1, '08:00', '12:30', 1), (9, 1, '16:00', '20:30', 1),
(9, 2, '08:00', '12:30', 1), (9, 2, '16:00', '20:30', 1),
(9, 3, '08:00', '12:30', 1), (9, 3, '16:00', '20:30', 1),
(9, 4, '08:00', '12:30', 1), (9, 4, '16:00', '20:30', 1),
(9, 5, '08:00', '12:30', 1), (9, 5, '16:00', '20:30', 1),
(9, 6, '08:00', '12:30', 1);

-- Profesional 10
INSERT INTO Horario_Disponible (id_profesional, dia_semana, hora_inicio, hora_fin, id_estado) VALUES
(10, 1, '08:00', '12:30', 1), (10, 1, '16:00', '20:30', 1),
(10, 2, '08:00', '12:30', 1), (10, 2, '16:00', '20:30', 1),
(10, 3, '08:00', '12:30', 1), (10, 3, '16:00', '20:30', 1),
(10, 4, '08:00', '12:30', 1), (10, 4, '16:00', '20:30', 1),
(10, 5, '08:00', '12:30', 1), (10, 5, '16:00', '20:30', 1),
(10, 6, '08:00', '12:30', 1);

-- Profesional 11
INSERT INTO Horario_Disponible (id_profesional, dia_semana, hora_inicio, hora_fin, id_estado) VALUES
(11, 1, '08:00', '12:30', 1), (11, 1, '16:00', '20:30', 1),
(11, 2, '08:00', '12:30', 1), (11, 2, '16:00', '20:30', 1),
(11, 3, '08:00', '12:30', 1), (11, 3, '16:00', '20:30', 1),
(11, 4, '08:00', '12:30', 1), (11, 4, '16:00', '20:30', 1),
(11, 5, '08:00', '12:30', 1), (11, 5, '16:00', '20:30', 1),
(11, 6, '08:00', '12:30', 1);

-- Profesional 12
INSERT INTO Horario_Disponible (id_profesional, dia_semana, hora_inicio, hora_fin, id_estado) VALUES
(12, 1, '08:00', '12:30', 1), (12, 1, '16:00', '20:30', 1),
(12, 2, '08:00', '12:30', 1), (12, 2, '16:00', '20:30', 1),
(12, 3, '08:00', '12:30', 1), (12, 3, '16:00', '20:30', 1),
(12, 4, '08:00', '12:30', 1), (12, 4, '16:00', '20:30', 1),
(12, 5, '08:00', '12:30', 1), (12, 5, '16:00', '20:30', 1),
(12, 6, '08:00', '12:30', 1);

-- Profesional 13
INSERT INTO Horario_Disponible (id_profesional, dia_semana, hora_inicio, hora_fin, id_estado) VALUES
(13, 1, '08:00', '12:30', 1), (13, 1, '16:00', '20:30', 1),
(13, 2, '08:00', '12:30', 1), (13, 2, '16:00', '20:30', 1),
(13, 3, '08:00', '12:30', 1), (13, 3, '16:00', '20:30', 1),
(13, 4, '08:00', '12:30', 1), (13, 4, '16:00', '20:30', 1),
(13, 5, '08:00', '12:30', 1), (13, 5, '16:00', '20:30', 1),
(13, 6, '08:00', '12:30', 1);

-- Profesional 14
INSERT INTO Horario_Disponible (id_profesional, dia_semana, hora_inicio, hora_fin, id_estado) VALUES
(14, 1, '08:00', '12:30', 1), (14, 1, '16:00', '20:30', 1),
(14, 2, '08:00', '12:30', 1), (14, 2, '16:00', '20:30', 1),
(14, 3, '08:00', '12:30', 1), (14, 3, '16:00', '20:30', 1),
(14, 4, '08:00', '12:30', 1), (14, 4, '16:00', '20:30', 1),
(14, 5, '08:00', '12:30', 1), (14, 5, '16:00', '20:30', 1),
(14, 6, '08:00', '12:30', 1);

-- Profesional 15
INSERT INTO Horario_Disponible (id_profesional, dia_semana, hora_inicio, hora_fin, id_estado) VALUES
(15, 1, '08:00', '12:30', 1), (15, 1, '16:00', '20:30', 1),
(15, 2, '08:00', '12:30', 1), (15, 2, '16:00', '20:30', 1),
(15, 3, '08:00', '12:30', 1), (15, 3, '16:00', '20:30', 1),
(15, 4, '08:00', '12:30', 1), (15, 4, '16:00', '20:30', 1),
(15, 5, '08:00', '12:30', 1), (15, 5, '16:00', '20:30', 1),
(15, 6, '08:00', '12:30', 1);

-- Profesional 16
INSERT INTO Horario_Disponible (id_profesional, dia_semana, hora_inicio, hora_fin, id_estado) VALUES
(16, 1, '08:00', '12:30', 1), (16, 1, '16:00', '20:30', 1),
(16, 2, '08:00', '12:30', 1), (16, 2, '16:00', '20:30', 1),
(16, 3, '08:00', '12:30', 1), (16, 3, '16:00', '20:30', 1),
(16, 4, '08:00', '12:30', 1), (16, 4, '16:00', '20:30', 1),
(16, 5, '08:00', '12:30', 1), (16, 5, '16:00', '20:30', 1),
(16, 6, '08:00', '12:30', 1);

-- Profesional 17
INSERT INTO Horario_Disponible (id_profesional, dia_semana, hora_inicio, hora_fin, id_estado) VALUES
(17, 1, '08:00', '12:30', 1), (17, 1, '16:00', '20:30', 1),
(17, 2, '08:00', '12:30', 1), (17, 2, '16:00', '20:30', 1),
(17, 3, '08:00', '12:30', 1), (17, 3, '16:00', '20:30', 1),
(17, 4, '08:00', '12:30', 1), (17, 4, '16:00', '20:30', 1),
(17, 5, '08:00', '12:30', 1), (17, 5, '16:00', '20:30', 1),
(17, 6, '08:00', '12:30', 1);

-- Profesional 18
INSERT INTO Horario_Disponible (id_profesional, dia_semana, hora_inicio, hora_fin, id_estado) VALUES
(18, 1, '08:00', '12:30', 1), (18, 1, '16:00', '20:30', 1),
(18, 2, '08:00', '12:30', 1), (18, 2, '16:00', '20:30', 1),
(18, 3, '08:00', '12:30', 1), (18, 3, '16:00', '20:30', 1),
(18, 4, '08:00', '12:30', 1), (18, 4, '16:00', '20:30', 1),
(18, 5, '08:00', '12:30', 1), (18, 5, '16:00', '20:30', 1),
(18, 6, '08:00', '12:30', 1);

-- Profesional 19
INSERT INTO Horario_Disponible (id_profesional, dia_semana, hora_inicio, hora_fin, id_estado) VALUES
(19, 1, '08:00', '12:30', 1), (19, 1, '16:00', '20:30', 1),
(19, 2, '08:00', '12:30', 1), (19, 2, '16:00', '20:30', 1),
(19, 3, '08:00', '12:30', 1), (19, 3, '16:00', '20:30', 1),
(19, 4, '08:00', '12:30', 1), (19, 4, '16:00', '20:30', 1),
(19, 5, '08:00', '12:30', 1), (19, 5, '16:00', '20:30', 1),
(19, 6, '08:00', '12:30', 1);

-- Profesional 20
INSERT INTO Horario_Disponible (id_profesional, dia_semana, hora_inicio, hora_fin, id_estado) VALUES
(20, 1, '08:00', '12:30', 1), (20, 1, '16:00', '20:30', 1),
(20, 2, '08:00', '12:30', 1), (20, 2, '16:00', '20:30', 1),
(20, 3, '08:00', '12:30', 1), (20, 3, '16:00', '20:30', 1),
(20, 4, '08:00', '12:30', 1), (20, 4, '16:00', '20:30', 1),
(20, 5, '08:00', '12:30', 1), (20, 5, '16:00', '20:30', 1),
(20, 6, '08:00', '12:30', 1);

-- Profesional 21
INSERT INTO Horario_Disponible (id_profesional, dia_semana, hora_inicio, hora_fin, id_estado) VALUES
(21, 1, '08:00', '12:30', 1), (21, 1, '16:00', '20:30', 1),
(21, 2, '08:00', '12:30', 1), (21, 2, '16:00', '20:30', 1),
(21, 3, '08:00', '12:30', 1), (21, 3, '16:00', '20:30', 1),
(21, 4, '08:00', '12:30', 1), (21, 4, '16:00', '20:30', 1),
(21, 5, '08:00', '12:30', 1), (21, 5, '16:00', '20:30', 1),
(21, 6, '08:00', '12:30', 1);

-- Profesional 22
INSERT INTO Horario_Disponible (id_profesional, dia_semana, hora_inicio, hora_fin, id_estado) VALUES
(22, 1, '08:00', '12:30', 1), (22, 1, '16:00', '20:30', 1),
(22, 2, '08:00', '12:30', 1), (22, 2, '16:00', '20:30', 1),
(22, 3, '08:00', '12:30', 1), (22, 3, '16:00', '20:30', 1),
(22, 4, '08:00', '12:30', 1), (22, 4, '16:00', '20:30', 1),
(22, 5, '08:00', '12:30', 1), (22, 5, '16:00', '20:30', 1),
(22, 6, '08:00', '12:30', 1);

-- Profesional 23
INSERT INTO Horario_Disponible (id_profesional, dia_semana, hora_inicio, hora_fin, id_estado) VALUES
(23, 1, '08:00', '12:30', 1), (23, 1, '16:00', '20:30', 1),
(23, 2, '08:00', '12:30', 1), (23, 2, '16:00', '20:30', 1),
(23, 3, '08:00', '12:30', 1), (23, 3, '16:00', '20:30', 1),
(23, 4, '08:00', '12:30', 1), (23, 4, '16:00', '20:30', 1),
(23, 5, '08:00', '12:30', 1), (23, 5, '16:00', '20:30', 1),
(23, 6, '08:00', '12:30', 1);

-- Profesional 24
INSERT INTO Horario_Disponible (id_profesional, dia_semana, hora_inicio, hora_fin, id_estado) VALUES
(24, 1, '08:00', '12:30', 1), (24, 1, '16:00', '20:30', 1),
(24, 2, '08:00', '12:30', 1), (24, 2, '16:00', '20:30', 1),
(24, 3, '08:00', '12:30', 1), (24, 3, '16:00', '20:30', 1),
(24, 4, '08:00', '12:30', 1), (24, 4, '16:00', '20:30', 1),
(24, 5, '08:00', '12:30', 1), (24, 5, '16:00', '20:30', 1),
(24, 6, '08:00', '12:30', 1);

-- Profesional 25
INSERT INTO Horario_Disponible (id_profesional, dia_semana, hora_inicio, hora_fin, id_estado) VALUES
(25, 1, '08:00', '12:30', 1), (25, 1, '16:00', '20:30', 1),
(25, 2, '08:00', '12:30', 1), (25, 2, '16:00', '20:30', 1),
(25, 3, '08:00', '12:30', 1), (25, 3, '16:00', '20:30', 1),
(25, 4, '08:00', '12:30', 1), (25, 4, '16:00', '20:30', 1),
(25, 5, '08:00', '12:30', 1), (25, 5, '16:00', '20:30', 1),
(25, 6, '08:00', '12:30', 1);
