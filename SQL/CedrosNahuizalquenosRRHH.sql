-- Crear base de datos
CREATE DATABASE CedrosNahuizalquenosRRHH;
GO

-- Usar la base de datos
USE CedrosNahuizalquenosRRHH;
GO

-- Tabla: UsuariosRRHH (usuarios que ingresan al sistema)
CREATE TABLE UsuariosRRHH (
    UsuarioID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Correo NVARCHAR(100) NOT NULL UNIQUE,
    Contrasena NVARCHAR(MAX) NOT NULL,
    FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    Activo BIT NOT NULL DEFAULT 1
);
GO

-- Tabla: Empleados
CREATE TABLE Empleados (
    EmpleadoID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Edad INT NOT NULL,
    FechaIngreso DATE NOT NULL,
    Area NVARCHAR(100) NOT NULL,
    PuestoFuncional NVARCHAR(100) NOT NULL,
    SalarioMensual DECIMAL(10,2) NOT NULL,
    Activo BIT NOT NULL DEFAULT 1
);
GO

-- Tabla: HorasExtras
CREATE TABLE HorasExtras (
    HoraExtraID INT PRIMARY KEY IDENTITY(1,1),
    EmpleadoID INT NOT NULL,
    Fecha DATE NOT NULL,
    Tipo NVARCHAR(50) NOT NULL, -- Diurna, Nocturna, Nocturnidad
    CantidadHoras DECIMAL(5,2) NOT NULL,

    CONSTRAINT FK_HorasExtras_Empleado FOREIGN KEY (EmpleadoID) REFERENCES Empleados(EmpleadoID)
);
GO

-- Tabla: Incidencias
CREATE TABLE Incidencias (
    IncidenciaID INT PRIMARY KEY IDENTITY(1,1),
    EmpleadoID INT NOT NULL,
    Tipo NVARCHAR(50) NOT NULL, -- Vacaciones, Incapacidad, Ausencia
    FechaInicio DATE NOT NULL,
    FechaFin DATE NOT NULL,
    Comentario NVARCHAR(250),

    CONSTRAINT FK_Incidencias_Empleado FOREIGN KEY (EmpleadoID) REFERENCES Empleados(EmpleadoID)
);
GO
