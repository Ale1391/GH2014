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
			   fecha_creacion smalldatetime,
			   mail varchar(50),
			   estado smallint,
			   PRIMARY KEY (codigo))
	
		INSERT INTO GITAR_HEROES.Hotel (domicilio_calle, domicilio_numero, ciudad, pais, cant_estrellas, recarga_estrellas, fecha_creacion, mail, estado)
		SELECT DISTINCT
			   Hotel_Calle,
			   Hotel_Nro_Calle,
			   Hotel_Ciudad,
			   'Argentina',
			   Hotel_CantEstrella,
			   Hotel_Recarga_Estrella,
			   NULL,						-- fecha_creacion
			   NULL,						-- mail
			   1							-- Consideramos por defecto a todos los hoteles habilitados
		FROM gd_esquema.Maestra

		-- Se carga un nombre supuesto a cada hotel
		UPDATE GITAR_HEROES.Hotel
		SET nombre = 'Congreso'
		WHERE codigo = 1
		
		UPDATE GITAR_HEROES.Hotel
		SET nombre = 'Roberto Goyeneche'
		WHERE codigo = 2
		
		UPDATE GITAR_HEROES.Hotel
		SET nombre = 'Israel'
		WHERE codigo = 3
		
		UPDATE GITAR_HEROES.Hotel
		SET nombre = 'Gaona'
		WHERE codigo = 4
		
		UPDATE GITAR_HEROES.Hotel
		SET nombre = 'Alicia Moreau de Justo'
		WHERE codigo = 5
		
		UPDATE GITAR_HEROES.Hotel
		SET nombre = 'Israel II'
		WHERE codigo = 6
		
		UPDATE GITAR_HEROES.Hotel
		SET nombre = 'Alvarez Jonte'
		WHERE codigo = 7
		
		UPDATE GITAR_HEROES.Hotel
		SET nombre = 'Dorrego'
		WHERE codigo = 8

		UPDATE GITAR_HEROES.Hotel
		SET nombre = 'Rivadavia'
		WHERE codigo = 9
		
		UPDATE GITAR_HEROES.Hotel
		SET nombre = 'Reconquista'
		WHERE codigo = 10
		
		UPDATE GITAR_HEROES.Hotel
		SET nombre = 'Figueroa Alcorta'
		WHERE codigo = 11

		UPDATE GITAR_HEROES.Hotel
		SET nombre = 'Monroe'
		WHERE codigo = 12
		
		UPDATE GITAR_HEROES.Hotel
		SET nombre = 'Jujuy'
		WHERE codigo = 13
		
		UPDATE GITAR_HEROES.Hotel
		SET nombre = 'San Isidro Labrador'
		WHERE codigo = 14
		
		UPDATE GITAR_HEROES.Hotel
		SET nombre = 'San Juan'
		WHERE codigo = 15
		
		UPDATE GITAR_HEROES.Hotel
		SET nombre = 'Galvan'
		WHERE codigo = 16
		
		UPDATE GITAR_HEROES.Hotel
		SET nombre = 'Scalabrini Ortiz'
		WHERE codigo = 17		
		
		UPDATE GITAR_HEROES.Hotel
		SET nombre = 'La Rabida'
		WHERE codigo = 18


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
			localidad varchar(60),
			pais_origen varchar(50),
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
			NULL,				-- localidad
			NULL,				-- pais_origen
			1					-- estado
		
		FROM gd_esquema.Maestra

