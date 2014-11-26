USE GD2C2014

GO

Create Schema GITAR_HEROES authorization gd

GO

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
			NULL,				-- En la tabla maestra no hay defenido tel�fono para el cliente
			Cliente_Dom_Calle,
			Cliente_Nro_Calle,
			Cliente_Piso,
			Cliente_Depto,
			1					-- Estado
		FROM gd_esquema.Maestra

--Las restricciones de la tabla Cliente no se hacen a nivel de base de datos por inconsistencias en el sistema viejo


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
			(codigo_rol smallint,
			username char(15),
			PRIMARY KEY (codigo_rol, username),
			FOREIGN KEY (codigo_rol) REFERENCES GITAR_HEROES.Rol,
			FOREIGN KEY (username) REFERENCES GITAR_HEROES.Usuario)

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
		VALUES (7, 'ABM de R�gimen de Estadia')

		INSERT INTO GITAR_HEROES.Funcionalidad (codigo, descripcion)
		VALUES (8, 'Generar o Modificar Reserva')
		
		INSERT INTO GITAR_HEROES.Funcionalidad (codigo, descripcion)
		VALUES (9, 'Cancelar Reserva')
		
		INSERT INTO GITAR_HEROES.Funcionalidad (codigo, descripcion)
		VALUES (10, 'Registrar estadia')
		
		INSERT INTO GITAR_HEROES.Funcionalidad (codigo, descripcion)
		VALUES (11, 'Registrar Consumibles')
		
		INSERT INTO GITAR_HEROES.Funcionalidad (codigo, descripcion)
		VALUES (12, 'Facturar Publicaciones')
		
				INSERT INTO GITAR_HEROES.Funcionalidad (codigo, descripcion)
		VALUES (13, 'Listado Estadistico')


	-- /////////////// ROLFUNCIONALIDAD ///////////////

		CREATE Table GITAR_HEROES.RolFuncionalidad
			(codigo_rol smallint,
			codigo_funcionalidad smallint,
			PRIMARY KEY (codigo_rol, codigo_funcionalidad),
			FOREIGN KEY (codigo_rol) REFERENCES GITAR_HEROES.Rol,
			FOREIGN KEY (codigo_funcionalidad) REFERENCES GITAR_HEROES.Funcionalidad)

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


	-- /////////////// REGIMEN ///////////////

		CREATE Table GITAR_HEROES.Regimen
			(codigo smallint IDENTITY PRIMARY KEY,
			descripcion varchar(60),
			precio_base decimal(10,2))

		INSERT INTO GITAR_HEROES.Regimen
		SELECT Regimen_Descripcion, Regimen_Precio
		FROM gd_esquema.Maestra
		GROUP BY Regimen_Descripcion, Regimen_Precio


	-- /////////////// REGIMENHOTEL ///////////////

		CREATE Table GITAR_HEROES.RegimenHotel
			(codigo_hotel int,
			codigo_regimen smallint,
			PRIMARY KEY (codigo_hotel, codigo_regimen),
			FOREIGN KEY (codigo_hotel) REFERENCES GITAR_HEROES.Hotel,
			FOREIGN KEY (codigo_regimen) REFERENCES GITAR_HEROES.Regimen)

		INSERT INTO GITAR_HEROES.RegimenHotel
		SELECT Hotel.codigo, Regimen.codigo
		FROM gd_esquema.Maestra Maestra JOIN GITAR_HEROES.Hotel Hotel ON Maestra.Hotel_Ciudad = Hotel.ciudad
			 JOIN GITAR_HEROES.Regimen Regimen ON Maestra.Regimen_Descripcion = Regimen.descripcion
		GROUP BY Hotel.codigo, Regimen.codigo
		ORDER BY Hotel.codigo, Regimen.codigo


	-- /////////////// TIPOHABITACION ///////////////

		CREATE Table GITAR_HEROES.TipoHabitacion
			(codigo int PRIMARY KEY,
			descripcion varchar(60),
			porcentual decimal(4,2))

		INSERT INTO GITAR_HEROES.TipoHabitacion
		SELECT DISTINCT
			   Habitacion_Tipo_Codigo,
			   Habitacion_Tipo_Descripcion,
			   Habitacion_Tipo_Porcentual
		FROM gd_esquema.Maestra
		ORDER BY Habitacion_Tipo_Codigo


	-- /////////////// HABITACION ///////////////

		CREATE Table GITAR_HEROES.Habitacion
			  (codigo_hotel int,
			   numero smallint,
			   piso smallint NOT NULL,
			   tipo int NOT NULL,
			   ubicacion varchar(60),
			   estado smallint NOT NULL,
			   PRIMARY KEY (codigo_hotel, numero),
			   FOREIGN KEY (codigo_hotel) REFERENCES GITAR_HEROES.Hotel,
			   FOREIGN KEY (tipo) REFERENCES GITAR_HEROES.TipoHabitacion)

		INSERT INTO GITAR_HEROES.Habitacion 
		SELECT DISTINCT
			  (SELECT Hotel.codigo FROM GITAR_HEROES.Hotel Hotel WHERE M.Hotel_Calle = Hotel.domicilio_calle AND M.Hotel_Nro_Calle = Hotel.domicilio_numero),
			   Habitacion_Numero,
			   Habitacion_Piso,
			   Habitacion_Tipo_Codigo,
			   CASE Habitacion_Frente WHEN 'S' THEN 'Vista al exterior' ELSE 'Interno' END,
			   1						-- Corresponde al estado que por defecto se encuentra "habilitado"

		FROM gd_esquema.Maestra M
		--ORDER BY 1, Habitacion_Numero

	-- /////////////// HOTELINHABILITADO ///////////////

		CREATE Table GITAR_HEROES.HotelInhabilitado
			(codigo_hotel int,
			fecha_inicio smalldatetime,
			fecha_fin smalldatetime,
			descripcion varchar(60),
			PRIMARY KEY (codigo_hotel, fecha_inicio),
			FOREIGN KEY (codigo_hotel) REFERENCES GITAR_HEROES.Hotel)


	-- /////////////// TIPOESTADORESERVA ///////////////

		CREATE Table GITAR_HEROES.TipoEstadoReserva
			(codigo smallint IDENTITY PRIMARY KEY,
			descripcion varchar(60))

		INSERT INTO GITAR_HEROES.TipoEstadoReserva (descripcion)
		VALUES ('Reserva correcta')

		INSERT INTO GITAR_HEROES.TipoEstadoReserva (descripcion)
		VALUES ('Reserva modificada')

		INSERT INTO GITAR_HEROES.TipoEstadoReserva (descripcion)
		VALUES ('Reserva cancelada por Recepcion')

		INSERT INTO GITAR_HEROES.TipoEstadoReserva (descripcion)
		VALUES ('Reserva cancelada por Cliente')

		INSERT INTO GITAR_HEROES.TipoEstadoReserva (descripcion)
		VALUES ('Reserva cancelada por No-Show')

		INSERT INTO GITAR_HEROES.TipoEstadoReserva (descripcion)
		VALUES ('Reserva con ingreso')


	-- /////////////// RESERVA ///////////////

		
		CREATE Table GITAR_HEROES.Reserva
			(codigo int PRIMARY KEY,
			fecha_reserva smalldatetime NOT NULL,
			fecha_inicio smalldatetime NOT NULL,
			fecha_fin smalldatetime,		--cant_noches
			codigo_hotel int,
			codigo_regimen smallint,
			tipo_doc_cliente smallint,
			nro_doc_cliente numeric(11,0),
			costo_total decimal (10,2),
			codigo_estado smallint,
			FOREIGN KEY (codigo_hotel) REFERENCES GITAR_HEROES.Hotel,
			FOREIGN KEY (codigo_estado) REFERENCES GITAR_HEROES.TipoEstadoReserva)


		INSERT INTO GITAR_HEROES.Reserva
		SELECT DISTINCT
			   Reserva_Codigo,
		-- NO existe fecha de realizacion en el sistema, tomo por defecto fecha de inicio
			   Reserva_Fecha_Inicio,							
			   Reserva_Fecha_Inicio,
			   Reserva_Fecha_Inicio + Reserva_Cant_Noches,		-- fecha_fin
		-- Se busca el codigo del hotel desde la tabla Hotel
			  (SELECT Hotel.codigo FROM GITAR_HEROES.Hotel Hotel 
			   WHERE Hotel.ciudad = Hotel_Ciudad AND 
					 Hotel.domicilio_calle = Hotel_Calle 
					 AND Hotel.domicilio_numero = Hotel_Nro_Calle),		-- codigo_hotel
		-- Se busca el codigo del regimen desde la tabla Regimen
			  (SELECT Regimen.codigo FROM GITAR_HEROES.Regimen Regimen 
			   WHERE Regimen.descripcion = Regimen_Descripcion),		-- codigo_regimen
			  1,	-- tipo_doc_cliente
		-- Se busca el numero de documento desde la tabla Cliente
			  (SELECT Cliente.nro_doc FROM GITAR_HEROES.Cliente Cliente 
			   WHERE Cliente.nro_doc = Cliente_Pasaporte_Nro 
			 		 AND Cliente.mail = Cliente_Mail),					-- nro_doc_cliente
			   Regimen_Precio * Habitacion_Tipo_Porcentual + Hotel_CantEstrella * Hotel_Recarga_Estrella,		-- costo_total
		-- Si la reserva tiene factura se la considera de estado efectivizada sino simplemente correcta
			   CASE WHEN EXISTS(SELECT 1 FROM gd_esquema.Maestra WHERE M.Reserva_Codigo = Reserva_Codigo AND Factura_Nro IS NOT NULL) 
			   THEN 6 ELSE 1 END
	      
		FROM gd_esquema.Maestra M
		--ORDER BY Reserva_Codigo
		

	-- /////////////// USUARIORESERVA ///////////////

		CREATE Table GITAR_HEROES.UsuarioReserva
			(codigo_reserva int, 
			username char(15),
			descripci�n varchar(30),
			PRIMARY KEY (codigo_reserva, username),
			FOREIGN KEY (codigo_reserva) REFERENCES GITAR_HEROES.Reserva,
			FOREIGN KEY (username) REFERENCES GITAR_HEROES.Usuario)


	-- /////////////// RESERVAHABITACION ///////////////

		CREATE Table GITAR_HEROES.ReservaHabitacion
			  (codigo_reserva int, 
			   codigo_hotel int,
			   numero_habitacion smallint,
			   PRIMARY KEY (codigo_reserva, codigo_hotel, numero_habitacion),
			   FOREIGN KEY (codigo_reserva) REFERENCES GITAR_HEROES.Reserva,
			   FOREIGN KEY (codigo_hotel, numero_habitacion) REFERENCES GITAR_HEROES.Habitacion)

