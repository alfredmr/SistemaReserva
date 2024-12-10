USE master;
GO

-- Forzar desconexión de todos los usuarios y eliminación de la base de datos si ya existe
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'SistemaReservas')
BEGIN
    -- Cerrar todas las conexiones activas a la base de datos
    DECLARE @sql NVARCHAR(MAX);
    SET @sql = N'';
    SELECT @sql += 'KILL ' + CONVERT(VARCHAR(5), session_id) + '; '
    FROM sys.dm_exec_sessions
    WHERE database_id = DB_ID('SistemaReservas');

    -- Ejecutar el comando para matar las conexiones
    EXEC sp_executesql @sql;

    -- Establece la base de datos en SINGLE_USER para forzar desconexión
    ALTER DATABASE SistemaReservas SET SINGLE_USER WITH ROLLBACK IMMEDIATE;

    -- Eliminar la base de datos
    DROP DATABASE SistemaReservas;
END;
GO

-- Crear la base de datos
CREATE DATABASE SistemaReservas;
GO

-- Usar la base de datos recién creada
USE SistemaReservas;
GO

-- Eliminar tablas si ya existen (prevención de errores al crear)
IF OBJECT_ID('Usuarios', 'U') IS NOT NULL DROP TABLE Usuarios;
IF OBJECT_ID('Clientes', 'U') IS NOT NULL DROP TABLE Clientes;
IF OBJECT_ID('Habitaciones', 'U') IS NOT NULL DROP TABLE Habitaciones;
IF OBJECT_ID('Reservas', 'U') IS NOT NULL DROP TABLE Reservas;
IF OBJECT_ID('Pagos', 'U') IS NOT NULL DROP TABLE Pagos;
IF OBJECT_ID('Logs', 'U') IS NOT NULL DROP TABLE Logs;
GO

-- Crear tablas
CREATE TABLE Usuarios (
	Idusuario INT IDENTITY(1,1) PRIMARY KEY,
	usuario_nombre NVARCHAR(100),
	usuario_apellido NVARCHAR(100),
	correo NVARCHAR(255),
	usuario NVARCHAR(50),
	clave NVARCHAR(MAX),
	rolUsuario NVARCHAR(50),
	estado NVARCHAR(20) NOT NULL DEFAULT 'Activo',
	fechaRegistro DATETIME DEFAULT GETDATE(),
);

CREATE UNIQUE INDEX idx_Usuario_Unico ON Usuarios(usuario);

CREATE TABLE Habitaciones (
	IdHabitacion INT IDENTITY(1,1) PRIMARY KEY,
	numeroHabitacion NVARCHAR(10) NOT NULL,
	tipo NVARCHAR(50) NOT NULL,
	precioPorNoche DECIMAL(10, 2) NOT NULL,
	estado NVARCHAR(20) NOT NULL DEFAULT 'Disponible',
	fechaRegistro DATETIME DEFAULT GETDATE()
);

CREATE TABLE Clientes (
	idCliente INT IDENTITY(1,1) PRIMARY KEY,
	nNumeroIdentificacion NVARCHAR(20) NOT NULL,
	nombreCompleto NVARCHAR(100) NOT NULL,
	telefono NVARCHAR(15),
	correo NVARCHAR(100),
	estatus BIT NOT NULL DEFAULT 1,
	fechaRegistro DATETIME DEFAULT GETDATE()
);

CREATE TABLE Reservas (
	IdReserva INT IDENTITY(1,1) PRIMARY KEY,
	IdCliente INT NOT NULL,
	IdHabitacion INT NOT NULL,
	idusuario INT NOT NULL,
	FechaInicio DATE NOT NULL,
	FechaFin DATE NOT NULL,
	Estado NVARCHAR(20) NOT NULL DEFAULT 'Activa',
	fechaRegistro DATETIME DEFAULT GETDATE(),
	CONSTRAINT fk_cliente FOREIGN KEY (IdCliente) REFERENCES Clientes(idCliente),
	CONSTRAINT fk_habitacion FOREIGN KEY (IdHabitacion) REFERENCES Habitaciones(IdHabitacion),
	CONSTRAINT fk_usuario FOREIGN KEY (idusuario) REFERENCES Usuarios(Idusuario),
	CONSTRAINT chk_FechasReserva CHECK (FechaInicio < FechaFin)
);

CREATE TABLE Pagos (
	IdPago INT IDENTITY(1,1) PRIMARY KEY,
	IdReserva INT NOT NULL,
	Monto DECIMAL(10, 2) NOT NULL,
	FechaPago DATE NOT NULL,
	MetodoPago NVARCHAR(50) NOT NULL,
	TipoDivisa NVARCHAR(50) NOT NULL,
	estado NVARCHAR(20) NOT NULL DEFAULT 'Activo',
	fechaRegistro DATETIME DEFAULT GETDATE(),
	CONSTRAINT fk_reserva FOREIGN KEY (IdReserva) REFERENCES Reservas(IdReserva)
);

