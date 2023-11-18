CREATE DATABASE LabMusica;

USE master
GO
CREATE LOGIN usrmusica WITH PASSWORD=N'1234567',
	DEFAULT_DATABASE=LabMusica,
	CHECK_EXPIRATION=OFF,
	CHECK_POLICY=ON
	GO
	USE LabMusica
	GO
	CREATE USER usrmusica FOR LOGIN usrmusica
	GO
	ALTER ROLE db_owner ADD MEMBER usrmusica
	GO

	DROP TABLE Usuario;
	DROP TABLE Empleado;
	DROP TABLE Producto;
	DROP TABLE Cliente;

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
	VALUES ('Instrumentos de cuerda');



	-- TABLA ARTICULO
	CREATE TABLE Articulo (
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	codigo VARCHAR(10) NOT NULL,
	descripcion VARCHAR(250) NOT NULL,
	unidadMedida VARCHAR(20) NOT NULL,
	precio DECIMAL NOT NULL CHECK(precio > 0),
	cantidadExistente  INT    NOT NULL,
	marca VARCHAR(30) NOT NULL,
	idCategoria INT NOT NULL,
	

	usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
	fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
	estado SMALLINT NOT NULL DEFAULT 1,
	CONSTRAINT fk_Empleado_Categoria FOREIGN KEY(idCategoria) REFERENCES Categoria(id),
	);

	ALTER PROC paArticuloListar @parametro VARCHAR(50)
	AS
	SELECT ar.id,idCategoria,ca.nombre as nombre_categoria,
	ar.codigo,ar.descripcion,ar.unidadMedida,ar.precio,ar.cantidadExistente,ar.marca,ar.usuarioRegistro,ar.fechaRegistro,ar.estado

	FROM Articulo ar INNER JOIN Categoria ca ON ar.idCategoria = ca.id
	WHERE ar.estado<>-1 AND ar.descripcion LIKE '%'+REPLACE(@parametro,' ','%')+'%';




	INSERT INTO Articulo(idCategoria,codigo,descripcion,unidadMedida,precio,cantidadExistente,marca)
	VALUES (1,'V001', 'Violin', 'Unidad', 550, 10,'Freeman');



	select * from Articulo;

	

	

	-- TABLA EMPLEADO
	CREATE TABLE Empleado (
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	cedulaIdentidad VARCHAR(15) NOT NULL,
	nombre  VARCHAR (20)  NOT NULL,
	primerApellido   VARCHAR (20)  NOT NULL,
	segundoApellido   VARCHAR (20)  NOT NULL,
	sexo CHAR (1)   NOT NULL,
	fechaContrato DATE  NOT NULL,
	celular BIGINT NOT NULL,

	usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
	fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
	estado SMALLINT NOT NULL DEFAULT 1
	);


	INSERT INTO Empleado(cedulaIdentidad,nombre,primerApellido,segundoApellido, sexo,fechaContrato,celular)
	VALUES ('10326517','Ever', 'Revollo', 'Rafael','M', '10/11/2023', 68615340 );

	CREATE PROC paEmpleadoListar @parametro VARCHAR(50)
	AS
	SELECT id,cedulaIdentidad,nombre,primerApellido,segundoApellido,sexo,fechaContrato,celular,usuarioRegistro,fechaRegistro,estado 
	FROM Empleado
	WHERE estado<>-1 AND cedulaIdentidad LIKE '%'+REPLACE(@parametro,' ','%')+'%';

	select * from Empleado;


	-- TABLA USUARIO
	CREATE TABLE Usuario (
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	idEmpleado INT NOT NULL,
	usuario VARCHAR(12) NOT NULL,
	clave VARCHAR(100) NOT NULL,
	rol VARCHAR(30) NOT NULL,

	usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
	fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
	estado SMALLINT NOT NULL DEFAULT 1,
	CONSTRAINT fk_Usuario_Empleado FOREIGN KEY(idEmpleado) REFERENCES Empleado(id)
	);

	INSERT INTO Usuario(idEmpleado,usuario,clave,rol)
	VALUES (1,'everrevollo','sis457', 'Administrador');

	ALTER PROC paUsuarioListar @parametro VARCHAR(50)
	AS
	SELECT us.id,idEmpleado,em.cedulaIdentidad as cedulaidentida_empleado,
	us.usuario,us.clave,us.rol,us.usuarioRegistro,us.fechaRegistro,us.estado

	FROM Usuario us INNER JOIN Empleado em ON us.idEmpleado = em.id
	WHERE us.estado<>-1 AND us.usuario LIKE '%'+REPLACE(@parametro,' ','%')+'%';

	select * from Usuario;


	-- TABLA CLIENTE
	CREATE TABLE Cliente(
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	cedulaIdentidad VARCHAR(15) NOT NULL,
	nombre VARCHAR(20) NOT NULL,
	primerApellido   VARCHAR (20)  NOT NULL,
	segundoApellido   VARCHAR (20)  NOT NULL,

	direccion  VARCHAR(60) NOT NULL,

	usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
	fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
	estado SMALLINT NOT NULL DEFAULT 1
	);

	CREATE PROC paClienteListar @parametro VARCHAR(50)
	AS
	SELECT id,nombre,primerApellido,segundoApellido,cedulaIdentidad,direccion,usuarioRegistro,fechaRegistro,estado 
	FROM Cliente
	WHERE estado<>-1 AND cedulaIdentidad LIKE '%'+REPLACE(@parametro,' ','%')+'%';

	select * from Cliente;
	-- TABLA VENTA
	CREATE TABLE Venta (
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	fecha DATE  NOT NULL,
	idUsuario INT NOT NULL,
	idCliente INT NOT NULL,
	usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
	fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
	estado SMALLINT NOT NULL DEFAULT 1,

	CONSTRAINT fk_Venta_Usuario FOREIGN KEY(idUsuario) REFERENCES Usuario(id),
	CONSTRAINT fk_Venta_Cliente FOREIGN KEY(idCliente) REFERENCES Cliente(id)
	);

	ALTER PROC paVentaListar @parametro VARCHAR(50)
	AS
	SELECT id,idCliente,idUsuario,fecha,usuarioRegistro,fechaRegistro,estado 
	FROM Venta
	WHERE estado<>-1 AND fecha LIKE '%'+REPLACE(@parametro,' ','%')+'%';




	-- TABLA VENTADETALLE
	CREATE TABLE VentaDetalle (
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	cantidad INT NOT NULL,
	precioUnitario FLOAT NOT NULL,
	precioTotal FLOAT NOT NULL,
	tipoPago VARCHAR(15) NOT NULL,
	idVenta INT NOT NULL,
	idArticulo INT NOT NULL,
	usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
	fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
	estado SMALLINT NOT NULL DEFAULT 1,
	CONSTRAINT fk_VentaDetalle_Venta FOREIGN KEY(idVenta) REFERENCES Venta(id),
	CONSTRAINT fk_VentaDetalle_Articulo FOREIGN KEY(idArticulo) REFERENCES Articulo(id)
	);
	ALTER PROC paVentaDetalleListar @parametro VARCHAR(50)
	AS
	SELECT vede.id,idArticulo,ar.descripcion as descripcion_articulo, ar.precio as precio_articulo,
	vede.cantidad,vede.precioUnitario,vede.precioTotal,vede.tipoPago,vede.usuarioRegistro,vede.fechaRegistro,vede.estado

	FROM VentaDetalle vede INNER JOIN Articulo ar ON vede.idArticulo = ar.id
	WHERE vede.estado<>-1 AND ar.descripcion LIKE '%'+REPLACE(@parametro,' ','%')+'%';

	select * from Articulo;
	select * from Venta;



