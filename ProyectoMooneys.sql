	CREATE DATABASE MOONEYS
	GO

	USE MOONEYS;
	GO

	CREATE TABLE Membresias (
		ID_M INT PRIMARY KEY, 
		Nombre VARCHAR(50) NOT NULL,
		Descripcion VARCHAR(150), 
		Max_Libros_Prestamo INT NOT NULL DEFAULT 1,
		Dias_Prestamo INT NOT NULL DEFAULT 7,
		Costo DECIMAL(10, 2) NOT NULL DEFAULT 0.00
	)
	GO

	CREATE TABLE Clientes (
		ID_C INT PRIMARY KEY,
		DNI VARCHAR(20) UNIQUE NOT NULL,
		Nombre VARCHAR(100) NOT NULL,
		Apellido VARCHAR(100) NOT NULL,
		Telefono VARCHAR(20),
		Direccion VARCHAR(255),
		Email VARCHAR(100) UNIQUE,
		ID_M INT NOT NULL,
		CONSTRAINT FK_ClientesM
			FOREIGN KEY (ID_M)
			REFERENCES Membresias(ID_M)
	)
	GO

	CREATE TABLE Libros (
		ID_L INT PRIMARY KEY,
		Titulo VARCHAR(100) NOT NULL,
		Autor VARCHAR(100) NOT NULL,
		ISBN VARCHAR(15) UNIQUE,
		Stock INT NOT NULL DEFAULT 0,
		URL VARCHAR(250),
		Sinopsis VARCHAR(250)
	)
	GO

	CREATE TABLE Usuarios (
		ID_U INT PRIMARY KEY,
		Nombre VARCHAR(100) NOT NULL,
		Email VARCHAR(100) UNIQUE NOT NULL,
		PIN VARCHAR(15) NOT NULL
	)
	GO

	CREATE TABLE Prestamos (
		ID_P INT PRIMARY KEY,
		ID_L INT NOT NULL,
		ID_C INT NOT NULL,
		ID_U INT NOT NULL,
		Fecha_P DATE NOT NULL,
		Fecha_D DATE,
		CONSTRAINT FK_PrestamosL
			FOREIGN KEY (ID_L)
			REFERENCES Libros(ID_L),
		CONSTRAINT FK_Prestamos_C
			FOREIGN KEY (ID_C)
			REFERENCES Clientes(ID_C),
		CONSTRAINT FK_PrestamosU
			FOREIGN KEY (ID_U)
			REFERENCES Usuarios(ID_U)
	)
	GO

--LISTADO DE MEMBRESPIA--
CREATE PROC sp_Membresias_Listar
AS
BEGIN
    SELECT * FROM Membresias
END
GO


--CLIENTES CRUD COMPLETO--
CREATE PROC SP_ListarClientes
AS
BEGIN
    SELECT * FROM Clientes
END
GO

	


--INSERCION--
CREATE PROC SP_InsertarCliente
@ID_C INT,
@DNI VARCHAR(20),
@Nombre VARCHAR(100),
@Apellido VARCHAR(100),
@Telefono VARCHAR(20),
@Direccion VARCHAR(255),
@Email VARCHAR(100),
@ID_M INT
AS
BEGIN
    INSERT INTO Clientes VALUES
    (@ID_C, @DNI, @Nombre, @Apellido, @Telefono, @Direccion, @Email, @ID_M)
END
GO

--ACTUALIZAR--
CREATE PROC sp_Membresias_Actualizar
@ID_M INT,
@Nombre VARCHAR(50),
@Descripcion VARCHAR(150),
@Max_Libros_Prestamo INT,
@Dias_Prestamo INT,
@Costo DECIMAL(10,2)
AS
BEGIN
    UPDATE Membresias SET
        Nombre = @Nombre,
        Descripcion = @Descripcion,
        Max_Libros_Prestamo = @Max_Libros_Prestamo,
        Dias_Prestamo = @Dias_Prestamo,
        Costo = @Costo
    WHERE ID_M = @ID_M
END
GO

--ELIMINAR--
CREATE PROC sp_Membresias_Eliminar
@ID_M INT
AS
BEGIN
    DELETE FROM Membresias WHERE ID_M = @ID_M
END
GO

--INSERCION DE DATOS DE PRUEBA--

INSERT INTO Membresias (ID_M, Nombre, Descripcion, Max_Libros_Prestamo, Dias_Prestamo, Costo) VALUES
(1, 'Básica', 'Acceso limitado a préstamos', 2, 7, 10.00),
(2, 'Estándar', 'Acceso normal a préstamos', 5, 14, 25.00),
(3, 'Premium', 'Acceso ilimitado a préstamos', 10, 30, 50.00);

INSERT INTO Clientes (ID_C, DNI, Nombre, Apellido, Telefono, Direccion, Email, ID_M) VALUES
(1, '74581236', 'Carlos', 'Pérez', '987654321', 'Av. Lima 123', 'carlos@gmail.com', 1),
(2, '78451239', 'María', 'Gómez', '912345678', 'Jr. Los Olivos 456', 'maria@gmail.com', 2),
(3, '70124589', 'Luis', 'Torres', '956789123', 'Calle Arequipa 789', 'luis@gmail.com', 3);

INSERT INTO Libros (ID_L, Titulo, Autor, ISBN, Stock, URL, Sinopsis) VALUES
(1, 'Cien Años de Soledad', 'Gabriel García Márquez', '9788497592208', 5, NULL, 'Realismo mágico'),
(2, 'Don Quijote de la Mancha', 'Miguel de Cervantes', '9788432225037', 3, NULL, 'Clásico de la literatura'),
(3, 'Harry Potter y la Piedra Filosofal', 'J.K. Rowling', '9788478884451', 10, NULL, 'Fantasía juvenil');

INSERT INTO Usuarios (ID_U, Nombre, Email, PIN) VALUES
(1, 'Administrador', 'admin@mooneys.com', '1234'),
(2, 'Bibliotecario', 'biblio@mooneys.com', '5678');

INSERT INTO Prestamos (ID_P, ID_L, ID_C, ID_U, Fecha_P, Fecha_D) VALUES
(1, 1, 1, 2, '2025-12-01', '2025-12-08'),
(2, 3, 2, 1, '2025-12-02', NULL);

--VERIFICAR EN LA BASE--

SELECT * FROM Membresias;
SELECT * FROM Clientes;
SELECT * FROM Libros;
SELECT * FROM Usuarios;
SELECT * FROM Prestamos;