CREATE TABLE Logs (
	IdLog INT IDENTITY(1,1) PRIMARY KEY,
	FechaHora DATETIME DEFAULT GETDATE(),
	Tipo NVARCHAR(50),
	Mensaje NVARCHAR(MAX),
	Detalle NVARCHAR(MAX) NULL
);
GO

-- Insertar registros iniciales en Clientes
-- Insertar registros iniciales
-- adminpass
-- empleado 1234
INSERT INTO Usuarios (usuario_nombre, usuario_apellido, correo, usuario, clave, rolUsuario) VALUES
('Admin', 'Sistema', 'admin@example.com', 'admin', '713bfda78870bf9d1b261f565286f85e97ee614efe5f0faf7c34e7ca4f65baca', 'Administrador'),
('Alfredo', 'Medina', '290712022@mail.utec.edu.sv', 'amedina', '713bfda78870bf9d1b261f565286f85e97ee614efe5f0faf7c34e7ca4f65baca', 'Administrador'),
('Empleado', '1', 'empleado@mail.com', 'empleado', '03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', 'Empleado');

INSERT INTO Clientes (nNumeroIdentificacion, nombreCompleto, telefono, correo) VALUES 
('12345678-1', 'Juan Pérez', '123456789', 'juan.perez@example.com'),
('12345678-2', 'Ana Gómez', '987654321', 'ana.gomez@example.com');

INSERT INTO Habitaciones (numeroHabitacion, tipo, precioPorNoche) VALUES 
('101', 'Simple', 50.00),
('102', 'Doble', 80.00),
('103', 'Suite', 150.00),
('104', 'Triple', 120.00),
('105', 'Deluxe', 200.00),
('106', 'Single Deluxe', 75.00),
('107', 'Doble Económica', 60.00),
('108', 'Suite Familiar', 180.00),
('109', 'Penthouse', 300.00),
('110', 'Simple Económica', 40.00);
GO

-- Eliminar triggers si ya existen
IF OBJECT_ID('trg_ValidarDisponibilidadHabitacion', 'TR') IS NOT NULL DROP TRIGGER trg_ValidarDisponibilidadHabitacion;
IF OBJECT_ID('trg_ActualizarEstadoHabitacion', 'TR') IS NOT NULL DROP TRIGGER trg_ActualizarEstadoHabitacion;
IF OBJECT_ID('trg_RegistrarPagoPendiente', 'TR') IS NOT NULL DROP TRIGGER trg_RegistrarPagoPendiente;
IF OBJECT_ID('trg_ValidarFechasReserva', 'TR') IS NOT NULL DROP TRIGGER trg_ValidarFechasReserva;
IF OBJECT_ID('trg_HistorialCambiosReserva', 'TR') IS NOT NULL DROP TRIGGER trg_HistorialCambiosReserva;
GO

-- Crear triggers
CREATE OR ALTER TRIGGER trg_ValidarDisponibilidadHabitacion
ON Reservas
AFTER INSERT
AS
BEGIN
    BEGIN TRY
        -- Validar si la habitación está disponible
        IF EXISTS (
            SELECT 1
            FROM Habitaciones h
            JOIN Inserted i ON h.IdHabitacion = i.IdHabitacion
            WHERE h.estado <> 'Disponible'
        )
        BEGIN
            -- Obtener detalles de la primera habitación en conflicto
            DECLARE @conflictHabitacion NVARCHAR(10);
            SELECT TOP 1 @conflictHabitacion = h.numeroHabitacion
            FROM Habitaciones h
            JOIN Inserted i ON h.IdHabitacion = i.IdHabitacion
            WHERE h.estado <> 'Disponible';

            -- Registrar el conflicto en logs
            INSERT INTO Logs (Tipo, Mensaje, Detalle)
            VALUES ('ERROR', 'Intento de reserva en habitación no disponible.',
                    CONCAT('Número de Habitación: ', @conflictHabitacion));

            -- Levantar un error para detener el flujo
            THROW 50001, 'La habitación no está disponible para esta reserva.', 1;
        END;
    END TRY
    BEGIN CATCH
        -- Validar si hay transacción activa antes de hacer rollback
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- Registrar detalles del error
        INSERT INTO Logs (Tipo, Mensaje, Detalle)
        VALUES ('ERROR', 'Error en trg_ValidarDisponibilidadHabitacion.',
                CONCAT(ERROR_MESSAGE(), ' en la línea ', ERROR_LINE()));

        -- Relanzar el error
        THROW;
    END CATCH;
END;
GO