-- ACLARACION: Las restricciones de la tabla Cliente no se hacen a nivel de base de datos por inconsistencias en el sistema viejo


	-- /////////////// USUARIO ///////////////

		CREATE Table GITAR_HEROES.Usuario
			(username char(15),
			password char(64) NOT NULL,
			nombre varchar(50) NOT NULL,
			apellido varchar(50) NOT NULL,
			tipo_doc smallint NOT NULL,
			nro_doc int NOT NULL,
			fecha_nacimiento smalldatetime NULL,
			domicilio varchar(60),
			mail varchar(50) NOT NULL UNIQUE,
			telefono bigint NULL,
			estado smallint NOT NULL,
			estado_sistema smallint NOT NULL,
			PRIMARY KEY (username),
			FOREIGN KEY (tipo_doc) REFERENCES GITAR_HEROES.TipoDocumento)
		

	-- /////////////// USUARIOHOTEL ///////////////
		
		CREATE Table GITAR_HEROES.UsuarioHotel
			(codigo_hotel int,
			username char(15),
			PRIMARY KEY (codigo_hotel, username),
			FOREIGN KEY (codigo_hotel) REFERENCES GITAR_HEROES.Hotel,
			FOREIGN KEY (username) REFERENCES GITAR_HEROES.Usuario)


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
		VALUES (7, 'ABM de Régimen de Estadia')

		INSERT INTO GITAR_HEROES.Funcionalidad (codigo, descripcion)
		VALUES (8, 'Generar o Modificar Reserva')
		
		INSERT INTO GITAR_HEROES.Funcionalidad (codigo, descripcion)
		VALUES (9, 'Cancelar Reserva')
		
		INSERT INTO GITAR_HEROES.Funcionalidad (codigo, descripcion)
		VALUES (10, 'Registrar estadia')
		
		INSERT INTO GITAR_HEROES.Funcionalidad (codigo, descripcion)
		VALUES (11, 'Registrar Consumibles')
		
		INSERT INTO GITAR_HEROES.Funcionalidad (codigo, descripcion)
		VALUES (12, 'Facturar Estadia')
		
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
		
		-- Administrador - Generar o Modificar Reserva
		INSERT INTO GITAR_HEROES.RolFuncionalidad
		VALUES (1, 8)

		-- Administrador - Cancelar Reserva
		INSERT INTO GITAR_HEROES.RolFuncionalidad
		VALUES (1, 9)

		-- Administrador - Registrar Estadia
		INSERT INTO GITAR_HEROES.RolFuncionalidad
		VALUES (1, 10)

		-- Administrador - Registrar Consumibles
		INSERT INTO GITAR_HEROES.RolFuncionalidad
		VALUES (1, 11)

		-- Administrador - Facturar Estadia
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
			   descripcion_comodidades varchar(100),
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
			   CASE Habitacion_Frente WHEN 'S' THEN 'Vista al exterior' ELSE 'Interno' END,		-- ubicacion
			   NULL,					-- descripcion_comodidades
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
			costo_base decimal (10,2),
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
			   (Regimen_Precio * Habitacion_Tipo_Porcentual + Hotel_CantEstrella * Hotel_Recarga_Estrella) * M.Reserva_Cant_Noches,		-- costo_base
		-- Si la reserva tiene factura se la considera de estado efectivizada sino simplemente correcta
			   CASE WHEN EXISTS(SELECT 1 FROM gd_esquema.Maestra WHERE M.Reserva_Codigo = Reserva_Codigo AND Factura_Nro IS NOT NULL) 
			   THEN 6 ELSE 1 END
	      
		FROM gd_esquema.Maestra M
		--ORDER BY Reserva_Codigo
		

	-- /////////////// USUARIORESERVA ///////////////

		CREATE Table GITAR_HEROES.UsuarioReserva
			(codigo_reserva int, 
			username char(15),
			descripción varchar(30),
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
			   
			   
		INSERT INTO GITAR_HEROES.ReservaHabitacion
		SELECT Reserva_Codigo,
			  (SELECT Hotel.codigo FROM GITAR_HEROES.Hotel Hotel 
			   WHERE M.Hotel_Calle = Hotel.domicilio_calle AND M.Hotel_Nro_Calle = Hotel.domicilio_numero),		--codigo_hotel
			   Habitacion_Numero

		FROM gd_esquema.Maestra M
		GROUP BY Reserva_Codigo, Hotel_Calle, Hotel_Nro_Calle, Habitacion_Numero

	

	-- /////////////// RESERVACANCELADA ///////////////

		CREATE Table GITAR_HEROES.ReservaCancelada
			(codigo_reserva int PRIMARY KEY,
			fecha_cancelacion smalldatetime,
			descripcion_motivo varchar(60),
			username char(15),
		-- Podria considerarse username tambien como clave primaria
			FOREIGN KEY (codigo_reserva) REFERENCES GITAR_HEROES.Reserva,
			FOREIGN KEY (username) REFERENCES GITAR_HEROES.Usuario)


	-- /////////////// TIPOCONSUMIBLE ///////////////

		CREATE Table GITAR_HEROES.TipoConsumible
			(codigo int PRIMARY KEY,
			descripción varchar(60),
			precio decimal(10,2))
			
		INSERT INTO GITAR_HEROES.TipoConsumible
		SELECT DISTINCT Consumible_Codigo, Consumible_Descripcion, Consumible_Precio
		FROM gd_esquema.Maestra
		WHERE Consumible_Codigo IS NOT NULL
		
		-- Se inserta el tipo de consumible -1 que correspondera al final de la carga de consumibles por estadia
		INSERT INTO GITAR_HEROES.TipoConsumible
		VALUES (-1, 'Finalizacion carga consumibles', NULL)
		
		-- Se inserta el tipo de consumible -2 que correspondera a Otros.
		INSERT INTO GITAR_HEROES.TipoConsumible
		VALUES (-2, 'Otros', NULL)
		

	-- /////////////// ESTADIA ///////////////

		CREATE Table GITAR_HEROES.Estadia
			(codigo_reserva int,
			fecha_ingreso smalldatetime,
			username_ingreso char(15),
			fecha_egreso smalldatetime,		-- cant_noches
			username_egreso char(15),
			PRIMARY KEY (codigo_reserva),
			FOREIGN KEY (codigo_reserva) REFERENCES GITAR_HEROES.Reserva,
			FOREIGN KEY (username_ingreso) REFERENCES GITAR_HEROES.Usuario,
			FOREIGN KEY (username_egreso) REFERENCES GITAR_HEROES.Usuario)

		INSERT INTO GITAR_HEROES.Estadia
		SELECT DISTINCT
			   Reserva_Codigo,
			   Estadia_Fecha_Inicio,
			   NULL,		-- username_ingreso
			   Estadia_Fecha_Inicio + Estadia_Cant_Noches,		-- fecha_egreso
			   NULL			-- username_egreso
			     
		FROM gd_esquema.Maestra M
		WHERE (SELECT Reserva.codigo_estado FROM GITAR_HEROES.Reserva Reserva WHERE Reserva.codigo = M.Reserva_Codigo) = 6
			   AND Estadia_Fecha_Inicio IS NOT NULL


	-- /////////////// CONSUMIBLEADQUIRIDO ///////////////

		CREATE Table GITAR_HEROES.ConsumibleAdquirido
			(codigo_consumible int,
			codigo_reserva int,
			cantidad int NOT NULL,
			leyenda varchar(32),
			PRIMARY KEY (codigo_consumible, codigo_reserva),
			FOREIGN KEY (codigo_consumible) REFERENCES GITAR_HEROES.TipoConsumible,
			FOREIGN KEY (codigo_reserva) REFERENCES GITAR_HEROES.Estadia)
		
		INSERT INTO GITAR_HEROES.ConsumibleAdquirido
		SELECT DISTINCT 
			   Consumible_Codigo,
			   Reserva_Codigo,
			   SUM(Item_Factura_Cantidad),
			   'Consumible'			-- leyenda
			   
		FROM gd_esquema.Maestra M
		WHERE Consumible_Codigo IS NOT NULL
		GROUP BY Consumible_Codigo, Reserva_Codigo
		
		-- Se crea una tabla temporal para almacenar las reservas que no tienen asociados consumibles pero el valor de la factura es mayor que 0
		SELECT Reserva_Codigo, Factura_Nro, Factura_Total
		INTO #ReservasConConsumiblesNoRegistrados
		FROM gd_esquema.Maestra M
		WHERE NOT EXISTS (SELECT 1 FROM gd_esquema.Maestra WHERE Reserva_Codigo = M.Reserva_Codigo AND Consumible_Codigo IS NOT NULL)
			  AND Factura_Total <> 0
		--ORDER BY Reserva_Codigo DESC
		
		-- Se inserta un consumible extra para los casos registrados en la tabla temporal #ReservasConConsumiblesNoRegistrados
		INSERT INTO GITAR_HEROES.ConsumibleAdquirido
		SELECT -2,					-- codigo_consumibe
			   Reserva_Codigo,
			   1,					-- cantidad
			   'Otros consumibles'	-- leyenda
			   
		FROM #ReservasConConsumiblesNoRegistrados


	-- /////////////// OTROCONSUMIBLEADQUIRIDO ///////////////

		CREATE Table GITAR_HEROES.OtroConsumibleAdquirido
					(codigo_reserva int,
					 costo numeric(10,2),
					 PRIMARY KEY (codigo_reserva),
					 FOREIGN KEY (codigo_reserva) REFERENCES GITAR_HEROES.Estadia)
		
		INSERT INTO GITAR_HEROES.OtroConsumibleAdquirido
		SELECT Reserva_Codigo,
			   Factura_Total	
		FROM #ReservasConConsumiblesNoRegistrados


	-- /////////////// TIPOPAGO ///////////////

		CREATE Table GITAR_HEROES.TipoPago
			  (codigo smallint PRIMARY KEY,
			   descripcion varchar(30) NOT NULL)
		
		-- Tipo de pago en Efectivo
		INSERT INTO GITAR_HEROES.TipoPago
		VALUES (1, 'Efectivo')
		
		-- Tipo de pago por Tarjeta de credito
		INSERT INTO GITAR_HEROES.TipoPago
		VALUES (2, 'Tarjeta de credito')


	-- /////////////// FACTURA ///////////////

		CREATE Table GITAR_HEROES.Factura
			(numero_factura int PRIMARY KEY,
			tipo_doc_cliente smallint, 
			nro_doc_cliente numeric(11,0), 
			codigo_reserva int,
			fecha smalldatetime,
			total decimal(10,2),
			codigo_tipo_pago smallint,
			nro_tarjeta numeric(17),
			FOREIGN KEY (codigo_reserva) REFERENCES GITAR_HEROES.Estadia,
			FOREIGN KEY (codigo_tipo_pago) REFERENCES GITAR_HEROES.TipoPago)

		INSERT INTO GITAR_HEROES.Factura
		SELECT DISTINCT
			   Factura_Nro,
			   1,		-- tipo_doc_cliente correspondiente al pasaporte.
			   Cliente_Pasaporte_Nro,
			   Reserva_Codigo,
			   Factura_Fecha,
		-- Al total que aparece en la tabla maestra se le suma el monto de la estadia que no esta contemplado
			   Factura_Total + (SELECT Item_Factura_Monto FROM gd_esquema.Maestra M1 WHERE M.Factura_Nro = M1.Factura_Nro AND M1.Consumible_Codigo IS NULL),
			   1,						-- codigo_tipo_pago lo asumimos como Efectivo para todos los casos.
			   NULL						-- nro_tarjeta lo asumimos NULL para todos los casos por considerarse pagadas en efectivo.
			   
			   
		FROM gd_esquema.Maestra M
		WHERE Factura_Nro IS NOT NULL


	-- /////////////// ITEMFACTURACONSUMIBLE ///////////////

	CREATE Table GITAR_HEROES.ItemFacturaConsumible
		  (numero_factura int,
		   --numero_item smallint,
		   codigo_consumible int,
		   codigo_reserva int,
		   cantidad int,
		   monto decimal(10,2),
		   PRIMARY KEY (numero_factura, codigo_consumible),
		   FOREIGN KEY (numero_factura) REFERENCES GITAR_HEROES.Factura,
		   FOREIGN KEY (codigo_consumible, codigo_reserva) REFERENCES GITAR_HEROES.ConsumibleAdquirido)
	
	INSERT INTO GITAR_HEROES.ItemFacturaConsumible
	SELECT Factura_Nro,
		   Consumible_Codigo,
		   Reserva_Codigo,
		   SUM(Item_Factura_Cantidad),
		   Item_Factura_Monto
		  
	FROM gd_esquema.Maestra M
	WHERE Factura_Nro IS NOT NULL AND Consumible_Codigo IS NOT NULL
	GROUP BY Factura_Nro, Consumible_Codigo, Reserva_Codigo, Item_Factura_Monto
	
	-- Se inserta un consumible extra para los casos registrados en la tabla temporal #ReservasConConsumiblesNoRegistrados
	INSERT INTO GITAR_HEROES.ItemFacturaConsumible
	SELECT Factura_Nro,
		   -2,					-- codigo_consumibe
		   Reserva_Codigo,		-- codigo_reserva
		   1,					-- cantidad
		   Factura_Total		-- monto
		   
	FROM #ReservasConConsumiblesNoRegistrados
	
	-- /////////////// ITEMFACTURAESTADIA ///////////////

	CREATE Table GITAR_HEROES.ItemFacturaEstadia
		  (numero_factura int,
		   tipo_registro int,		-- Toma el valor 1 para los registros que contemplan dias en que se alojo y 0 para los que no
		   --codigo_reserva int,
		   --codigo_hotel int,
		   --numero_habitacion smallint,
		   cantidad_dias int,
		   monto decimal(10,2),
		   PRIMARY KEY (numero_factura, tipo_registro),
		   FOREIGN KEY (numero_factura) REFERENCES GITAR_HEROES.Factura)
			   
	INSERT INTO GITAR_HEROES.ItemFacturaEstadia
	SELECT Factura_Nro,
		   1,		-- tipo_registro, todos con valor 1 porque se considera que se alojaron todos los dias de la reserva
		   Estadia_Cant_Noches,		-- cantidad_dias
		   Item_Factura_Monto * Reserva_Cant_Noches
		  
	FROM gd_esquema.Maestra M
	WHERE Factura_Nro IS NOT NULL AND Consumible_Codigo IS NULL
	ORDER BY Factura_Nro


	-- /////////////// LISTADO ///////////////

	CREATE Table GITAR_HEROES.Listado
		  (codigo_listado int PRIMARY KEY,
		   descripcion varchar(320))
	
	INSERT INTO GITAR_HEROES.Listado
	VALUES (1, 'Hoteles con mayor cantidad de reservas canceladas')
	
	INSERT INTO GITAR_HEROES.Listado
	VALUES (2, 'Hoteles con mayor cantidad de consumibles facturados')
	
	INSERT INTO GITAR_HEROES.Listado
	VALUES (3, 'Hoteles con mayor cantidad de días fuera de servicio')
	
	INSERT INTO GITAR_HEROES.Listado
	VALUES (4, 'Habitaciones con mayor cantidad de días y veces que fueron ocupadas')
	
	INSERT INTO GITAR_HEROES.Listado
	VALUES (5, 'Cliente con mayor cantidad de puntos')

	-- FIN crearTablas
	END


GO



	
	
	
	
	

-- ////////////////////// ABM USUARIO //////////////////////

CREATE Procedure GITAR_HEROES.generarUsuario 
	  (@username char(15), 
	   @password char(64),
	   @nombre varchar(50),
	   @apellido varchar(50),
	   @tipo_doc smallint,
	   @nro_doc int,
	   @fecha_nacimiento smalldatetime,
	   @domicilio varchar(60),
	   @mail varchar(50),
	   @telefono bigint,
	   @codigo_rol smallint)

AS
	BEGIN
		IF EXISTS (SELECT 1 FROM GITAR_HEROES.Usuario WHERE @username = username)
			RAISERROR('ERROR, El usuario ya existe!!!',16,1)
		ELSE
			BEGIN
				
				INSERT INTO GITAR_HEROES.Usuario
				VALUES (@username,
						@password,
						@nombre,
						@apellido,
						@tipo_doc,
						@nro_doc,
						@fecha_nacimiento,
						@domicilio,
						@mail,
						@telefono,
						1,			-- estado
						1)			-- estado_sistema
				
				INSERT INTO GITAR_HEROES.RolUsuario
				VALUES (@codigo_rol, @username)
	
				PRINT('Usuario creado correctamente!!!')
						
			END
	END

GO

CREATE Procedure GITAR_HEROES.modificarUsuario 
	  (@username char(15), 
	   @password char(64),
	   @nombre varchar(50),
	   @apellido varchar(50),
	   @tipo_doc smallint,
	   @nro_doc int,
	   @fecha_nacimiento smalldatetime,
	   @domicilio varchar(60),
	   @mail varchar(50),
	   @telefono bigint,
	   @codigo_rol smallint,
	   @codigo_estado_sistema smallint)

AS
	BEGIN
		IF NOT EXISTS (SELECT 1 FROM GITAR_HEROES.Usuario WHERE @username = username)
			RAISERROR('ERROR, El usuario NO existe!!!', 16, 1)
		ELSE
			BEGIN
				
				UPDATE GITAR_HEROES.Usuario 
				SET	password = @password,
					nombre = @nombre,
					apellido = @apellido,
					tipo_doc = @tipo_doc,
					nro_doc = @nro_doc,
					fecha_nacimiento =@fecha_nacimiento,
					domicilio = @domicilio,
					mail = @mail,
					telefono = @telefono,
				 	estado_sistema = @codigo_estado_sistema
					
				WHERE username = @username
				
				UPDATE GITAR_HEROES.RolUsuario
				SET codigo_rol = @codigo_rol, username = @username
				WHERE username = @username
				
				PRINT('Usuario modificado correctamente!!!')
						
			END
	END


GO

CREATE Procedure GITAR_HEROES.limpiarUsuarioHotel(@username char(15))
AS
	BEGIN
		DELETE GITAR_HEROES.UsuarioHotel
		WHERE username = @username
	END


GO

-- Agrega a la tabla UsuarioHotel un registro vinculando al username con sus hoteles asignados
CREATE Procedure GITAR_HEROES.agregarUsuarioHotel (@codigo_hotel int, @username char(15))
AS
	BEGIN
		INSERT INTO GITAR_HEROES.UsuarioHotel
		VALUES (@codigo_hotel, @username)
	END

GO


-- ////////////////////// ABM HOTEL //////////////////////

CREATE Procedure GITAR_HEROES.cargarHotel (@nombre varchar(50), 
									 	   @domicilio_calle varchar(60), 
										   @domicilio_numero int,
										   @ciudad varchar(50),
										   @pais varchar(30),
										   @telefono bigint,
										   @cant_estrellas smallint,
										   @recarga_estrellas decimal(10,2),
										   @fecha_creacion smalldatetime,
										   @mail varchar(50),
										   @codigo_hotel int output)
AS
	BEGIN
		SELECT codigo
		INTO #hotelesAnteriores
		FROM GITAR_HEROES.Hotel
	
		INSERT INTO GITAR_HEROES.Hotel
		VALUES (@nombre, 
				@domicilio_calle, 
			    @domicilio_numero,
			    @ciudad,
			    @pais,
			    @telefono,
			    @cant_estrellas,
			    @recarga_estrellas,
			    @fecha_creacion,
			    @mail,
			    1)	-- estado (lo consideramos habilitado por defecto)
			    
		SET @codigo_hotel = (SELECT codigo FROM GITAR_HEROES.Hotel WHERE codigo NOT IN (SELECT codigo FROM #hotelesAnteriores))
	
	END

GO




-- ////////////////////// GENERAR O MODIFICAR RESERVA //////////////////////

CREATE Function GITAR_HEROES.obtenerSiguienteReserva()
RETURNS int
AS
	BEGIN
	RETURN (SELECT TOP 1 codigo + 1 FROM GITAR_HEROES.Reserva ORDER BY codigo DESC)
	END

GO

create procedure GITAR_HEROES.verificar_disponibilidad

@fechaInicioNuevaReserva
datetime,

@fechaFinNuevaReserva
datetime,

@hotelid
int,

@tipo_hab
int,

@num_hab
int output

as

begin

create
table #fechas_requeridas

(

fecha datetime

)

declare
@fechaaux datetime

select
@fechaaux = @fechaInicioNuevaReserva

--insert into #fechas_requeridas values (@fechaaux)

while
(@fechaaux <= @fechaFinNuevaReserva)

begin

--incremento en un dia

insert into #fechas_requeridas (fecha) values (@fechaaux)

select @fechaaux = dateadd(day, 1, @fechaaux)

end

-- consulta que muestra todas las habitaciones de todos los hoteteles disponibles actualmente

select
@num_hab=i.numero

from
GITAR_HEROES.Habitacion i

where
not exists (

select
1 from #fechas_requeridas f, GITAR_HEROES.ReservaHabitacion join GITAR_HEROES.Reserva

on
GITAR_HEROES.ReservaHabitacion.codigo_reserva = GITAR_HEROES.Reserva.codigo

and
GITAR_HEROES.ReservaHabitacion.codigo_hotel = GITAR_HEROES.Reserva.codigo_hotel

where

GITAR_HEROES
.Reserva.codigo_estado in (1, 2,6) --codigos de estado que indican que la habitacion esta ocupada

and
f.fecha between GITAR_HEROES.Reserva.fecha_inicio and GITAR_HEROES.Reserva.fecha_fin -- se aplica filtro de fecha

and
i.codigo_hotel = GITAR_HEROES.Reserva.codigo_hotel

and
i.numero = GITAR_HEROES.ReservaHabitacion.numero_habitacion

)

and
i.codigo_hotel = @hotelid

and
i.estado = 1

and
i.tipo = @tipo_hab

order
by i.codigo_hotel, i.piso, i.numero

drop
table #fechas_requeridas

if
@num_hab is null

begin

set @num_hab = -1

end

end

GO

-- ////////////////////// REGISTRAR ESTADIA //////////////////////

CREATE Procedure GITAR_HEROES.ingresoEgresoEstadia (@codigo_hotel int, @codigo_reserva int, @fecha smalldatetime, @username char(15))
AS
	BEGIN
		IF EXISTS (SELECT 1 FROM GITAR_HEROES.Reserva WHERE codigo = @codigo_reserva AND codigo_hotel = @codigo_hotel)
		BEGIN
			IF NOT EXISTS (SELECT 1 FROM GITAR_HEROES.Estadia WHERE codigo_reserva = @codigo_reserva)
			-- Check in de la estadia
			BEGIN
				-- Se obtiene la fecha de inicio de la reserva
				DECLARE @fecha_inicio_reserva smalldatetime
				SET @fecha_inicio_reserva = (SELECT fecha_inicio FROM GITAR_HEROES.Reserva WHERE codigo = @codigo_reserva)
				
				-- Se corrobora que la fecha de inicio para la estadia sea igual a la estipulada en la reserva
				IF (@fecha_inicio_reserva = @fecha)
				BEGIN
					INSERT INTO GITAR_HEROES.Estadia
					VALUES (@codigo_reserva, @fecha_inicio_reserva, @username, NULL, NULL)
				END
				ELSE
					RAISERROR('La fecha ingresada es distinta a la de inicio de la reserva.', 16, 1)
			END
			ELSE IF (SELECT fecha_egreso FROM GITAR_HEROES.Estadia WHERE codigo_reserva = @codigo_reserva) IS NULL
			-- Check out de la estadia
			BEGIN
				-- Se obtiene la fecha de finalizacion de la reserva
				DECLARE @fecha_fin_reserva smalldatetime
				SET @fecha_fin_reserva = (SELECT fecha_fin FROM GITAR_HEROES.Reserva WHERE codigo = @codigo_reserva)
				
				-- Se corrobora que la fecha de egreso para la estadia no supere a la estipulada en la reserva
				IF (@fecha <= @fecha_fin_reserva)	
				BEGIN		
					UPDATE GITAR_HEROES.Estadia
					SET fecha_egreso = @fecha, username_egreso = @username
					WHERE codigo_reserva = @codigo_reserva
				END
				ELSE
					RAISERROR('La fecha ingresada excede la fecha de finalizacion de la reserva.', 16, 1)
			END
			ELSE
				RAISERROR ('El check out de la estadia ya fue realizado.', 16,1)
		END
		ELSE
			RAISERROR('El codigo de reserva ingresado no existe o no corresponde al hotel logueado.', 16, 1)	
	END

GO

-- ////////////////////// REGISTRAR CONSUMIBLES //////////////////////

CREATE Procedure GITAR_HEROES.cargarConsumible (@descripcion_consumible varchar(60), @codigo_reserva int, @cantidad int)
AS
	BEGIN
		-- Se corrobora que la reserva este efectivizada
		IF @codigo_reserva IN (SELECT codigo_reserva FROM GITAR_HEROES.Estadia)
		BEGIN
			
			DECLARE @codigo_consumible int
			SET @codigo_consumible = (SELECT codigo FROM GITAR_HEROES.TipoConsumible WHERE descripción = @descripcion_consumible)
						
			-- Se buscan consumibles del mismo codigo ya ingresados
			IF EXISTS (SELECT 1 FROM GITAR_HEROES.ConsumibleAdquirido WHERE @codigo_reserva = codigo_reserva AND @codigo_consumible = codigo_consumible)
			-- Si existe se suman las cantidades
			BEGIN
				DECLARE @cantidad_anterior int
				SET @cantidad_anterior = (SELECT cantidad FROM GITAR_HEROES.ConsumibleAdquirido WHERE @codigo_reserva = codigo_reserva AND @codigo_consumible = codigo_consumible)
				
				UPDATE GITAR_HEROES.ConsumibleAdquirido
				SET cantidad = @cantidad_anterior + @cantidad
				WHERE @codigo_reserva = codigo_reserva AND @codigo_consumible = codigo_consumible
			END
			ELSE
			-- Sino se inserta un registro nuevo
			BEGIN
				INSERT INTO GITAR_HEROES.ConsumibleAdquirido
				VALUES (@codigo_consumible, @codigo_reserva, @cantidad, 'Consumible')
			END
		END
		ELSE
			RAISERROR('ERROR: La reserva NO fue efectivizada.', 16, 1)
	END
	
GO

CREATE Procedure GITAR_HEROES.modificarConsumible (@descripcion_consumible varchar(60), @codigo_reserva int, @cantidad int)
AS
	BEGIN
		-- Se corrobora que la reserva este efectivizada
		IF @codigo_reserva IN (SELECT codigo_reserva FROM GITAR_HEROES.Estadia)
		BEGIN
			DECLARE @codigo_consumible int
			SET @codigo_consumible = (SELECT codigo FROM GITAR_HEROES.TipoConsumible WHERE descripción = @descripcion_consumible)

			-- Se buscan consumibles del mismo codigo ya ingresados
			IF EXISTS (SELECT 1 FROM GITAR_HEROES.ConsumibleAdquirido WHERE @codigo_reserva = codigo_reserva AND @codigo_consumible = codigo_consumible)
			-- Si existe se modifica la cantidad
			BEGIN
				UPDATE GITAR_HEROES.ConsumibleAdquirido
				SET cantidad = @cantidad
				WHERE @codigo_reserva = codigo_reserva AND @codigo_consumible = codigo_consumible
			END
			ELSE
			-- Sino se indica que el consumible no fue cargado
				RAISERROR('ERROR: Consumible no cargado.', 16, 1)
		END
		ELSE
			RAISERROR('ERROR: La reserva NO fue efectivizada.', 16, 1)
	END	

GO

CREATE Procedure GITAR_HEROES.finalizarCargaConsumibles (@codigo_reserva int)
AS
	BEGIN
		
		DECLARE @codigo_regimen smallint
		SET @codigo_regimen = (SELECT codigo FROM GITAR_HEROES.Regimen WHERE descripcion IN ('Full Board', 'All Inclusive'))
		
		-- Revisamos el regimen aplicado a esa reserva
		IF (SELECT codigo_regimen FROM GITAR_HEROES.Reserva WHERE codigo = @codigo_reserva) = @codigo_regimen
		BEGIN
			INSERT INTO GITAR_HEROES.ConsumibleAdquirido
			VALUES (-1, @codigo_reserva, NULL, 'Fin de carga, descuento por regimen de estadia')
		END
		ELSE
		BEGIN
			INSERT INTO GITAR_HEROES.ConsumibleAdquirido
			VALUES (-1, @codigo_reserva, NULL, 'Fin de carga')
		END
	END

GO

-- ////////////////////// FACTURAR ESTADIA //////////////////////

/*
CREATE Function GITAR_HEROES.obtenerCostoBase (@codigo_reserva int)
RETURNS decimal(10,2)
AS
	BEGIN

	RETURN 0
	END
*/
	
CREATE Function GITAR_HEROES.obtenerSiguienteFactura()
RETURNS int
AS
	BEGIN
	RETURN (SELECT TOP 1 numero_factura + 1 FROM GITAR_HEROES.Factura ORDER BY numero_factura DESC)
	END

GO

CREATE Procedure GITAR_HEROES.facturar (@codigo_hotel int, @codigo_reserva int, @codigo_tipo_pago int, @nro_tarjeta numeric (17,0))

AS
	BEGIN
	-- IF no fue facturada previamente
		IF (@codigo_reserva IN (SELECT codigo_reserva FROM GITAR_HEROES.Estadia) 
			AND @codigo_hotel = (SELECT codigo_hotel FROM GITAR_HEROES.Reserva WHERE codigo = @codigo_reserva)) 
		BEGIN
			-- ///////////////////
			-- Variables para tabla Factura
			DECLARE @tipo_doc_cliente smallint,
					@nro_doc_cliente int,
					@fecha_egreso_estadia smalldatetime,
					@total decimal(10,2),
					@numero_factura int
					
			SET @tipo_doc_cliente = (SELECT tipo_doc_cliente FROM GITAR_HEROES.Reserva WHERE codigo = @codigo_reserva)
			SET @nro_doc_cliente = (SELECT nro_doc_cliente FROM GITAR_HEROES.Reserva WHERE codigo = @codigo_reserva)
			SET @fecha_egreso_estadia = (SELECT fecha_egreso FROM GITAR_HEROES.Estadia WHERE codigo_reserva = @codigo_reserva)
			SET @numero_factura = GITAR_HEROES.obtenerSiguienteFactura()
			
			-- Se carga la factura dejando vacío el campo total
			INSERT INTO GITAR_HEROES.Factura 
					   (numero_factura,
						tipo_doc_cliente, 
						nro_doc_cliente, 
						codigo_reserva, 
						fecha, codigo_tipo_pago, 
						nro_tarjeta)
					    
			VALUES (@numero_factura,
					@tipo_doc_cliente,
					@nro_doc_cliente, 
					@codigo_reserva, 
					@fecha_egreso_estadia, 
					@codigo_tipo_pago, 
					@nro_tarjeta)

			-- ///////////////////
			-- Variables para tabla ItemFacturaEstadia
			DECLARE @fecha_inicio_estadia smalldatetime,
					@fecha_fin_reserva smalldatetime,
					@monto_base decimal(10,2),
					@cant_dias_alojados int,
					@cant_dias_reservados int

			SET @fecha_inicio_estadia = (SELECT fecha_ingreso FROM GITAR_HEROES.Estadia WHERE codigo_reserva = @codigo_reserva)
			SET @fecha_fin_reserva = (SELECT fecha_fin FROM GITAR_HEROES.Reserva WHERE codigo = @codigo_reserva)
			SET @monto_base = (SELECT costo_base FROM GITAR_HEROES.Reserva WHERE codigo = @codigo_reserva)
			SET @cant_dias_alojados = DATEDIFF (DAY, @fecha_inicio_estadia, @fecha_egreso_estadia)
			SET @cant_dias_reservados = DATEDIFF (DAY, @fecha_inicio_estadia, @fecha_fin_reserva)
		
			-- Se cargan los items del tipo estadia
				-- Registro que suma (tipo 1)
			INSERT INTO GITAR_HEROES.ItemFacturaEstadia
			VALUES (@numero_factura,
					1,		-- tipo_registro
					@cant_dias_alojados,
					@monto_base)

				-- Registro que no suma ni resta, indica dias no alojados (tipo 0)
			IF (@cant_dias_reservados - @cant_dias_alojados <> 0)
			BEGIN
				INSERT INTO GITAR_HEROES.ItemFacturaEstadia
				VALUES (@numero_factura,
						0,		-- tipo_registro
						@cant_dias_reservados - @cant_dias_alojados,
						0)		-- monto
			END

			-- ///////////////////
			DECLARE @codigo_regimen smallint,
					@descripcion_regimen varchar(60)
					
			SET @codigo_regimen = (SELECT codigo_regimen FROM GITAR_HEROES.Reserva WHERE codigo = @codigo_reserva)
			SET @descripcion_regimen = (SELECT descripcion FROM GITAR_HEROES.Regimen WHERE codigo = @codigo_regimen)
			
			-- Se cargan los items del tipo consumible
				-- Se cargan los consumibles propiamente dichos
			INSERT INTO GITAR_HEROES.ItemFacturaConsumible
			SELECT @numero_factura,
				   codigo_consumible,
				   @codigo_reserva,
				   cantidad,
				   -- monto
				   CASE WHEN codigo_consumible <> -2	
						THEN (SELECT precio FROM GITAR_HEROES.TipoConsumible WHERE codigo = @codigo_reserva)
						ELSE (SELECT costo FROM GITAR_HEROES.OtroConsumibleAdquirido WHERE codigo_reserva = @codigo_reserva)
				   END
			
			FROM GITAR_HEROES.ConsumibleAdquirido
			WHERE codigo_reserva = @codigo_reserva AND codigo_consumible != -1
			
				-- Se carga el descuento si el regimen corresponde a All Inclusive
			IF @descripcion_regimen IN ('All Inclusive', 'Full Board') 
			BEGIN
				INSERT INTO GITAR_HEROES.ItemFacturaConsumible
				SELECT @numero_factura,
					   codigo_consumible,
					   @codigo_reserva,
					   1,
					   -(SELECT SUM(cantidad * monto)  FROM GITAR_HEROES.ItemFacturaConsumible WHERE ConsAdq.codigo_reserva = @codigo_reserva AND ConsAdq.codigo_consumible != -1)
				
				FROM GITAR_HEROES.ConsumibleAdquirido ConsAdq
				WHERE codigo_reserva = @codigo_reserva AND codigo_consumible = -1
			END
			
			-- ///////////////////
			-- Carga de total en la tabla factura
				-- Se crea una tabla temporal para luego sumar conceptos
			SELECT monto
			INTO #MontosItemsFactura
			FROM GITAR_HEROES.ItemFacturaEstadia
			WHERE numero_factura = @numero_factura
			
			INSERT INTO #MontosItemsFactura
			SELECT monto
			FROM GITAR_HEROES.ItemFacturaConsumible
			WHERE numero_factura = @numero_factura

			--SELECT * FROM #MontosItemsFactura
			--DROP Table #MontosItemsFactura		
			
			SET @total = (SELECT SUM(monto) FROM #MontosItemsFactura)
			
				-- Se actualiza la tabla factura
			UPDATE GITAR_HEROES.Factura
			SET total = @total
			WHERE numero_factura = @numero_factura
		END
		ELSE
		BEGIN
			RAISERROR('La reserva ingresada no existe o no fue efectivizada, o no pertenece al hotel en que se encuentra logueado.', 16, 1)
		END
	END


GO


-- ////////////////////// GENERAR LISTADO //////////////////////

--DROP Procedure GITAR_HEROES.obtenerMeses

CREATE Procedure GITAR_HEROES.obtenerMeses (@trimestre smallint, @mes1 smallint output)
AS
	BEGIN
	
		IF @trimestre = 1
		BEGIN
			SET @mes1 = 1
		END
		ELSE IF @trimestre = 2
		BEGIN
			SET @mes1 = 4
		END
		ELSE IF @trimestre = 3
		BEGIN
			SET @mes1 = 7
		END
		ELSE IF @trimestre = 4
		BEGIN
			SET @mes1 = 10
		END
		
	END

GO
--DROP Procedure GITAR_HEROES.topCancelaciones 
CREATE Procedure GITAR_HEROES.topCancelaciones 
				(@anio int,
				 @trimestre smallint)
AS
	BEGIN

		-- Variables a ser utilizadas para filtrar la busqueda
		DECLARE @mes1 smallint,
				@mes2 smallint,
				@mes3 smallint
				
		EXEC GITAR_HEROES.obtenerMeses @trimestre, @mes1 output

		SET @mes2 = @mes1 + 1
		SET @mes3 = @mes1 + 2

		SELECT TOP 5 
			   R.codigo_hotel,
			  (SELECT nombre FROM GITAR_HEROES.Hotel WHERE codigo = R.codigo_hotel) AS descripcion_hotel,
			   COUNT(R.codigo_hotel) AS cantidad
		
		INTO GITAR_HEROES.listadoEstadistico--##listadoEstadistico
		FROM GITAR_HEROES.Reserva R
		WHERE codigo_estado IN (3, 4, 5)
			  AND (SELECT YEAR(fecha_inicio) FROM GITAR_HEROES.ReservaCancelada WHERE R.codigo = codigo_reserva) = @anio
			  AND (SELECT MONTH(fecha_inicio) FROM GITAR_HEROES.ReservaCancelada WHERE R.codigo = codigo_reserva) IN (@mes1, @mes2, @mes3) 
		GROUP BY codigo_hotel
		ORDER BY 3 DESC
		
	END

GO

Create Procedure GITAR_HEROES.topConsumicionesFacturadas 
				(@anio int,
				 @trimestre smallint)
AS
	BEGIN
		
		-- Variables a ser utilizadas para filtrar la busqueda
		DECLARE @mes1 smallint,
				@mes2 smallint,
				@mes3 smallint
				
		EXEC GITAR_HEROES.obtenerMeses @trimestre, @mes1 output

		SET @mes2 = @mes1 + 1
		SET @mes3 = @mes1 + 2

		SELECT TOP 5
			   Reserva.codigo_hotel,
			  (SELECT nombre FROM GITAR_HEROES.Hotel WHERE codigo = Reserva.codigo_hotel) AS nombre_hotel,
			   SUM(Item.cantidad) AS cantidad_consumibles
		
		INTO GITAR_HEROES.listadoEstadistico--##ListadoEstadistico
		FROM GITAR_HEROES.ItemFacturaConsumible Item JOIN GITAR_HEROES.Reserva Reserva ON Reserva.codigo = Item.codigo_reserva
		WHERE (SELECT YEAR(fecha) FROM GITAR_HEROES.Factura WHERE numero_factura = Item.numero_factura) = @anio 
			  AND (SELECT MONTH(fecha) FROM GITAR_HEROES.Factura WHERE numero_factura = Item.numero_factura) IN (@mes1, @mes2, @mes3)
		GROUP BY Reserva.codigo_hotel
		ORDER BY 3 DESC
		
	END

GO

Create Procedure GITAR_HEROES.topSinServicio
				(@anio int,
				 @trimestre smallint)
AS
	BEGIN
		
		-- Variables a ser utilizadas para filtrar la busqueda
		DECLARE @mes1 smallint,
				@mes2 smallint,
				@mes3 smallint
				
		EXEC GITAR_HEROES.obtenerMeses @trimestre, @mes1 output

		SET @mes2 = @mes1 + 1
		SET @mes3 = @mes1 + 2
		
		SELECT TOP 5
			   codigo_hotel,
			  (SELECT nombre FROM GITAR_HEROES.Hotel WHERE codigo = HI.codigo_hotel) AS nombre_hotel,
			   SUM(DATEDIFF(DAY, fecha_inicio, fecha_fin)) AS cant_dias_inhabilitacion
		INTO GITAR_HEROES.listadoEstadistico--##ListadoEstadistico
		FROM GITAR_HEROES.HotelInhabilitado HI
		WHERE YEAR(fecha_inicio) = @anio
			  AND MONTH(fecha_inicio) IN (@mes1, @mes2, @mes3)
		GROUP BY codigo_hotel
		ORDER BY 3 DESC		
		
	END

GO

Create Procedure GITAR_HEROES.topHabitacionesOcupadas
				(@anio int,
				 @trimestre smallint)
AS
	BEGIN
		
		-- Variables a ser utilizadas para filtrar la busqueda
		DECLARE @mes1 smallint,
				@mes2 smallint,
				@mes3 smallint
				
		EXEC GITAR_HEROES.obtenerMeses @trimestre, @mes1 output

		SET @mes2 = @mes1 + 1
		SET @mes3 = @mes1 + 2
		
		SELECT TOP 5
			   ResHab.codigo_hotel,
			  (SELECT nombre FROM GITAR_HEROES.Hotel WHERE codigo = ResHab.codigo_hotel) AS nombre_hotel,
			   ResHab.numero_habitacion,
			   SUM(DATEDIFF(DAY, Reserva.fecha_inicio, Reserva.fecha_fin)) AS cant_dias_reservados,
			   COUNT(*) AS cant_veces
			   
		INTO GITAR_HEROES.listadoEstadistico--##ListadoEstadistico
		FROM GITAR_HEROES.ReservaHabitacion ResHab JOIN GITAR_HEROES.Reserva Reserva ON ResHab.codigo_reserva = Reserva.codigo
		WHERE YEAR(Reserva.fecha_inicio) = @anio
			  AND MONTH(Reserva.fecha_inicio) IN (@mes1, @mes2, @mes3)
		GROUP BY ResHab.codigo_hotel, ResHab.numero_habitacion
		ORDER BY 4, 5 DESC
		
	END

GO

Create Procedure GITAR_HEROES.topPuntuacionClientes
				(@anio int,
				 @trimestre smallint)
AS
	BEGIN
		
		-- Variables a ser utilizadas para filtrar la busqueda
		DECLARE @mes1 smallint,
				@mes2 smallint,
				@mes3 smallint
				
		EXEC GITAR_HEROES.obtenerMeses @trimestre, @mes1 output

		SET @mes2 = @mes1 + 1
		SET @mes3 = @mes1 + 2
		
		SELECT TOP 5
			   --apellido,
			   --nombre,
			   Factura.tipo_doc_cliente,
			   Factura.nro_doc_cliente,
			   FLOOR(SUM((ItemE.monto) / 10 + (ItemC.monto) / 5)) AS puntos
			   
		INTO GITAR_HEROES.listadoEstadistico--##ListadoEstadistico
		FROM GITAR_HEROES.Factura Factura JOIN GITAR_HEROES.ItemFacturaConsumible ItemC 
			 ON Factura.numero_factura = ItemC.numero_factura
			 JOIN GITAR_HEROES.ItemFacturaEstadia ItemE ON Factura.numero_factura = ItemE.numero_factura
			 
		WHERE YEAR(Factura.fecha) = @anio
			  AND MONTH(Factura.fecha) IN (@mes1, @mes2, @mes3)
		GROUP BY Factura.tipo_doc_cliente, Factura.nro_doc_cliente
		ORDER BY 3 DESC
		
	END

GO

CREATE Procedure GITAR_HEROES.generarListado (@anio int, @trimestre smallint, @codigo_listado int)
AS
	BEGIN
		IF (@anio <= YEAR(GETDATE()) AND @trimestre IN (1, 2, 3, 4))
		BEGIN	
			-- Dependiendo del tipo de listado se ejecuta el procedimiento correspondiente
			IF @codigo_listado = 1
				EXEC GITAR_HEROES.topCancelaciones @anio, @trimestre
			ELSE IF @codigo_listado = 2
				EXEC GITAR_HEROES.topConsumicionesFacturadas @anio, @trimestre
			ELSE IF @codigo_listado = 3
				EXEC GITAR_HEROES.topSinServicio @anio, @trimestre
			ELSE IF @codigo_listado = 4
				EXEC GITAR_HEROES.topHabitacionesOcupadas @anio, @trimestre
			ELSE IF @codigo_listado = 5
				EXEC GITAR_HEROES.topPuntuacionClientes @anio, @trimestre	
		END
		ELSE
			RAISERROR ('Año y/o trimestre ingresado inválido.', 16, 1)
	END

GO


-- ////////////////////// OTROS PROCEDIMIENTOS  Y FUNCIONES //////////////////////

-- Asigna al username todos los hoteles existentes registrados
CREATE Procedure GITAR_HEROES.setearSuperUsuario (@username char(15))
AS
	BEGIN
		DECLARE @codigo_hotel int
		
		DECLARE cHoteles CURSOR FOR
		SELECT codigo
		FROM Hotel
		
		OPEN cHoteles
		
		FETCH cHoteles INTO @codigo_hotel
		
		WHILE (@@FETCH_STATUS = 0)
		BEGIN
			EXEC GITAR_HEROES.agregarUsuarioHotel @codigo_hotel, @username
			FETCH cHoteles INTO @codigo_hotel
		END
		
		CLOSE cHoteles
		DEALLOCATE cHoteles
		
	END

GO

--DROP Function GITAR_HEROES.precioHabitacion

CREATE Function GITAR_HEROES.precioHabitacion (@codigo_regimen smallint, @codigo_hotel int, @tipo_habitacion int)
RETURNS decimal(10,2)
AS
	BEGIN
	
		DECLARE @precio_base decimal(10,2),
				@cant_estrellas smallint,
				@recarga_estrellas decimal(10,2),
				@porcentual decimal(4,2)

		SET @precio_base = (SELECT precio_base FROM GITAR_HEROES.Regimen WHERE codigo = @codigo_regimen)
		SET @cant_estrellas = (SELECT cant_estrellas FROM GITAR_HEROES.Hotel WHERE codigo = @codigo_hotel)
		SET @recarga_estrellas = (SELECT recarga_estrellas FROM GITAR_HEROES.Hotel WHERE codigo = @codigo_hotel)
		SET @porcentual = (SELECT porcentual FROM GITAR_HEROES.TipoHabitacion WHERE codigo = @tipo_habitacion)
	
	RETURN ((@precio_base * @porcentual) + (@recarga_estrellas * @cant_estrellas))
			
	END

GO

CREATE Procedure GITAR_HEROES.borrarTablas
AS
	BEGIN

	-- BORRADO DE TABLAS		
		DROP Table GITAR_HEROES.Listado
		DROP Table GITAR_HEROES.ItemFacturaEstadia
		DROP Table GITAR_HEROES.ItemFacturaConsumible
		DROP Table GITAR_HEROES.Factura
		DROP Table GITAR_HEROES.TipoPago
		DROP Table GITAR_HEROES.OtroConsumibleAdquirido
		DROP Table GITAR_HEROES.ConsumibleAdquirido
		DROP Table GITAR_HEROES.Estadia
		DROP Table GITAR_HEROES.TipoConsumible
		DROP Table GITAR_HEROES.ReservaCancelada
		DROP Table GITAR_HEROES.ReservaHabitacion
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
		DROP Procedure GITAR_HEROES.obtenerMeses
		DROP Procedure GITAR_HEROES.generarListado
		DROP Procedure GITAR_HEROES.topCancelaciones
		DROP Procedure GITAR_HEROES.topConsumicionesFacturadas
		DROP Procedure GITAR_HEROES.topSinServicio
		DROP Procedure GITAR_HEROES.topHabitacionesOcupadas
		DROP Procedure GITAR_HEROES.topPuntuacionClientes
		DROP Procedure GITAR_HEROES.setearSuperUsuario
		DROP Procedure GITAR_HEROES.facturar
		DROP Procedure GITAR_HEROES.finalizarCargaConsumibles
		DROP Procedure GITAR_HEROES.modificarConsumible
		DROP Procedure GITAR_HEROES.cargarConsumible
		DROP Procedure GITAR_HEROES.crearTablas
		DROP procedure GITAR_HEROES.verificar_disponibilidad
		DROP Procedure GITAR_HEROES.ingresoEgresoEstadia
		DROP Procedure GITAR_HEROES.cargarHotel
		DROP Procedure GITAR_HEROES.limpiarUsuarioHotel
		DROP Procedure GITAR_HEROES.agregarUsuarioHotel
		DROP Procedure GITAR_HEROES.modificarUsuario
		DROP Procedure GITAR_HEROES.generarUsuario
		DROP Procedure GITAR_HEROES.borrarTablas
		
		--DROP Table ##ListadoEstadistico

	-- BORRADO DE FUNCIONES
		DROP Function GITAR_HEROES.obtenerSiguienteReserva
		DROP Function GITAR_HEROES.obtenerSiguienteFactura
		DROP Function GITAR_HEROES.precioHabitacion
		--DROP Function GITAR_HEROES.obtenerCostoBase
	
	-- BORRADO DEL ESQUEMA		
		DROP Schema GITAR_HEROES
	
	END	

GO
-- ////////////////////  EJECUCIÓN DE PROCEDIMIENTOS ////////////////////

-- CREACION DE USUARIOS BASE
-- Se declara la variable para asignar fecha de nacimiento a usuarios
DECLARE @fecha_nac smalldatetime
SET @fecha_nac = GETDATE()

EXEC GITAR_HEROES.crearTablas

-- Se crea el usuario admnistrador base en el sistema
EXEC GITAR_HEROES.generarUsuario 
	 'admin', 
	 'e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7',
	 'Administrador General',
	 'Administrador General',
	 1,
	 11111111,
	 @fecha_nac,
	 'Domicilio no especificado',
	 'admin@gitarheroes.com',
	 11111111,
	 1

-- Se agrega acceso a todos los hoteles para el usuario "admin"
EXEC GITAR_HEROES.setearSuperUsuario 'admin'

-- Se crea el usuario guest generico en el sistema
EXEC GITAR_HEROES.generarUsuario 
	 'guest', 
	 '',
	 'Guest',
	 'Guest',
	 2,
	 22222222,
	 @fecha_nac,
	 'Domicilio no especificado',
	 'guest@gitarheroes.com',
	 22222222,
	 3
	 
	 
-- Se agrega acceso a todos los hoteles para el usuario "guest"
EXEC GITAR_HEROES.setearSuperUsuario 'guest'
	 
--EXEC GITAR_HEROES.borrarTablas