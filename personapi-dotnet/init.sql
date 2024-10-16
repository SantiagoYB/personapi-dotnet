-- Habilitar opciones avanzadas
EXEC sp_configure 'show advanced options', 1;
RECONFIGURE;

-- Habilitar autenticación mixta (SQL Server y Windows)
EXEC sp_configure 'mixed mode authentication', 1;
RECONFIGURE;

-- Habilitar el inicio de sesión de 'sa'
ALTER LOGIN sa ENABLE;
GO

-- Crear la base de datos si no existe
IF DB_ID('persona_db') IS NULL
BEGIN
    CREATE DATABASE persona_db;
END
GO

USE persona_db;
GO

-- Tabla persona
IF OBJECT_ID('persona', 'U') IS NOT NULL
BEGIN
    DROP TABLE persona;
END
GO

CREATE TABLE persona (
    cc INT NOT NULL,
    nombre VARCHAR(45) NOT NULL,
    apellido VARCHAR(45) NOT NULL,
    genero CHAR(1) CHECK (genero IN ('M', 'F')),
    edad INT NULL,
    PRIMARY KEY (cc)
);
GO

-- Tabla profesion
IF OBJECT_ID('profesion', 'U') IS NOT NULL
BEGIN
    DROP TABLE profesion;
END
GO

CREATE TABLE profesion (
    id INT NOT NULL,
    nom VARCHAR(90) NOT NULL,
    des TEXT NULL,
    PRIMARY KEY (id)
);
GO

-- Tabla estudios
IF OBJECT_ID('estudios', 'U') IS NOT NULL
BEGIN
    DROP TABLE estudios;
END
GO

CREATE TABLE estudios (
    id_prof INT NOT NULL,
    cc_per INT NOT NULL,
    fecha DATE NULL,
    univer VARCHAR(50) NULL,
    PRIMARY KEY (id_prof, cc_per),
    FOREIGN KEY (cc_per) REFERENCES persona(cc),
    FOREIGN KEY (id_prof) REFERENCES profesion(id)
);
GO

-- Tabla telefono
IF OBJECT_ID('telefono', 'U') IS NOT NULL
BEGIN
    DROP TABLE telefono;
END
GO

CREATE TABLE telefono (
    num VARCHAR(15) NOT NULL,
    oper VARCHAR(45) NOT NULL,
    duenio INT NOT NULL,
    PRIMARY KEY (num),
    FOREIGN KEY (duenio) REFERENCES persona(cc)
);
GO