CREATE OR ALTER TRIGGER trg_ActualizarEstadoHabitacion
ON Reservas
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    BEGIN TRY
        -- Actualizar las habitaciones a "No Disponible" si están reservadas en Reservas activas
        UPDATE Habitaciones
        SET estado = 'Ocupada'
        WHERE IdHabitacion IN (
            SELECT DISTINCT IdHabitacion
            FROM Reservas
            WHERE Estado = 'Activa'
        );

        -- Actualizar las habitaciones a "Disponible" si ya no tienen reservas activas
        UPDATE Habitaciones
        SET estado = 'Disponible'
        WHERE IdHabitacion NOT IN (
            SELECT DISTINCT IdHabitacion
            FROM Reservas
            WHERE Estado = 'Activa'
        );
    END TRY
    BEGIN CATCH
        -- Manejar errores
        INSERT INTO Logs (Tipo, Mensaje, Detalle)
        VALUES ('ERROR', 'Error en trg_ActualizarEstadoHabitacion.', 
                CONCAT(ERROR_MESSAGE(), ' en la línea ', ERROR_LINE()));
        THROW;
    END CATCH
END;
GO


CREATE OR ALTER TRIGGER trg_RegistrarPagoPendiente
ON Reservas
AFTER INSERT
AS
BEGIN
    BEGIN TRY
        INSERT INTO Logs (Tipo, Mensaje, Detalle)
        SELECT 
            'WARNING', 
            'Pago pendiente detectado.', 
            CONCAT('Reserva ID: ', i.IdReserva, ', Cliente ID: ', i.IdCliente, 
                   ', Cliente NumeroIdentificacion: ', c.nNumeroIdentificacion, 
                   ', Numero de Habitacion: ', h.numeroHabitacion)
        FROM Inserted i
        JOIN Clientes c ON i.IdCliente = c.idCliente
        JOIN Habitaciones h ON i.IdHabitacion = h.IdHabitacion
        WHERE NOT EXISTS (
            SELECT 1
            FROM Pagos p
            WHERE p.IdReserva = i.IdReserva
        );
    END TRY
    BEGIN CATCH
        INSERT INTO Logs (Tipo, Mensaje, Detalle)
        VALUES ('ERROR', 'Error en trg_RegistrarPagoPendiente.', 
                CONCAT(ERROR_MESSAGE(), ' en la línea ', ERROR_LINE()));
    END CATCH;
END;
GO

CREATE OR ALTER TRIGGER trg_ValidarFechasReserva
ON Reservas
AFTER INSERT
AS
BEGIN
    BEGIN TRY
        -- Validar conflicto de fechas en la misma habitación
        IF EXISTS (
            SELECT 1
            FROM Reservas r
            JOIN Inserted i ON r.IdHabitacion = i.IdHabitacion
            WHERE r.Estado = 'Activa'
              AND r.IdReserva <> i.IdReserva
              AND (
                  i.FechaInicio BETWEEN r.FechaInicio AND r.FechaFin OR
                  i.FechaFin BETWEEN r.FechaInicio AND r.FechaFin OR
                  (i.FechaInicio <= r.FechaInicio AND i.FechaFin >= r.FechaFin)
              )
        )
        BEGIN
            -- Levantar un error
            THROW 50002, 'Conflicto de fechas detectado.', 1;
        END;
    END TRY
    BEGIN CATCH
    -- Validar si hay transacción activa antes de hacer rollback
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;

    -- Registro detallado del error
    INSERT INTO Logs (Tipo, Mensaje, Detalle)
    VALUES ('ERROR', 'Error al insertar reserva.', 
            CONCAT(ERROR_MESSAGE(), ' en la línea ', ERROR_LINE()));

    -- Relanzar el error
    THROW;
END CATCH

END;
GO


CREATE OR ALTER TRIGGER trg_HistorialCambiosReserva
ON Reservas
AFTER UPDATE
AS
BEGIN
    BEGIN TRY
        INSERT INTO Logs (Tipo, Mensaje, Detalle)
        SELECT 
            'INFO', 
            'Reserva actualizada.',
            CONCAT('Reserva ID: ', i.IdReserva, ', Nuevo Estado: ', i.Estado, 
                   ', Cliente: ', c.nombreCompleto, ', Número de Habitación: ', h.numeroHabitacion)
        FROM Inserted i
        JOIN Clientes c ON i.IdCliente = c.idCliente
        JOIN Habitaciones h ON i.IdHabitacion = h.IdHabitacion;
    END TRY
    BEGIN CATCH
        INSERT INTO Logs (Tipo, Mensaje, Detalle)
        VALUES ('ERROR', 'Error en trg_HistorialCambiosReserva.', 
                CONCAT(ERROR_MESSAGE(), ' en la línea ', ERROR_LINE()));
    END CATCH;
END;
GO

