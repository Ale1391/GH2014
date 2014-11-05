CREATE Procedure GITAR_HEROES.crearTablas
AS
	BEGIN

-- ////////////////////  CREACION DE TABLAS ////////////////////

	-- /////////////// HOTEL ///////////////
		
		CREATE Table GITAR_HEROES.Hotel
			  (codigo int IDENTITY,
			   nombre varchar(50),
			   domicilio_calle varchar(60),
			   domicilio_numero int,
			   ciudad varchar(50),
			   pais varchar(30),
			   telefono bigint,
			   cant_estrellas smallint,
			   recarga_estrellas decimal(10,2),
			   estado smallint,
			   PRIMARY KEY (codigo))
	
		INSERT INTO GITAR_HEROES.Hotel (domicilio_calle, domicilio_numero, ciudad, pais, cant_estrellas, recarga_estrellas, estado)
		SELECT DISTINCT
			   Hotel_Calle,
			   Hotel_Nro_Calle,
			   Hotel_Ciudad,
			   'Argentina',
			   Hotel_CantEstrella,
			   Hotel_Recarga_Estrella,
			   1							-- Consideramos por defecto a todos los hoteles habilitados
		FROM gd_esquema.Maestra


	-- //////////////////// TIPODOCUMENTO ////////////////////

		CREATE Table GITAR_HEROES.TipoDocumento
			(codigo smallint IDENTITY PRIMARY KEY,
			descripcion varchar(20) NOT NULL)

		
		INSERT INTO GITAR_HEROES.TipoDocumento (descripcion)
		VALUES ('Pasaporte')

		INSERT INTO GITAR_HEROES.TipoDocumento (descripcion)
		VALUES ('DNI')

		INSERT INTO GITAR_HEROES.TipoDocumento (descripcion)
		VALUES ('LC')

		INSERT INTO GITAR_HEROES.TipoDocumento (descripcion)
		VALUES ('LE')


	-- /////////////// USUARIO ///////////////

		CREATE Table GITAR_HEROES.Usuario
			(username char(15),
			password char(15) NOT NULL,
			nombre varchar(50) NOT NULL,
			apellido varchar(50) NOT NULL,
			tipo_doc smallint NOT NULL,
			nro_doc int NOT NULL,
			fecha_nacimiento smalldatetime,
			domicilio_calle varchar(60),
			domicilio_numero int,
			domicilio_piso smallint,
			domicilio_depto char(1),
			mail varchar(50) NOT NULL UNIQUE,
			telefono bigint,
			estado smallint,
			estado_sistema smallint,
			PRIMARY KEY (username),
			FOREIGN KEY (tipo_doc) REFERENCES GITAR_HEROES.TipoDocumento)
		

	-- /////////////// USUARIOHOTEL ///////////////
		
		CREATE Table GITAR_HEROES.UsuarioHotel
			(codigo_hotel int,
			username char(15),
			PRIMARY KEY (codigo_hotel, username))


	-- /////////////// ROL ///////////////

		CREATE Table GITAR_HEROES.Rol
			(codigo smallint IDENTITY PRIMARY KEY,
			descripcion varchar(60) NOT NULL,
			estado smallint)

		INSERT INTO	GITAR_HEROES.Rol (descripcion, estado)
		VALUES ('Administrador', 1)

		INSERT INTO	GITAR_HEROES.Rol (descripcion, estado)
		VALUES ('Recepcionista', 1)
		
		INSERT INTO	GITAR_HEROES.Rol (descripcion, estado)
		VALUES ('Guest', 1)	


	-- /////////////// ROLUSUARIO ///////////////

		CREATE Table GITAR_HEROES.RolUsuario
			(codigo_rol int,
			username varchar(15),
			PRIMARY KEY (codigo_rol, username))


	-- /////////////// FUNCIONALIDAD ///////////////

		CREATE Table GITAR_HEROES.Funcionalidad
			(codigo smallint PRIMARY KEY,
			descripcion varchar(60) NOT NULL)

		INSERT INTO GITAR_HEROES.Funcionalidad (codigo, descripcion)
		VALUES (1, 'ABM de Rol')

		INSERT INTO GITAR_HEROES.Funcionalidad (codigo, descripcion)
		VALUES (3, 'ABM de Usuario')

		INSERT INTO GITAR_HEROES.Funcionalidad (codigo, descripcion)
		VALUES (4, 'ABM de Clientes')
		
		INSERT INTO GITAR_HEROES.Funcionalidad (codigo, descripcion)
		VALUES (5, 'ABM de Hotel')
		
		INSERT INTO GITAR_HEROES.Funcionalidad (codigo, descripcion)
		VALUES (6, 'ABM de Habitacion')
		
		INSERT INTO GITAR_HEROES.Funcionalidad (codigo, descripcion)
		VALUES (7, 'ABM de Régimen de Estadia')

		INSERT INTO GITAR_HEROES.Funcionalidad (codigo, descripcion)
		VALUES (8, 'Generar o Modificar Reserva')
		
		INSERT INTO GITAR_HEROES.Funcionalidad (codigo, descripcion)
		VALUES (9, 'Registrar estadia')
		
		INSERT INTO GITAR_HEROES.Funcionalidad (codigo, descripcion)
		VALUES (10, 'Registrar Consumibles')
		
		INSERT INTO GITAR_HEROES.Funcionalidad (codigo, descripcion)
		VALUES (11, 'Facturar Publicaciones')
		
				INSERT INTO GITAR_HEROES.Funcionalidad (codigo, descripcion)
		VALUES (13, 'Listado Estadistico')


	-- /////////////// ROLFUNCIONALIDAD ///////////////

		CREATE Table GITAR_HEROES.RolFuncionalidad
			(codigo_rol int,
			codigo_funcionalidad int,
			PRIMARY KEY (codigo_rol, codigo_funcionalidad))

		-- Administrador - ABM de Rol
		INSERT INTO GITAR_HEROES.RolFuncionalidad
		VALUES (1, 1)

		-- Administrador - ABM de Usuario
		INSERT INTO GITAR_HEROES.RolFuncionalidad
		VALUES (1, 3)

		-- Administrador - ABM de Clientes
		INSERT INTO GITAR_HEROES.RolFuncionalidad
		VALUES (1, 4)

		-- Administrador - ABM de Hotel
		INSERT INTO GITAR_HEROES.RolFuncionalidad
		VALUES (1, 5)

		-- Administrador - ABM de Habitacion
		INSERT INTO GITAR_HEROES.RolFuncionalidad
		VALUES (1, 6)

		-- Administrador - ABM de Regimen de Estadia
		INSERT INTO GITAR_HEROES.RolFuncionalidad
		VALUES (1, 7)

		-- Administrador - Registrar Consumibles
		INSERT INTO GITAR_HEROES.RolFuncionalidad
		VALUES (1, 11)

		-- Administrador - Facturar Publicaciones
		INSERT INTO GITAR_HEROES.RolFuncionalidad
		VALUES (1, 12)

		-- Administrador - Listado estadistico
		INSERT INTO GITAR_HEROES.RolFuncionalidad
		VALUES (1, 13)
		
		
		-- Recepcionista - ABM de Clientes
		INSERT INTO GITAR_HEROES.RolFuncionalidad
		VALUES (2, 4)

		-- Recepcionista - Generar o Modificar Reserva
		INSERT INTO GITAR_HEROES.RolFuncionalidad
		VALUES (2, 8)

		-- Recepcionista - Cancelar Reserva
		INSERT INTO GITAR_HEROES.RolFuncionalidad
		VALUES (2, 9)

		-- Recepcionista - Registrar Estadia
		INSERT INTO GITAR_HEROES.RolFuncionalidad
		VALUES (2, 10)


		-- Guest - Generar o Modificar Reserva
		INSERT INTO GITAR_HEROES.RolFuncionalidad
		VALUES (3, 8)

		-- Guest - Generar o Modificar Reserva
		INSERT INTO GITAR_HEROES.RolFuncionalidad
		VALUES (3, 9)



