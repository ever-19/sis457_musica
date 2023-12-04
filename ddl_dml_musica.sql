CREATE DATABASE Musica;

USE master
GO
CREATE LOGIN usrmusica12 WITH PASSWORD=N'12345abc',
	DEFAULT_DATABASE=Musica,
	CHECK_EXPIRATION=OFF,
	CHECK_POLICY=ON
	GO
	USE Musica
	GO
	CREATE USER usrmusica12 FOR LOGIN usrmusica12
	GO
	ALTER ROLE db_owner ADD MEMBER usrmusica12
	GO

	DROP TABLE Usuario;
	DROP TABLE Producto;

	-- TABLA CATEGORIA 

	CREATE TABLE Categoria(
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	nombre VARCHAR(40) NOT NULL,

	usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
	fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
	estado SMALLINT NOT NULL DEFAULT 1
	);
	
	CREATE PROC paCategoriaListar @parametro VARCHAR(50)
	AS
	SELECT id,nombre,usuarioRegistro,fechaRegistro,estado 
	FROM Categoria
	WHERE estado<>-1 AND nombre LIKE '%'+REPLACE(@parametro,' ','%')+'%';

	INSERT INTO Categoria(nombre)
	VALUES ('Instrumentos de Cuerda');
	INSERT INTO Categoria(nombre)
	VALUES ('Instrumentos de Viento');
	INSERT INTO Categoria(nombre)
	VALUES ('Instrumento Electrónico');
	

	-- TABLA MARCA
	CREATE TABLE Marca(
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	nombre VARCHAR(40) NOT NULL,

	usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
	fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
	estado SMALLINT NOT NULL DEFAULT 1
	);

	CREATE PROC paMarcaListar @parametro VARCHAR(50)
	AS
	SELECT id,nombre,usuarioRegistro,fechaRegistro,estado 
	FROM Marca
	WHERE estado<>-1 AND nombre LIKE '%'+REPLACE(@parametro,' ','%')+'%';

	INSERT INTO Marca(nombre)
	VALUES ('KORG');

	-- TABLA UNIDAD DE MEDIDA
	CREATE TABLE UnidadMedida(
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	nombre VARCHAR(40) NOT NULL,

	usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
	fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
	estado SMALLINT NOT NULL DEFAULT 1
	);

	CREATE PROC paUnidadMedidaListar @parametro VARCHAR(50)
	AS
	SELECT id,nombre,usuarioRegistro,fechaRegistro,estado 
	FROM UnidadMedida
	WHERE estado<>-1 AND nombre LIKE '%'+REPLACE(@parametro,' ','%')+'%';

	INSERT INTO UnidadMedida(nombre)
	VALUES ('Unidad');

	-- TABLA PRODUCTO
	CREATE TABLE Producto (
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	codigo VARCHAR(10) NOT NULL,
	nombre VARCHAR(50) NOT NULL,
	descripcion VARCHAR(600) NOT NULL,
	precio DECIMAL NOT NULL CHECK(precio > 0),
	cantidadExistente  INT    NOT NULL,
	urlImagen VARCHAR(2000) NOT NULL,
	idCategoria INT NOT NULL,
	idMarca INT NOT NULL,
	idUnidadMedida INT NOT NULL,
	
	usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
	fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
	estado SMALLINT NOT NULL DEFAULT 1,
	CONSTRAINT fk_Producto_Categoria FOREIGN KEY(idCategoria) REFERENCES Categoria(id),
	CONSTRAINT fk_Producto_Marca FOREIGN KEY(idMarca) REFERENCES Marca(id),
	CONSTRAINT fk_Producto_UnidadMedida FOREIGN KEY(idUnidadMedida) REFERENCES UnidadMedida(id)
	);

	CREATE PROC paProductoListar @parametro VARCHAR(50)
	AS
	SELECT id,codigo,nombre,descripcion,precio,cantidadExistente,urlImagen,idCategoria,idMarca,idUnidadMedida,usuarioRegistro,fechaRegistro,estado 
	FROM Producto
	WHERE estado<>-1 AND nombre LIKE '%'+REPLACE(@parametro,' ','%')+'%';

	INSERT INTO Producto(codigo,nombre,descripcion,precio,cantidadExistente,urlImagen,idCategoria,idMarca,idUnidadMedida)
	VALUES ('TCL','TECLADO','Teclado de 5 octavas vintage',18000,5,'https://i.postimg.cc/NfNxVJqq/TECLADO-KRONOS.jpg',3,1,1);
	
	-- TABLA USUARIO
	CREATE TABLE Usuario (
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	nombre VARCHAR(100) NOT NULL,
	apellidos VARCHAR(100) NOT NULL,
	correo VARCHAR(500) NOT NULL,
	usuario VARCHAR(12) NOT NULL,
	clave VARCHAR(100) NOT NULL,
	esAdmin BIT NOT NULL,

	usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
	fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
	estado SMALLINT NOT NULL DEFAULT 1
	);

	CREATE PROC paUsuarioListar @parametro VARCHAR(50)
	AS
	SELECT id,nombre,apellidos,correo,usuario,clave,esAdmin,usuarioRegistro,fechaRegistro,estado 
	FROM Usuario
	WHERE estado<>-1 AND nombre LIKE '%'+REPLACE(@parametro,' ','%')+'%';

	-- VALORES PARA USUARIO
	INSERT INTO Usuario(nombre,apellidos,correo,usuario,clave,esAdmin)
	VALUES ('Ever', 'Revollo', 'revollorafaelever@gmail.com','ever123','sNHlIWhFGGMvPk5ii1nVUw==',1);
	INSERT INTO Usuario(nombre,apellidos,correo,usuario,clave,esAdmin)
	VALUES ('Lucas', 'Fernandez', 'lucafer@gmail.com','lucafer123','sNHlIWhFGGMvPk5ii1nVUw==',1);
	
	select * from Usuario;


	-- TABLA PAIS
	CREATE TABLE Pais(
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	nombre VARCHAR(100) NOT NULL,
	

	usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
	fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
	estado SMALLINT NOT NULL DEFAULT 1,
	);

	CREATE PROC paPaisListar @parametro VARCHAR(50)
	AS
	SELECT id,nombre,usuarioRegistro,fechaRegistro,estado 
	FROM Pais
	WHERE estado<>-1 AND nombre LIKE '%'+REPLACE(@parametro,' ','%')+'%';

	-- TABLA DEPARTAMENTO
	CREATE TABLE Departamento(
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	nombre VARCHAR(100) NOT NULL,
	idPais INT NOT NULL,
	
	usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
	fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
	estado SMALLINT NOT NULL DEFAULT 1,
	CONSTRAINT fk_Departamento_Pais FOREIGN KEY(idPais) REFERENCES Pais(id)
	);

	CREATE PROC paDepartamentoListar @parametro VARCHAR(50)
	AS
	SELECT id,nombre,idPais,usuarioRegistro,fechaRegistro,estado 
	FROM Departamento
	WHERE estado<>-1 AND nombre LIKE '%'+REPLACE(@parametro,' ','%')+'%';

	-- TABLA CIUDAD
	CREATE TABLE Ciudad(
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	nombre VARCHAR(100) NOT NULL,
	idPais INT NOT NULL,
	idDepartamento INT NOT NULL,
	usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
	fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
	estado SMALLINT NOT NULL DEFAULT 1,
	CONSTRAINT fk_Ciudad_Departamento FOREIGN KEY(idDepartamento) REFERENCES Departamento(id),
	CONSTRAINT fk_Ciudad_Pais FOREIGN KEY(idPais) REFERENCES Pais(id)
	);

	CREATE PROC paCiudadListar @parametro VARCHAR(50)
	AS
	SELECT id,nombre,idPais,idDepartamento,usuarioRegistro,fechaRegistro,estado 
	FROM Ciudad
	WHERE estado<>-1 AND nombre LIKE '%'+REPLACE(@parametro,' ','%')+'%';

	-- TABLA ENVIO
	CREATE TABLE Venta(
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	idUsuario INT NOT NULL,
	
	direccion VARCHAR(550) NOT NULL,
	celular VARCHAR(550) NOT NULL,
	idPais INT NOT NULL,
	idDepartamento INT NOT NULL,
	idCiudad INT NOT NULL,

	usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
	fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
	estado SMALLINT NOT NULL DEFAULT 1,

	CONSTRAINT fk_Venta_Usuario FOREIGN KEY(idUsuario) REFERENCES Usuario(id),
	CONSTRAINT fk_Venta_Pais FOREIGN KEY(idPais) REFERENCES Pais(id),
	CONSTRAINT fk_Venta_Departamento FOREIGN KEY(idDepartamento) REFERENCES Departamento(id),
	CONSTRAINT fk_Venta_Ciudad FOREIGN KEY(idCiudad) REFERENCES Ciudad(id),
	);

	CREATE PROC paVentaListar @parametro VARCHAR(50)
	AS
	SELECT id,direccion,celular,idUsuario,idPais,idDepartamento,idCiudad,usuarioRegistro,fechaRegistro,estado 
	FROM Venta
	WHERE estado<>-1 AND fechaRegistro LIKE '%'+REPLACE(@parametro,' ','%')+'%';



	-- TABLA COMPRADETALLE
	CREATE TABLE VentaDetalle (
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	idVenta INT NOT NULL,
	idProducto INT NOT NULL,
	cantidad INT NOT NULL,
	total DECIMAL NOT NULL,

	usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
	fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
	estado SMALLINT NOT NULL DEFAULT 1,
	CONSTRAINT fk_VentaDetalle_Venta FOREIGN KEY(idVenta) REFERENCES Venta(id),
	CONSTRAINT fk_VentaDetalle_Producto FOREIGN KEY(idProducto) REFERENCES Producto(id)
	);

	CREATE PROC paVentaDetalleListar @parametro VARCHAR(50)
	AS
	SELECT id,idVenta,idProducto,total,usuarioRegistro,fechaRegistro,estado 
	FROM VentaDetalle
	WHERE estado<>-1 AND fechaRegistro LIKE '%'+REPLACE(@parametro,' ','%')+'%';
	

	-- TABLA CARRITO
	CREATE TABLE Carrito(
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	idProducto INT NOT NULL,
	idUsuario INT NOT NULL,

	usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
	fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
	estado SMALLINT NOT NULL DEFAULT 1,

	CONSTRAINT fk_Carrito_Producto FOREIGN KEY(idProducto) REFERENCES Producto(id),
	CONSTRAINT fk_Carrito_Usuario FOREIGN KEY(idUsuario) REFERENCES Usuario(id),
	);

	CREATE PROC paCarritoListar @parametro VARCHAR(50)
	AS
	SELECT id,idProducto,idUsuario,usuarioRegistro,fechaRegistro,estado 
	FROM Carrito
	WHERE estado<>-1 AND fechaRegistro LIKE '%'+REPLACE(@parametro,' ','%')+'%';