-- Crear la vista vw_ReservasDetalle
IF OBJECT_ID('vw_ReservasDetalle', 'V') IS NOT NULL
    DROP VIEW vw_ReservasDetalle;
GO

CREATE OR ALTER VIEW vw_ReservasDetalle AS
SELECT 
    r.IdReserva,
    c.nombreCompleto AS Cliente,
    c.nNumeroIdentificacion AS IdentificacionCliente,
    h.numeroHabitacion AS NumeroHabitacion,
    h.tipo AS TipoHabitacion,
    h.precioPorNoche AS PrecioPorNoche,
    u.usuario_nombre AS UsuarioResponsable,
    r.FechaInicio,
    r.FechaFin,
    r.Estado AS EstadoReserva,
    h.estado AS EstadoHabitacion,
    CASE
        WHEN p.IdPago IS NOT NULL THEN 'Pagada'
        ELSE 'Pendiente de Pago'
    END AS EstadoPago,
    p.Monto AS MontoPagado,
    p.FechaPago,
    p.MetodoPago,
    p.TipoDivisa,
    r.fechaRegistro AS FechaReserva
FROM Reservas r
LEFT JOIN Clientes c ON r.IdCliente = c.idCliente
LEFT JOIN Habitaciones h ON r.IdHabitacion = h.IdHabitacion
LEFT JOIN Usuarios u ON r.idusuario = u.Idusuario
LEFT JOIN Pagos p ON r.IdReserva = p.IdReserva;
GO


-- Crear procedimientos almacenados

-- 1. Actualización del estado de las habitaciones
CREATE OR ALTER PROCEDURE ActualizarEstadoHabitaciones
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Crear tabla temporal
        CREATE TABLE #HabitacionesActualizadas (numeroHabitacion NVARCHAR(10));

        -- Habitaciones finalizadas vuelven a estar disponibles
        UPDATE Habitaciones
        SET estado = 'Disponible'
        OUTPUT INSERTED.numeroHabitacion INTO #HabitacionesActualizadas
        WHERE IdHabitacion IN (
            SELECT IdHabitacion
            FROM Reservas
            WHERE FechaFin < GETDATE() AND Estado = 'Activa'
        );

        -- Cambiar estado de reservas finalizadas
        UPDATE Reservas
        SET Estado = 'Finalizada'
        WHERE FechaFin < GETDATE() AND Estado = 'Activa';

        -- Registrar en Logs
        INSERT INTO Logs (Tipo, Mensaje, Detalle)
        SELECT 
            'INFO', 
            'Estado de habitaciones actualizado correctamente.',
            CONCAT('Habitaciones actualizadas: ', STRING_AGG(numeroHabitacion, ', '))
        FROM #HabitacionesActualizadas;

        DROP TABLE #HabitacionesActualizadas;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        INSERT INTO Logs (Tipo, Mensaje, Detalle)
        VALUES ('ERROR', 'Error al actualizar estado de habitaciones.', 
                CONCAT(ERROR_MESSAGE(), ' en la línea ', ERROR_LINE()));
    END CATCH;
END;
GO


-- 2. Purga de datos antiguos
CREATE OR ALTER PROCEDURE PurgarDatosAntiguos
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        DELETE FROM Pagos
        WHERE IdReserva IN (
            SELECT IdReserva
            FROM Reservas
            WHERE FechaFin < DATEADD(YEAR, -2, GETDATE())
        );

        DELETE FROM Reservas
        WHERE FechaFin < DATEADD(YEAR, -2, GETDATE());

        -- Insertar log para registrar purga
        INSERT INTO Logs (Tipo, Mensaje, Detalle)
        VALUES ('INFO', 'Datos antiguos purgados correctamente.', NULL);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        INSERT INTO Logs (Tipo, Mensaje, Detalle)
        VALUES ('ERROR', 'Error al purgar datos antiguos.', 
                CONCAT(ERROR_MESSAGE(), ' en la línea ', ERROR_LINE()));
    END CATCH;
END;
GO

-- 3. Sincronización de inventarios
CREATE OR ALTER PROCEDURE SincronizarInventario
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Crear tabla temporal
        CREATE TABLE #Sincronizadas (numeroHabitacion NVARCHAR(10));

        -- Sincronización de inventario
        UPDATE Habitaciones
        SET Estado = 'Disponible'
        OUTPUT INSERTED.numeroHabitacion INTO #Sincronizadas
        WHERE IdHabitacion NOT IN (
            SELECT IdHabitacion
            FROM Reservas
            WHERE Estado = 'Activa'
        );

        -- Insertar log para registrar la sincronización
        INSERT INTO Logs (Tipo, Mensaje, Detalle)
        SELECT 
            'INFO', 
            'Inventario sincronizado correctamente.',
            CONCAT('Habitaciones sincronizadas: ', STRING_AGG(numeroHabitacion, ', '))
        FROM #Sincronizadas;

        DROP TABLE #Sincronizadas;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        INSERT INTO Logs (Tipo, Mensaje, Detalle)
        VALUES ('ERROR', 'Error al sincronizar el inventario.', 
                CONCAT(ERROR_MESSAGE(), ' en la línea ', ERROR_LINE()));
    END CATCH;