/*
	-- /////////////// RESERVACANCELADA ///////////////

		CREATE Table GITAR_HEROES.ReservaCancelada
			(codigo_reserva int PRIMARY KEY,
			fecha:cancelacion smalldatetime,
			descripci�n_motivo varchar(60),
			username char(15),
			FOREIGN KEY (codigo_reserva) REFERENCES (GITAR_HEROES.Reserva)
			FOREIGN KEY (username) REFERENCES (GITAR_HEROES.Usuario))


	-- /////////////// TIPOCONSUMIBLE ///////////////

		CREATE Table GITAR_HEROES.TipoConsumible
			(codigo_consumible int PRIMARY KEY,
			descripci�n_motivo varchar(60),


	-- /////////////// ESTADIA ///////////////

		CREATE Table GITAR_HEROES.Estadia
			(codigo_reserva int PRIMARY KEY,
			username_ingreso char(15),
			fecha_egreso smalldatetime,		-- cant_noches
			username_egreso char(15),
			FOREIGN KEY (codigo_reserva) REFERENCES (GITAR_HEROES.Reserva),
			FOREIGN KEY (username_ingreso) REFERENCES (GITAR_HEROES.Usuario),
			FOREIGN KEY (username_egreso) REFERENCES (GITAR_HEROES.Usuario))

		--INSERT INTO GITAR_HEROES.Estadia
		--SELECT
		--FROM gd_esquema.Maestra


	-- /////////////// CONSUMIBLESADQUIRIDOS ///////////////

		CREATE Table GITAR_HEROES.ConsumiblesAdquiridos
			(codigo_consumible int,
			codigo_reserva int,
			numero_habitacion int,
			cantidad int NOT NULL,
			PRIMARY KEY (codigo_consumible, codigo_reserva, numero_habitacion),
		
			FOREIGN KEY (codigo_consumible) REFERENCES (GITAR_HEROES.),
		-- Podr�an referenciar la tabla ReservaHabitacion ----------------------------
			-- Podr�a referenciar la tabla Estad�a -------------------------------
			FOREIGN KEY (codigo_reserva) REFERENCES (GITAR_HEROES.Reserva),	      
			FOREIGN KEY (numero_habitacion) REFERENCES (GITAR_HEROES.habitacion))
		------------------------------------------------------------------------------
		
		--INSERT INTO GITAR_HEROES.ConsumiblesAdquiridos
		--SELECT 
		--FROM gd_esquema.Maestra


	-- /////////////// TIPOPAGO ///////////////

		CREATE Table GITAR_HEROES.TipoPago
			(codigo smallint PRIMARY KEY,
			descripcion varchar(30) NOT NULL)


	-- /////////////// FACTURA ///////////////

		CREATE Table GITAR_HEROES.Factura
			(numero_factura int PRIMARY KEY,
			tipo_doc_cliente smallint, 
			nro_doc_cliente numeric(11,0), 
			codigo_hotel int,
			fecha smalldatetime,
			total decimal(10,2),
			codigo_tipo_pago smallint,
			nro_tarjeta numeric(17),
			dias_cumplidos int NOT NULL,
			dias_faltantes int NOT NULL,
			-- Podr�an referenciar la tabla Reserva ---------------------------------------
			FOREIGN KEY (tipo_doc_cliente, nro_doc_cliente) REFERENCES (GITAR_HEROES.Cliente),
			FOREIGN KEY (codigo_hotel) REFERENCES (GITAR_HEROES.HOTEL),
			FOREIGN KEY (codigo_tipo_pago) REFERENCES (GITAR_HEROES.TipoPago))

		--INSERT INTO GITAR_HEROES.Factura
		--SELECT 
		--FROM gd_esquema.Maestra


	-- /////////////// ITEMFACTURA ///////////////

		CREATE Table GITAR_HEROES.ItemFactura
			(numero_item smallint,
			numero_factura int,
			codigo_consumible int,
			cantidad int,
			PRIMARY KEY (numero_item, numero_factura),
			FOREIGN KEY (numero_factura) REFERENCES (GITAR_HEROES.Factura),
			FOREIGN KEY (codigo_consumible) REFERENCES (GITAR_HEROES.ConsumiblesAdquiridos))

*/



	END

