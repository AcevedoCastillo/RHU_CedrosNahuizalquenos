INSERT INTO Empleados (Area, PuestoFuncional, Nombre, Edad, FechaIngreso, SalarioMensual)
VALUES 
('Recursos Humanos', 'Analista de RRHH', 'Carla Gómez', 32, '2021-03-15', 850.00),
('Producción', 'Operario', 'Luis Martínez', 28, '2020-06-01', 700.00),
('Administración', 'Contador', 'Ana Ramírez', 40, '2019-11-10', 1200.00),
('Logística', 'Supervisor de Envíos', 'Carlos Méndez', 35, '2022-01-20', 950.00),
('IT', 'Soporte Técnico', 'Javier López', 29, '2023-05-05', 880.00);

INSERT INTO HorasExtras (EmpleadoID, Fecha, Tipo, CantidadHoras)
VALUES 
(1, '2024-06-01', 'Diurna', 2),
(1, '2024-06-03', 'Nocturna', 3),
(2, '2024-06-02', 'Nocturnidad', 1.5),
(3, '2024-06-04', 'Diurna', 4),
(4, '2024-06-05', 'Nocturna', 2);

INSERT INTO Incidencias (EmpleadoID, FechaInicio, FechaFin, Tipo, Comentario)
VALUES 
(1, '2024-05-10', '2024-05-14', 'Vacaciones', 'Vacaciones programadas'),
(2, '2024-06-01', '2024-06-03', 'Incapacidad', 'Reposo por enfermedad'),
(3, '2024-06-05', '2024-06-05', 'Ausencia', 'Falta sin justificar'),
(4, '2024-05-20', '2024-05-27', 'Vacaciones', 'Vacaciones de verano'),
(5, '2024-06-06', '2024-06-08', 'Incapacidad', 'Accidente leve');