END;
GO



--4 Crear procedimiento PurgarLogsAntiguos
CREATE OR ALTER PROCEDURE PurgarLogsAntiguos
AS
BEGIN
    BEGIN TRY
        DELETE FROM Logs
        WHERE FechaHora < DATEADD(MONTH, -1, GETDATE());

        INSERT INTO Logs (Tipo, Mensaje)
        VALUES ('INFO', 'Se purgaron registros antiguos de la tabla Logs.');
    END TRY
    BEGIN CATCH
        INSERT INTO Logs (Tipo, Mensaje, Detalle)
        VALUES ('ERROR', 'Error al purgar registros antiguos de Logs.', 
                CONCAT(ERROR_MESSAGE(), ' en la línea ', ERROR_LINE()));
    END CATCH;
END;
GO


-- 5 manejo de usuarios duplicados
CREATE OR ALTER PROCEDURE InsertarUsuario
    @usuario_nombre NVARCHAR(100),
    @usuario_apellido NVARCHAR(100),
    @correo NVARCHAR(255),
    @usuario NVARCHAR(50),
    @clave NVARCHAR(64),
    @rolUsuario NVARCHAR(50)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Insertar el nuevo usuario
        INSERT INTO Usuarios (usuario_nombre, usuario_apellido, correo, usuario, clave, rolUsuario)
        VALUES (@usuario_nombre, @usuario_apellido, @correo, @usuario, @clave, @rolUsuario);

        -- Registro en logs
        INSERT INTO Logs (Tipo, Mensaje, Detalle)
        VALUES ('INFO', 'Usuario creado exitosamente.', CONCAT('Usuario: ', @usuario));

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        -- Registro detallado del error
        INSERT INTO Logs (Tipo, Mensaje, Detalle)
        VALUES ('ERROR', 'Error al insertar usuario.', 
                CONCAT(ERROR_MESSAGE(), ' en la línea ', ERROR_LINE()));

        -- Re-levantar el error para retroalimentación
        THROW;
    END CATCH
END;
GO

-- 6 insertar reservas
CREATE OR ALTER PROCEDURE InsertarReserva
    @IdCliente INT,
    @IdHabitacion INT,
    @idusuario INT,
    @FechaInicio DATE,
    @FechaFin DATE
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Validar existencia de claves foráneas
        IF NOT EXISTS (SELECT 1 FROM Clientes WHERE idCliente = @IdCliente)
            THROW 50001, 'El cliente no existe.', 1;

        IF NOT EXISTS (SELECT 1 FROM Habitaciones WHERE IdHabitacion = @IdHabitacion AND estado = 'Disponible')
            THROW 50002, 'La habitación no está disponible.', 1;

        IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Idusuario = @idusuario)
            THROW 50003, 'El usuario no existe.', 1;

        -- Insertar la reserva
        INSERT INTO Reservas (IdCliente, IdHabitacion, idusuario, FechaInicio, FechaFin)
        VALUES (@IdCliente, @IdHabitacion, @idusuario, @FechaInicio, @FechaFin);

        -- Registro en logs
        INSERT INTO Logs (Tipo, Mensaje, Detalle)
        VALUES ('INFO', 'Reserva creada exitosamente.', 
                CONCAT('Cliente: ', @IdCliente, ', Habitación: ', @IdHabitacion, ', Por: ', @idusuario));

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        -- Registro detallado del error
        INSERT INTO Logs (Tipo, Mensaje, Detalle)
        VALUES ('ERROR', 'Error al insertar reserva.', 
                CONCAT(ERROR_MESSAGE(), ' en la línea ', ERROR_LINE()));

        -- Re-levantar el error para retroalimentación
        THROW;
    END CATCH
END;
GO