GO

CREATE Procedure GITAR_HEROES.borrarTablas
AS
	BEGIN

	-- BORRADO DE TABLAS		
/*
		DROP Table GITAR_HEROES.ItemFactura
		DROP Table GITAR_HEROES.Factura
		DROP Table GITAR_HEROES.TipoPago
		DROP Table GITAR_HEROES.ConsumiblesAdquiridos
		DROP Table GITAR_HEROES.Estadia
		DROP Table GITAR_HEROES.TipoConsumible
		DROP Table GITAR_HEROES.ReservaCancelada
		DROP Table GITAR_HEROES.ReservaHabitacion
*/
		DROP Table GITAR_HEROES.UsuarioReserva
		DROP Table GITAR_HEROES.Reserva
		DROP Table GITAR_HEROES.TipoEstadoReserva
		DROP Table GITAR_HEROES.HotelInhabilitado
		DROP Table GITAR_HEROES.Habitacion
		DROP Table GITAR_HEROES.TipoHabitacion
		DROP Table GITAR_HEROES.RegimenHotel
		DROP Table GITAR_HEROES.Regimen
		DROP Table GITAR_HEROES.RolFuncionalidad
		DROP Table GITAR_HEROES.Funcionalidad
		DROP Table GITAR_HEROES.RolUsuario
		DROP Table GITAR_HEROES.Rol
		DROP Table GITAR_HEROES.UsuarioHotel
		DROP Table GITAR_HEROES.Usuario
		DROP Table GITAR_HEROES.Cliente
		DROP Table GITAR_HEROES.TipoDocumento
		DROP Table GITAR_HEROES.Hotel
		
	-- BORRADO DE PROCEDIMIENTOS ALMACENADOS	
		DROP Procedure GITAR_HEROES.crearTablas
		DROP Procedure GITAR_HEROES.borrarTablas

	-- BORRADO DEL ESQUEMA		
		DROP Schema GITAR_HEROES
		
	END	

GO
-- ////////////////////  EJECUCI�N DE PROCEDIMIENTOS ////////////////////

EXEC GITAR_HEROES.crearTablas

--EXEC GITAR_HEROES.borrarTablas