/*			
	-- /////////////// CLIENTE ///////////////


			CREATE Table GITAR_HEROES.Cliente
				(nombre varchar(50) NOT NULL,
				apellido varchar(50) NOT NULL,
				tipo_doc smallint NOT NULL,
				nro_doc int NOT NULL,
				fecha_nacimiento smalldatetime,
				nacionalidad varchar(30),
				mail varchar(50) NOT NULL,
				telefono bigint,
				domicilio_calle varchar(60),
				domicilio_numero int,
				domicilio_piso smallint,
				domicilio_depto char,
				estado smallint,
				FOREIGN KEY (tipo_doc) REFERENCES GITAR_HEROES.TipoDocumento)
	
		INSERT INTO GITAR_HEROES.Cliente
		SELECT DISTINCT
			Cliente_Nombre, 
			Cliente_Apellido, 
			1,					-- Tipo documento, ya que son todos pasaportes
			Cliente_Pasaporte_Nro,
			Cliente_Fecha_Nac,
			Cliente_Nacionalidad,
			Cliente_Mail,
			NULL,				-- En la tabla maestra no hay defenido teléfono para el cliente
			Cliente_Dom_Calle,
			Cliente_Nro_Calle,
			Cliente_Piso,
			Cliente_Depto,
			1					-- Estado
		FROM gd_esquema.Maestra

--Las restricciones de la tabla Cliente no se hacen a nivel de base de datos por inconsistencias en el sistema viejo
*/

	END

GO

CREATE Procedure GITAR_HEROES.borrarTablas
AS
	BEGIN

	-- BORRADO DE TABLAS		
		DROP Table GITAR_HEROES.RolFuncionalidad
		DROP Table GITAR_HEROES.Funcionalidad
		DROP Table GITAR_HEROES.RolUsuario
		DROP Table GITAR_HEROES.Rol
		DROP Table GITAR_HEROES.UsuarioHotel
		DROP Table GITAR_HEROES.Usuario
		--DROP Table GITAR_HEROES.Cliente
		DROP Table GITAR_HEROES.TipoDocumento
		DROP Table GITAR_HEROES.Hotel
		
	-- BORRADO DE PROCEDIMIENTOS ALMACENADOS	
		DROP Procedure GITAR_HEROES.crearTablas
		
	END	

--DROP Procedure GITAR_HEROES.borrarTablas

-- ////////////////////  EJECUCIÓN DE PROCEDIMIENTOS ////////////////////

GO

EXEC GITAR_HEROES.crearTablas