--7 insertar pago
CREATE OR ALTER PROCEDURE InsertarPago
    @IdReserva INT,
    @Monto DECIMAL(10, 2),
    @FechaPago DATE,
    @MetodoPago NVARCHAR(50),
    @TipoDivisa NVARCHAR(50)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Validar existencia de la reserva
        IF NOT EXISTS (SELECT 1 FROM Reservas WHERE IdReserva = @IdReserva AND Estado = 'Activa')
            THROW 50001, 'La reserva no existe o no está activa.', 1;

        -- Insertar el pago
        INSERT INTO Pagos (IdReserva, Monto, FechaPago, MetodoPago, TipoDivisa)
        VALUES (@IdReserva, @Monto, @FechaPago, @MetodoPago, @TipoDivisa);

        -- Registro en logs
        INSERT INTO Logs (Tipo, Mensaje, Detalle)
        VALUES ('INFO', 'Pago registrado exitosamente.', 
                CONCAT('Reserva: ', @IdReserva, ', Monto: ', @Monto));

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        -- Registro detallado del error
        INSERT INTO Logs (Tipo, Mensaje, Detalle)
        VALUES ('ERROR', 'Error al insertar pago.', 
                CONCAT(ERROR_MESSAGE(), ' en la línea ', ERROR_LINE()));

        -- Re-levantar el error para retroalimentación
        THROW;
    END CATCH
END;
GO

-- 8 insertar cliente

CREATE OR ALTER PROCEDURE InsertarCliente
    @nNumeroIdentificacion NVARCHAR(20),
    @nombreCompleto NVARCHAR(100),
    @telefono NVARCHAR(15),
    @correo NVARCHAR(100)
AS
BEGIN
    INSERT INTO Clientes (nNumeroIdentificacion, nombreCompleto, telefono, correo)
    VALUES (@nNumeroIdentificacion, @nombreCompleto, @telefono, @correo);
END;
GO

--9 actualización usuario

CREATE OR ALTER PROCEDURE ActualizarUsuario
    @Idusuario INT,
    @usuario_nombre NVARCHAR(100),
    @usuario_apellido NVARCHAR(100),
    @correo NVARCHAR(255),
    @usuario NVARCHAR(50),
    @clave NVARCHAR(64),
    @rolUsuario NVARCHAR(50)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Validar si el usuario existe
        IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Idusuario = @Idusuario)
        BEGIN
            -- Insertar en logs que el usuario no existe
            INSERT INTO Logs (Tipo, Mensaje, Detalle)
            VALUES ('ERROR', 'Intento de actualización de un usuario inexistente.', 
                    CONCAT('ID Usuario: ', @Idusuario));
            
            -- Lanzar un error
            THROW 50001, 'El usuario no existe.', 1;
        END;

        -- Validar si el nombre de usuario ya está en uso por otro usuario
        IF EXISTS (
            SELECT 1
            FROM Usuarios
            WHERE usuario = @usuario AND Idusuario <> @Idusuario
        )
        BEGIN
            -- Insertar en logs que el nombre de usuario está en uso
            INSERT INTO Logs (Tipo, Mensaje, Detalle)
            VALUES ('ERROR', 'Nombre de usuario ya está en uso.', 
                    CONCAT('Nombre de usuario: ', @usuario, ' ya está asociado a otro usuario.'));

            -- Lanzar un error
            THROW 50002, 'El nombre de usuario ya está en uso.', 1;
        END;

        -- Actualizar los datos del usuario
        UPDATE Usuarios
        SET usuario_nombre = @usuario_nombre,
            usuario_apellido = @usuario_apellido,
            correo = @correo,
            clave = @clave,
            rolUsuario = @rolUsuario
        WHERE Idusuario = @Idusuario;

        -- Insertar en logs el éxito de la actualización
        INSERT INTO Logs (Tipo, Mensaje, Detalle)
        VALUES ('INFO', 'Usuario actualizado exitosamente.', 
                CONCAT('ID Usuario: ', @Idusuario, ', Nuevo usuario: ', @usuario));

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Realizar rollback en caso de error
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- Insertar en logs el error ocurrido
        INSERT INTO Logs (Tipo, Mensaje, Detalle)
        VALUES ('ERROR', 'Error al actualizar usuario.', 
                CONCAT(ERROR_MESSAGE(), ' en la línea ', ERROR_LINE()));

        -- Relanzar el error
        THROW;
    END CATCH
END;
GO

-- 10 eliminar usuario

CREATE OR ALTER PROCEDURE EliminarUsuario
    @Idusuario INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Validar si el usuario existe
        IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Idusuario = @Idusuario)
        BEGIN
            THROW 50001, 'El usuario no existe.', 1;
        END

        -- Eliminar el usuario
        DELETE FROM Usuarios
        WHERE Idusuario = @Idusuario;

        -- Insertar en logs el éxito de la eliminación
        INSERT INTO Logs (Tipo, Mensaje, Detalle)
        VALUES ('INFO', 'Usuario eliminado exitosamente.', 
                CONCAT('ID Usuario: ', @Idusuario));

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Realizar rollback en caso de error
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- Insertar en logs el error ocurrido
        INSERT INTO Logs (Tipo, Mensaje, Detalle)
        VALUES ('ERROR', 'Error al eliminar usuario.', 
                CONCAT(ERROR_MESSAGE(), ' en la línea ', ERROR_LINE()));

        -- Relanzar el error
        THROW;
    END CATCH
