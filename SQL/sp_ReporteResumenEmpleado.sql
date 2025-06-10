
CREATE OR ALTER PROCEDURE [dbo].[sp_ReporteResumenEmpleado]
    @EmpleadoID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @FechaCorte DATE = CONVERT(DATE, CONCAT(YEAR(GETDATE()), '-12-31'));

    SELECT 
        e.Area,
        e.Nombre AS EmpleadoID,
        e.PuestoFuncional,
        e.FechaIngreso,
        @FechaCorte AS FechaCorte,
        DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) AS TiempoEnEmpresaAnios,
        e.SalarioMensual AS TotalPorPuestoMensual,

        ISNULL(SUM(CASE WHEN he.Tipo = 'Diurna' THEN he.CantidadHoras ELSE 0 END), 0) AS HorasExtrasDiurnas,
        ISNULL(SUM(CASE WHEN he.Tipo = 'Nocturna' THEN he.CantidadHoras ELSE 0 END), 0) AS HorasExtrasNocturnas,
        ISNULL(SUM(CASE WHEN he.Tipo = 'Nocturnidad' THEN he.CantidadHoras ELSE 0 END), 0) AS HorasNocturnas,

        -- Cálculos monetarios
        ROUND(e.SalarioMensual * 2 * (ISNULL(SUM(CASE WHEN he.Tipo = 'Diurna' THEN he.CantidadHoras ELSE 0 END), 0) / 30.0) / 8.0, 2) AS PagoHorasExtrasDiurnas,
        ROUND(e.SalarioMensual * 2.5 * (ISNULL(SUM(CASE WHEN he.Tipo = 'Nocturna' THEN he.CantidadHoras ELSE 0 END), 0) / 30.0) / 8.0, 2) AS PagoHorasExtrasNocturnas,
        ROUND((e.SalarioMensual / 240.0) * ISNULL(SUM(CASE WHEN he.Tipo = 'Nocturnidad' THEN he.CantidadHoras ELSE 0 END), 0) * 0.25, 2) AS PagoHorasNocturnidad,

        -- Vacaciones (días según años en la empresa, salario diario * días)
        ROUND((e.SalarioMensual / 30.0) * 
            CASE 
                WHEN DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) >= 1 AND DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) < 3 THEN 10
                WHEN DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) >= 3 AND DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) < 10 THEN 15
                WHEN DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) >= 10 THEN 20
                ELSE 0
            END, 2) AS Vacaciones,

        -- Aguinaldo (según años)
        ROUND(
            CASE 
                WHEN DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) < 1 THEN 0
                WHEN DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) >= 1 AND DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) < 3 THEN (e.SalarioMensual / 30.0) * 15
                WHEN DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) >= 3 AND DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) < 10 THEN (e.SalarioMensual / 30.0) * 19
                WHEN DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) >= 10 THEN (e.SalarioMensual / 30.0) * 21
            END, 2
        ) AS AguinaldoTotal,

        -- Aguinaldo gravado
        ROUND(
            CASE 
                WHEN 
                    CASE 
                        WHEN DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) < 1 THEN 0
                        WHEN DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) >= 1 AND DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) < 3 THEN (e.SalarioMensual / 30.0) * 15
                        WHEN DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) >= 3 AND DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) < 10 THEN (e.SalarioMensual / 30.0) * 19
                        WHEN DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) >= 10 THEN (e.SalarioMensual / 30.0) * 21
                    END > 1200 
                THEN 
                    (CASE 
                        WHEN DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) < 1 THEN 0
                        WHEN DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) >= 1 AND DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) < 3 THEN (e.SalarioMensual / 30.0) * 15
                        WHEN DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) >= 3 AND DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) < 10 THEN (e.SalarioMensual / 30.0) * 19
                        WHEN DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) >= 10 THEN (e.SalarioMensual / 30.0) * 21
                    END - 1200)
                ELSE 0
            END, 2
        ) AS AguinaldoGravado,
        
        ROUND(
            CASE 
                WHEN 
                    CASE 
                        WHEN DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) < 1 THEN 0
                        WHEN DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) >= 1 AND DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) < 3 THEN (e.SalarioMensual / 30.0) * 15
                        WHEN DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) >= 3 AND DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) < 10 THEN (e.SalarioMensual / 30.0) * 19
                        WHEN DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) >= 10 THEN (e.SalarioMensual / 30.0) * 21
                    END <= 1200 
                THEN 
                    CASE 
                        WHEN DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) < 1 THEN 0
                        WHEN DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) >= 1 AND DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) < 3 THEN (e.SalarioMensual / 30.0) * 15
                        WHEN DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) >= 3 AND DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) < 10 THEN (e.SalarioMensual / 30.0) * 19
                        WHEN DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) >= 10 THEN (e.SalarioMensual / 30.0) * 21
                    END
                ELSE 1200
            END, 2
        ) AS AguinaldoNoGravado,

        -- Monto Gravado Cotizable
        ROUND(
            (e.SalarioMensual * 2 * (ISNULL(SUM(CASE WHEN he.Tipo = 'Diurna' THEN he.CantidadHoras ELSE 0 END), 0) / 30.0) / 8.0) +
            (e.SalarioMensual * 2.5 * (ISNULL(SUM(CASE WHEN he.Tipo = 'Nocturna' THEN he.CantidadHoras ELSE 0 END), 0) / 30.0) / 8.0) +
            ((e.SalarioMensual / 240.0) * ISNULL(SUM(CASE WHEN he.Tipo = 'Nocturnidad' THEN he.CantidadHoras ELSE 0 END), 0) * 0.25) +
            (e.SalarioMensual / 30.0) * 
                CASE 
                    WHEN DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) >= 1 AND DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) < 3 THEN 10
                    WHEN DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) >= 3 AND DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) < 10 THEN 15
                    WHEN DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) >= 10 THEN 20
                    ELSE 0
                END +
            -- Aguinaldo gravado
            CASE 
                WHEN 
                    CASE 
                        WHEN DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) < 1 THEN 0
                        WHEN DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) >= 1 AND DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) < 3 THEN (e.SalarioMensual / 30.0) * 15
                        WHEN DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) >= 3 AND DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) < 10 THEN (e.SalarioMensual / 30.0) * 19
                        WHEN DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) >= 10 THEN (e.SalarioMensual / 30.0) * 21
                    END > 1200 
                THEN 
                    (CASE 
                        WHEN DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) < 1 THEN 0
                        WHEN DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) >= 1 AND DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) < 3 THEN (e.SalarioMensual / 30.0) * 15
                        WHEN DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) >= 3 AND DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) < 10 THEN (e.SalarioMensual / 30.0) * 19
                        WHEN DATEDIFF(YEAR, e.FechaIngreso, @FechaCorte) >= 10 THEN (e.SalarioMensual / 30.0) * 21
                    END - 1200)
                ELSE 0
            END
        , 2) AS MontoGravadoCotizable,

        ROUND((e.SalarioMensual * 0.075), 2) AS ISSSPatronal,
        ROUND((e.SalarioMensual * 0.03), 2) AS ISSSEmpleado,
        ROUND((e.SalarioMensual * 0.0875), 2) AS AFPPatronal,
        ROUND((e.SalarioMensual * 0.0725), 2) AS AFPEmpleado,

        -- Monto a depositar
        ROUND(e.SalarioMensual - (e.SalarioMensual * 0.03) - (e.SalarioMensual * 0.0725), 2) AS MontoEmpleado,
        ROUND(e.SalarioMensual + (e.SalarioMensual * 0.075) + (e.SalarioMensual * 0.0875), 2) AS MontoPlanilla

    FROM Empleados e
    LEFT JOIN HorasExtras he ON e.EmpleadoID = he.EmpleadoID
    WHERE (@EmpleadoID IS NULL OR e.EmpleadoID = @EmpleadoID)
    GROUP BY e.Nombre, e.Area, e.PuestoFuncional, e.FechaIngreso, e.SalarioMensual;
END;
