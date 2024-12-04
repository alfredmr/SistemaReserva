-- 1. Insertar Usuarios
PRINT 'Insertar Usuarios';
EXEC InsertarUsuario 'Pedro', 'Martínez', 'pedro.martinez@example.com', 'pedrom', 'password1', 'Administrador';
EXEC InsertarUsuario 'Luis', 'Gómez', 'luis.gomez@example.com', 'luisg', 'password2', 'Recepcionista';
EXEC InsertarUsuario 'María', 'Lopez', 'maria.lopez@example.com', 'marial', 'password3', 'Recepcionista';

-- Intentar insertar usuario duplicado
PRINT 'Intentar insertar usuario duplicado';
BEGIN TRY
    EXEC InsertarUsuario 'Pedro', 'Martínez', 'pedro.martinez@example.com', 'pedrom', 'password1', 'Administrador';
END TRY
BEGIN CATCH
    PRINT 'Error al insertar usuario duplicado: ' + ERROR_MESSAGE();
END CATCH;

-- 2. Insertar Clientes
PRINT 'Insertar Clientes';
EXEC InsertarCliente '98765432-1', 'Carlos Pérez', '555678901', 'carlos.perez@example.com';
EXEC InsertarCliente '98765432-2', 'Luisa Fernández', '555876543', 'luisa.fernandez@example.com';

-- 3. Insertar Habitaciones
PRINT 'Insertar Habitaciones';
INSERT INTO Habitaciones (numeroHabitacion, tipo, precioPorNoche) VALUES ('201', 'Suite', 250.00), ('202', 'Simple', 75.00);

-- 4. Insertar Reservas
PRINT 'Insertar Reservas Válidas';
EXEC InsertarReserva 1, 1, 1, '2024-12-01', '2024-12-10';
EXEC InsertarReserva 2, 2, 1, '2024-12-05', '2024-12-12';

-- Intentar insertar reserva con habitación ocupada
PRINT 'Intentar insertar reserva con habitación ocupada';
BEGIN TRY
    EXEC InsertarReserva 2, 1, 1, '2024-12-05', '2024-12-15';
END TRY
BEGIN CATCH
    PRINT 'Error al insertar reserva conflictiva: ' + ERROR_MESSAGE();
END CATCH;

-- Intentar insertar reserva con fechas inválidas
PRINT 'Intentar insertar reserva con fechas inválidas';
BEGIN TRY
    EXEC InsertarReserva 2, 2, 1, '2024-12-15', '2024-12-10';
END TRY
BEGIN CATCH
    PRINT 'Error al insertar reserva con fechas inválidas: ' + ERROR_MESSAGE();
END CATCH;

-- 5. Insertar Pagos
PRINT 'Insertar Pagos';
EXEC InsertarPago 1, 750.00, '2024-12-02', 'Tarjeta de Crédito', 'USD';

-- Intentar insertar pago para reserva inexistente
PRINT 'Intentar insertar pago para reserva inexistente';
BEGIN TRY
    EXEC InsertarPago 99, 100.00, '2024-12-02', 'Tarjeta de Débito', 'EUR';
END TRY
BEGIN CATCH
    PRINT 'Error al insertar pago: ' + ERROR_MESSAGE();
END CATCH;

-- 6. Procedimientos de Actualización y Mantenimiento
PRINT 'Actualizar Estado de Habitaciones';
EXEC ActualizarEstadoHabitaciones;

PRINT 'Sincronizar Inventario';
EXEC SincronizarInventario;

PRINT 'Purgar Datos Antiguos';
EXEC PurgarDatosAntiguos;

PRINT 'Purgar Logs Antiguos';
EXEC PurgarLogsAntiguos;

-- 7. Consultar Vista vw_ReservasDetalle
PRINT 'Consultar Vista vw_ReservasDetalle';
SELECT * FROM vw_ReservasDetalle;

-- 8. Consultar Logs para Validar Actividad
PRINT 'Consultar Logs';
SELECT * FROM Logs;