END;

GO

-- 11 bloquear usuario
CREATE OR ALTER PROCEDURE EliminarUsuario
    @Idusuario INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Validar si el usuario existe
        IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Idusuario = @Idusuario)
        BEGIN
            THROW 50001, 'El usuario no existe.', 1;
        END

        -- Eliminar el usuario
        DELETE FROM Usuarios
        WHERE Idusuario = @Idusuario;

        -- Insertar en logs el éxito de la eliminación
        INSERT INTO Logs (Tipo, Mensaje, Detalle)
        VALUES ('INFO', 'Usuario eliminado exitosamente.', CONCAT('ID Usuario: ', @Idusuario));

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Realizar rollback en caso de error
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- Insertar en logs el error ocurrido
        INSERT INTO Logs (Tipo, Mensaje, Detalle)
        VALUES ('ERROR', 'Error al eliminar usuario.', CONCAT(ERROR_MESSAGE(), ' en la línea ', ERROR_LINE()));

        -- Relanzar el error
        THROW;
    END CATCH
END;
GO

-- Obtener lista de DUI de clientes
CREATE OR ALTER PROCEDURE ObtenerListaDuiClientes
AS
BEGIN
    BEGIN TRY
        -- Realizar la consulta
        SELECT * FROM Clientes;

        -- Insertar en logs la acción realizada
        INSERT INTO Logs (Tipo, Mensaje, Detalle)
        VALUES ('INFO', 'Consulta de lista de DUIs de clientes realizada exitosamente.', NULL);
    END TRY
    BEGIN CATCH
        -- Manejar errores y registrar en logs
        INSERT INTO Logs (Tipo, Mensaje, Detalle)
        VALUES ('ERROR', 'Error al consultar lista de DUIs de clientes.', 
                CONCAT(ERROR_MESSAGE(), ' en la línea ', ERROR_LINE()));

        -- Relanzar el error
        THROW;
    END CATCH
END;
GO

-- Obtener número de habitaciones
CREATE OR ALTER PROCEDURE ObtenerNumeroHabitaciones
AS
BEGIN
    BEGIN TRY
        -- Realizar la consulta
        SELECT * FROM Habitaciones;

        -- Insertar en logs la acción realizada
        INSERT INTO Logs (Tipo, Mensaje, Detalle)
        VALUES ('INFO', 'Consulta de número de habitaciones realizada exitosamente.', NULL);
    END TRY
    BEGIN CATCH
        -- Manejar errores y registrar en logs
        INSERT INTO Logs (Tipo, Mensaje, Detalle)
        VALUES ('ERROR', 'Error al consultar número de habitaciones.', 
                CONCAT(ERROR_MESSAGE(), ' en la línea ', ERROR_LINE()));

        -- Relanzar el error
        THROW;
    END CATCH
END;
GO

-- Eliminar reserva
CREATE OR ALTER PROCEDURE EliminarReserva
    @IdReserva INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Verificar si existe el registro antes de eliminarlo
        IF EXISTS (SELECT 1 FROM Reservas WHERE IdReserva = @IdReserva)
        BEGIN
            -- Eliminar el pago relacionado
            DELETE FROM Pagos WHERE IdReserva = @IdReserva;

            -- Eliminar la reserva
            DELETE FROM Reservas WHERE IdReserva = @IdReserva;

            -- Insertar en logs la acción realizada
            INSERT INTO Logs (Tipo, Mensaje, Detalle)
            VALUES ('INFO', 'Reserva eliminada exitosamente.', CONCAT('ID Reserva: ', @IdReserva));

            PRINT 'El registro ha sido eliminado correctamente.';
        END
        ELSE
        BEGIN
            -- Insertar en logs el intento fallido
            INSERT INTO Logs (Tipo, Mensaje, Detalle)
            VALUES ('WARNING', 'Intento de eliminar una reserva inexistente.', CONCAT('ID Reserva: ', @IdReserva));

            PRINT 'El registro no existe.';
        END
    END TRY
    BEGIN CATCH
        -- Manejar errores y registrar en logs
        INSERT INTO Logs (Tipo, Mensaje, Detalle)
        VALUES ('ERROR', 'Error al intentar eliminar reserva.', 
                CONCAT(ERROR_MESSAGE(), ' en la línea ', ERROR_LINE()));

        -- Relanzar el error
        THROW;
    END CATCH
END;
GO

--modificar reserva

