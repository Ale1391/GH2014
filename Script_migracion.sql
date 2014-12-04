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
			NULL,				-- En la tabla maestra no hay defenido teléfono para el cliente
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


	-- /////////////// CONSUMIBLESADQUIRIDOS ///////////////

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
		   Item_Factura_Monto
		  
	FROM gd_esquema.Maestra M
	WHERE Factura_Nro IS NOT NULL AND Consumible_Codigo IS NULL
	ORDER BY Factura_Nro

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
	   @descripcion_rol varchar(60))

AS
	BEGIN
		IF EXISTS (SELECT 1 FROM GITAR_HEROES.Usuario WHERE @username = username)
			RAISERROR('ERROR, El usuario ya existe!!!',16,1)
		ELSE
			BEGIN
				
				DECLARE @codigo_rol smallint
				SET @codigo_rol = (SELECT codigo FROM GITAR_HEROES.Rol WHERE @descripcion_rol = descripcion)
				
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

-- Agrega a la tabla UsuarioHotel un registro vinculando al username con sus hoteles asignados
CREATE Procedure GITAR_HEROES.agregarUsuarioHotel (@codigo_hotel int, @username char(15))
AS
	BEGIN
		INSERT INTO GITAR_HEROES.UsuarioHotel
		VALUES (@codigo_hotel, @username)
	END

GO

-- ////////////////////// REGISTRAR ESTADIA //////////////////////

CREATE Procedure GITAR_HEROES.ingresoEgresoEstadia (@codigo_reserva int, @username char(15))
AS
	BEGIN
	
		IF NOT EXISTS (SELECT 1 FROM GITAR_HEROES.Estadia WHERE codigo_reserva = @codigo_reserva)
		-- Check in de la estadia
		BEGIN
			INSERT INTO GITAR_HEROES.Estadia
			VALUES (@codigo_reserva, GETDATE(), @username, NULL, NULL)
		END
		ELSE
		-- Check out de la estadia
		BEGIN
			UPDATE GITAR_HEROES.Estadia
			SET fecha_egreso = GETDATE(), username_egreso = @username
			WHERE codigo_reserva = @codigo_reserva
		END
	
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

/*
CREATE Procedure GITAR_HEROES.modificarConsumible (@descripcion_consumible varchar(60), @codigo_reserva int, @cantidad int)
AS
	BEGIN

	END
*/
GO

-- ////////////////////// OTROS PROCEDIMIENTOS //////////////////////

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

CREATE Procedure GITAR_HEROES.borrarTablas
AS
	BEGIN

	-- BORRADO DE TABLAS		
		
		DROP Table GITAR_HEROES.ItemFacturaEstadia
		DROP Table GITAR_HEROES.ItemFacturaConsumible
		DROP Table GITAR_HEROES.Factura
		DROP Table GITAR_HEROES.TipoPago
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
		DROP Procedure GITAR_HEROES.modificarConsumible
		DROP Procedure GITAR_HEROES.cargarConsumible
		DROP Procedure GITAR_HEROES.agregarUsuarioHotel
		DROP Procedure GITAR_HEROES.setearSuperUsuario
		DROP Procedure GITAR_HEROES.crearTablas
		DROP Procedure GITAR_HEROES.ingresoEgresoEstadia
		DROP Procedure GITAR_HEROES.generarUsuario
		DROP Procedure GITAR_HEROES.borrarTablas

	-- BORRADO DEL ESQUEMA		
		DROP Schema GITAR_HEROES
		
	END	

GO
-- ////////////////////  EJECUCIÓN DE PROCEDIMIENTOS ////////////////////

EXEC GITAR_HEROES.crearTablas

-- Se crea el usuario admnistrador base en el sistema
EXEC GITAR_HEROES.generarUsuario 
	 'admin', 
	 'e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7',
	 'Administrador General',
	 'Administrador General',
	 1,
	 11111111,
	 NULL,
	 'Domicilio no especificado',
	 'admin@gitarheroes.com',
	 11111111,
	 'Administrador'

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
	 NULL,
	 'Domicilio no especificado',
	 'guest@gitarheroes.com',
	 22222222,
	 'Guest'
	 
	 
-- Se agrega acceso a todos los hoteles para el usuario "guest"
EXEC GITAR_HEROES.setearSuperUsuario 'guest'
	 
--EXEC GITAR_HEROES.borrarTablas