CREATE OR ALTER PROCEDURE ModificarReserva
    @IdReserva INT,
    @IdCliente INT,
    @IdHabitacion INT,
    @idusuario INT,
    @FechaInicio DATE,
    @FechaFin DATE
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Validar si la reserva existe
        IF NOT EXISTS (SELECT 1 FROM Reservas WHERE IdReserva = @IdReserva)
            THROW 50001, 'La reserva no existe.', 1;

        -- Validar si el cliente existe
        IF NOT EXISTS (SELECT 1 FROM Clientes WHERE idCliente = @IdCliente)
            THROW 50002, 'El cliente no existe.', 1;

        -- Validar si la habitación está disponible para las fechas proporcionadas
        IF EXISTS (
            SELECT 1
            FROM Reservas
            WHERE IdHabitacion = @IdHabitacion
              AND IdReserva <> @IdReserva -- Excluir la misma reserva
              AND Estado = 'Activa'
              AND (
                  @FechaInicio BETWEEN FechaInicio AND FechaFin OR
                  @FechaFin BETWEEN FechaInicio AND FechaFin OR
                  (FechaInicio <= @FechaInicio AND FechaFin >= @FechaFin)
              )
        )
            THROW 50003, 'La habitación no está disponible para las fechas seleccionadas.', 1;

        -- Actualizar la reserva
        UPDATE Reservas
        SET IdCliente = @IdCliente,
            IdHabitacion = @IdHabitacion,
            idusuario = @idusuario,
            FechaInicio = @FechaInicio,
            FechaFin = @FechaFin,
            fechaRegistro = GETDATE()
        WHERE IdReserva = @IdReserva;

        -- Registrar en logs el éxito de la modificación
        INSERT INTO Logs (Tipo, Mensaje, Detalle)
        VALUES ('INFO', 'Reserva modificada exitosamente.', 
                CONCAT('ID Reserva: ', @IdReserva, ', Cliente: ', @IdCliente, 
                       ', Habitación: ', @IdHabitacion, ', Fechas: ', 
                       @FechaInicio, ' a ', @FechaFin));

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- Registrar error en logs
        INSERT INTO Logs (Tipo, Mensaje, Detalle)
        VALUES ('ERROR', 'Error al modificar reserva.', 
                CONCAT(ERROR_MESSAGE(), ' en la línea ', ERROR_LINE()));

        -- Relanzar el error
        THROW;
    END CATCH
END;
GO





-- Eliminar el job si ya existe
USE msdb;
GO

IF EXISTS (SELECT job_id FROM msdb.dbo.sysjobs WHERE name = 'LiberarHabitacionesCada5Minutos')
BEGIN
    EXEC msdb.dbo.sp_delete_job @job_name = 'LiberarHabitacionesCada5Minutos';
END;
GO

-- Crear el Job para ejecutar el procedimiento de Actualización de Estado de Habitaciones
EXEC dbo.sp_add_job 
    @job_name = 'LiberarHabitacionesCada5Minutos';
GO

EXEC sp_add_jobstep
    @job_name = 'LiberarHabitacionesCada5Minutos',
    @step_name = 'Actualizar Estado de Habitaciones',
    @subsystem = 'TSQL',
    @command = 'EXEC SistemaReservas.dbo.ActualizarEstadoHabitaciones;',
    @retry_attempts = 5,
    @retry_interval = 5;
GO

EXEC sp_add_schedule 
    @schedule_name = 'Cada5Minutos',
    @freq_type = 4,  -- Diario
    @freq_interval = 1,  -- Cada 1 día
    @freq_subday_type = 4,  -- Minutos
    @freq_subday_interval = 5;  -- Cada 5 minutos
GO

EXEC sp_attach_schedule 
   @job_name = 'LiberarHabitacionesCada5Minutos', 
   @schedule_name = 'Cada5Minutos';
GO

EXEC sp_add_jobserver 
    @job_name = 'LiberarHabitacionesCada5Minutos';
GO


-- Crear Job para purga de Logs
USE msdb;
GO

IF EXISTS (SELECT job_id FROM msdb.dbo.sysjobs WHERE name = 'PurgarLogsMensual')
BEGIN
    EXEC msdb.dbo.sp_delete_job @job_name = 'PurgarLogsMensual';
END;
GO

EXEC dbo.sp_add_job 
    @job_name = 'PurgarLogsMensual';
GO

EXEC sp_add_jobstep
    @job_name = 'PurgarLogsMensual',
    @step_name = 'Eliminar logs antiguos',
    @subsystem = 'TSQL',
    @command = 'EXEC SistemaReservas.dbo.PurgarLogsAntiguos;';
GO

EXEC sp_add_schedule 
    @schedule_name = 'Mensual',
    @freq_type = 1,  -- Por mes
    @freq_interval = 1,
    @active_start_time = 000000;
GO

EXEC sp_attach_schedule 
   @job_name = 'PurgarLogsMensual', 
   @schedule_name = 'Mensual';
GO

EXEC sp_add_jobserver 
    @job_name = 'PurgarLogsMensual';
GO