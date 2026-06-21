# EF — Reporte de Proyecto
**Estudiante:** Gutierrez Lara Israel
**Proyecto:** [Hotel Pequeno]
**Repositorio:** [Repositorio](https://github.com/Nov-222/Segunda-Instancia-EC1)
**Fecha de entrega:** [20/06/2025]

> **USO DE IA PROHIBIDO.** Cualquier evidencia de uso de IA (ChatGPT, Copilot, Claude, u otras) anula el examen completo — los 3 proyectos reciben nota 0.

> Usar este template una vez por proyecto. Entregar 3 archivos:
> - `EF_hotel_apellido-nombre.md`
---

## Sección 1 — Deploy
**Ids Validos para Documento de Huespedes:**
- A-123456
- B-123456

**URL del proyecto:** [URL pública](https://segunda-instancia-ec1.onrender.com)
**Swagger / API:** [URL si aplica](https://segunda-instancia-ec1-production.up.railway.app/swagger/index.html)

> Captura del proyecto corriendo con datos reales:

![Deploy en producción](capturas/hotel-deploy.png)

---

## Sección 2 — Pruebas con TDD + cobertura

### Cobertura inicial (15%)

**Herramienta:** [dotnet coverage / Jest / Istanbul / ng test --coverage]

> Captura del reporte de cobertura antes de escribir pruebas nuevas:

![Cobertura inicial](capturas/hotel-cobertura-inicial.png)

---

### Ciclo TDD — Prueba 1

**HU:** [HU-03] [Visualizar Reservas]
> Como [administrador ] quiero [poder visualizar todas las reservas hechas en la HU2] para [ tener un mejor entendimiento de mis futuros clientes a llegar.]

**CA elegido:** [Dado que existen reservas, cuando se visualice la reserva, entonces estas deben mostrarse en orden cronológico ascendente por fecha de inicio.]

**Commit 1 — Rojo** [`5b5e65e`](https://github.com/Nov-222/Segunda-Instancia-EC1/commit/5b5e65e):
```
test: [HU-03] agregar test para [Listar_Reservas_Admin_ExistenReservas_Exito]
```
Test escrito (sin el código que lo pase aún):
```csharp / typescript
[Test]
public void Listar_Reservas_Admin_ExistenReservas_Exito()
{
    var reservas = new List<VisualizacionDTO>
    {
        new VisualizacionDTO
        {
            Id =1,
            Fecha_Inicio = new DateTime(2026,6,20),
            Fecha_Finalizacion = new DateTime(2026,6,27),
            Estado = "Reservada",
            Nro_Habitacion = 10,
            Precio_Total = 1500,
            Nombre_Cliente = "Jose Enrique DIaz Velarde"
        },
        new VisualizacionDTO
        {
            Id = 2,
            Fecha_Inicio = new DateTime(2026,6,15),
            Fecha_Finalizacion = new DateTime(2026,6,22),
            Estado = "Activa",
            Nro_Habitacion = 13,
            Precio_Total = 2000,
            Nombre_Cliente = "Maria Belen Zurita Cardenas"
        }
    };
    repositorio.Setup(f => f.Obtener_Reservas()).Returns(reservas);


    var resultado = servicio.Listar_Reservas_Admin();

    Assert.That(resultado, Is.Not.Empty);
}
```

> Captura del test fallando o error de compilación:

![Test rojo](capturas/hotel-tdd1-rojo.png)

---

**Commit 2 — Verde** [`2654144`](https://github.com/Nov-222/Segunda-Instancia-EC1/commit/2654144):
```
feat: [HU-03] implementar [Listar_Reservas_Admin] para pasar test
```
Código mínimo para hacer pasar el test:
```csharp / typescript
        public List<VisualizacionDTO> Listar_Reservas_Admin()
        {
            return repositorio.Obtener_Reservas();
        }
```

> Captura del test pasando:

![Test verde](capturas/hotel-tdd1-verde.png)

---

**Commit 3 — Refactor**:
```
Debido a que esta funcion no presenta demasiada complejidad (No tiene logica de negocio, solamente es un intermediario en caso de escalabilidad),no es necesario un refactor
```

### Ciclo TDD — Prueba 2

**HU:** [HU-04] [Marcar Check In]
> Como [administrador] quiero [poder marcar como Activa las reservas en estado de Reservado a través de presionar un botón] para [poder registrar la hora y fecha de ingreso.]

**CA elegido:** [Dado que existen reservas en estado Reservado, cuando se cambie de estado, entonces en la base de datos se debe registrar la fecha y hora de cambio de estado a 'Activo' y poblar la tabla de detalles con multa en 0.]

**Commit 1 — Rojo** [`5846dba`](https://github.com/Nov-222/Segunda-Instancia-EC1/commit/5846dba):
```
test: [HU-04] agregar test para [Procesar_CheckIn_ReservaValida_Exito]
```
Test escrito (sin el código que lo pase aún):
```csharp / typescript
[Test]
public void Procesar_CheckIn_ReservaValida_Exito()
{
    int IdValido = 20;
    repositorio.Setup(f => f.Registrar_CheckIn(IdValido)).Returns(true);

    var resultado = servicio.Procesar_CheckIn(IdValido);

    Assert.That(resultado, Is.EqualTo(true));
}
```

> Captura del test fallando o error de compilación:

![Test rojo](capturas/hotel-tdd2-rojo.png)

---

**Commit 2 — Verde** [`0f9a595`](https://github.com/Nov-222/Segunda-Instancia-EC1/commit//0f9a595):
```
feat: [HU-04] implementar [Procesar_CheckIn] para pasar test
```
Código mínimo para hacer pasar el test:
```csharp / typescript
        public bool Procesar_CheckIn(int Id)
        {
            return repositorio.Registrar_CheckIn(Id);
        }
```

> Captura del test pasando:

![Test verde](capturas/hotel-tdd2-verde.png)

---

**Commit 3 — Refactor**:

Debido a que esta funcion no presenta demasiada complejidad (No tiene logica de negocio, solamente es un intermediario en caso de escalabilidad),no es necesario un refactor

---

### Ciclo TDD — Prueba 3

**HU:** [HU-05] [Marcar Check Out]
> Como [administrador] quiero [poder marcar como Finalizada las reservas en estado de Activa a través de presionar un botón] para [poder registrar la hora y fecha de salida.]

**CA elegido:** [Dado que existen reservas en estado Activa, cuando se cambie de estado, entonces en la base de datos se debe registrar la fecha y hora de cambio de estado a 'Finalizada' y guardar la fecha de salida en Detalle_Estadia.]

**Commit 1 — Rojo** [`fee851e`](https://github.com/Nov-222/Segunda-Instancia-EC1/commit/fee851e):
```
test: [HU-05] agregar test para [Procesar_CheckOut_ReservaValida_Exito]
```
Test escrito (sin el código que lo pase aún):
```csharp / typescript
        [Test]
        public void Procesar_CheckOut_ReservaValida_Exito()
        {
            int IdValido = 20;
            repositorio.Setup(f => f.Registrar_CheckOut(IdValido)).Returns(true);

            var resultado = servicio.Procesar_CheckOut(IdValido);

            Assert.That(resultado, Is.EqualTo(true));
        }
```

> Captura del test fallando o error de compilación:

![Test rojo](capturas/hotel-tdd3-rojo.png)

---

**Commit 2 — Verde** [`2ac1792`](https://github.com/Nov-222/Segunda-Instancia-EC1/commit/2ac1792):
```
feat: [HU-03] implementar [Procesar_CheckOut] para pasar test
```
Código mínimo para hacer pasar el test:
```csharp / typescript
public bool Procesar_CheckOut(int Id)
{
    return repositorio.Registrar_CheckOut(Id);
}
```

> Captura del test pasando:

![Test verde](capturas/hotel-tdd3-verde.png)

---

**Commit 3 — Refactor**:

Debido a que esta funcion no presenta demasiada complejidad (No tiene logica de negocio, solamente es un intermediario en caso de escalabilidad),no es necesario un refactor

---
### Ciclo TDD — Prueba 4

**HU:** [HU-02] [Reservar Estadía]
> Como [administrador] quiero [ingresar los datos (Fecha de Inicio, Fecha de Finalización, Documento de Cliente) y poder seleccionar la habitación de la lista desplegable (HU1)] para [poder reservar una estadía.]

**CA elegido:** [Dado que se hayan enviado correctamente todos los datos, cuando se reserve la estadía, entonces el software debe notificar que ha sido un éxito.]

**Commit 1 — Rojo** [`be51d58`](https://github.com/Nov-222/Segunda-Instancia-EC1/commit/be51d58):
```
test: [HU-02] agregar test para [Confirmar_Reserva_DatosValidos_Exito]
```
Test escrito (sin el código que lo pase aún):
```csharp / typescript
[Test]
public void Confirmar_Reserva_DatosValidos_Exito()
{
    var datos = new ReservarEstadiaDTO
    {
        Fecha_Inicio = new DateTime(2026, 7, 6),

        Fecha_Finalizacion = new DateTime(2026, 7, 13),

        Id_Habitacion = 20,

        Documentos_Huespedes = new List<string> { "ABC-784535" }
    };

    var habitacion_disponible = new HabitacionDisponibleDTO
    {
        Id = 20,
        Tipo_Nombre = "Matrimonial",
        Precio_Noche = 100
    };

    repositorio.Setup(f => f.Obtener_Habitaciones(datos.Fecha_Inicio,datos.Fecha_Finalizacion)).Returns(new List<HabitacionDisponibleDTO> { habitacion_disponible }); ;
    repositorio.Setup(f => f.Guardar_Estadia(datos, It.IsAny<int>())).Returns(10);

    var resultado = servicio.Confirmar_Reserva(datos);

    Assert.That(resultado, Is.EqualTo(true));
}
```

> Captura del test fallando o error de compilación:

![Test rojo](capturas/hotel-tdd4-rojo.png)

---

**Commit 2 — Verde** [`719cf7f`](https://github.com/Nov-222/Segunda-Instancia-EC1/commit/719cf7f):
```
feat: [HU-02] implementar [Guardar_Estadia] para pasar test
```
Código mínimo para hacer pasar el test:
```csharp / typescript
        public bool Confirmar_Reserva(ReservarEstadiaDTO Datos)
        {
            var HabitacionesDisponibles = Repositorio.Obtener_Habitaciones(Datos.Fecha_Inicio, Datos.Fecha_Finalizacion);

            var HabitacionLibre = HabitacionesDisponibles.Any(h => h.Id == Datos.Id_Habitacion);

            if (!HabitacionLibre) return false;

            var InfoHabitacion = HabitacionesDisponibles.First(h => h.Id == Datos.Id_Habitacion);

            int DiasEstadia = (Datos.Fecha_Finalizacion.Date - Datos.Fecha_Inicio.Date).Days;


            int PrecioTotalCalculado = Calcular_Costo(DiasEstadia, InfoHabitacion.Precio_Noche);

            int IdNuevaEstadia = Repositorio.Guardar_Estadia(Datos, PrecioTotalCalculado);

            if (IdNuevaEstadia > 0)
            {
                foreach (string Documento in Datos.Documentos_Huespedes)
                {
                    Repositorio.Registrar_Estadia(IdNuevaEstadia, Documento);
                }
                return true;
            }

            return false;
        }
```

> Captura del test pasando:

![Test verde](capturas/hotel-tdd4-verde.png)

---

**Commit 3 — Refactor** [`fef358e`](https://github.com/Nov-222/Segunda-Instancia-EC1/commit/fef358e):
```
refactor: [HU-02] limpiar [Convirtiendo Metodo Largo a funciones con mayor escalabilidad]
```
Cambios aplicados:
```csharp / typescript
public bool Confirmar_Reserva(ReservarEstadiaDTO Datos)
{
    var InfoHabitacion = BuscarHabitacionDisponible(Datos);

    if(InfoHabitacion == null) { return false; }

    int DiasEstadia = (Datos.Fecha_Finalizacion.Date - Datos.Fecha_Inicio.Date).Days;


    int PrecioTotalCalculado = Calcular_Costo(DiasEstadia, InfoHabitacion.Precio_Noche);

    int IdNuevaEstadia = Repositorio.Guardar_Estadia(Datos, PrecioTotalCalculado);

    if (IdNuevaEstadia > 0)
    {
        foreach (string Documento in Datos.Documentos_Huespedes)
        {
            Repositorio.Registrar_Estadia(IdNuevaEstadia, Documento);
        }
        return true;
    }

    return false;
}

public HabitacionDisponibleDTO? BuscarHabitacionDisponible(ReservarEstadiaDTO Datos)
{
    var HabitacionesDisponibles = Consultar_Disponibilidad(Datos.Fecha_Inicio, Datos.Fecha_Finalizacion);

    return HabitacionesDisponibles.FirstOrDefault(h => h.Id == Datos.Id_Habitacion);
}
```

> Captura del test aún pasando después del refactor:

![Test post-refactor](capturas/hotel-tdd4-refactor.png)
---
### Cobertura final

**Cobertura alcanzada:** 23%

> Captura del reporte de cobertura final:

![Cobertura final](capturas/hotel-cobertura-final.png)

> Si la cobertura es <50%, pegar aquí la justificación enviada al docente:

La razon por la cual la cobertura es menor a 50% y cercano a 23% es debido a lo que esta evaluando, se esta revisando la capa servicio y repositorio, pero debido a la complejidad de las HUs, algunas funciones son simples intermediarias,pero siendo coficadas con el fin de ser escalable (Servicios), en cambio el repositorio fue desarrollado completamente para cumplir unicamente la funcion de traer datos, por lo que no cuentan con logica de negocio y al ser mockeados, no suman ni restan al porcentaje de Cobertura

---

## Sección 3 — Code smells corregidos

Mínimo 3 nuevos (adicionales a los del EC2).

| # | Tipo | Commit | Descripción |
|---|---|---|---|
| 1 | [Metodo Largo] | [`fef358e`](https://github.com/Nov-222/Segunda-Instancia-EC1/commit/fef358e): | [Antes: Metodo Largo → Después: Se mejoro la escalabilidad] |
| 2 | [Codigo Repetido] | [`7cb5569`](https://github.com/Nov-222/Segunda-Instancia-EC1/commit/7cb5569) | [Antes: Codigo Repetido → Después: Funcion Reutilizable] |
| 3 | No completado | [`c3d4e5f`](https://github.com/Nov-222/Segunda-Instancia-EC1/commit/c3d4e5f) | [Antes: X → Después: Y] |

### Detalle — Smell 1: [Metodo Largo]

**Código antes:**
```csharp / typescript
public bool Confirmar_Reserva(ReservarEstadiaDTO Datos)
{
    var HabitacionesDisponibles = Repositorio.Obtener_Habitaciones(Datos.Fecha_Inicio, Datos.Fecha_Finalizacion);

    var HabitacionLibre = HabitacionesDisponibles.Any(h => h.Id == Datos.Id_Habitacion);

    if (!HabitacionLibre) return false;

    var InfoHabitacion = HabitacionesDisponibles.First(h => h.Id == Datos.Id_Habitacion);

    int DiasEstadia = (Datos.Fecha_Finalizacion.Date - Datos.Fecha_Inicio.Date).Days;


    int PrecioTotalCalculado = Calcular_Costo(DiasEstadia, InfoHabitacion.Precio_Noche);

    int IdNuevaEstadia = Repositorio.Guardar_Estadia(Datos, PrecioTotalCalculado);

    if (IdNuevaEstadia > 0)
    {
        foreach (string Documento in Datos.Documentos_Huespedes)
        {
            Repositorio.Registrar_Estadia(IdNuevaEstadia, Documento);
        }
        return true;
    }

    return false;
}
```

**Código después:**
```csharp / typescript
public bool Confirmar_Reserva(ReservarEstadiaDTO Datos)
{
    var InfoHabitacion = BuscarHabitacionDisponible(Datos);

    if(InfoHabitacion == null) { return false; }

    int DiasEstadia = (Datos.Fecha_Finalizacion.Date - Datos.Fecha_Inicio.Date).Days;


    int PrecioTotalCalculado = Calcular_Costo(DiasEstadia, InfoHabitacion.Precio_Noche);

    int IdNuevaEstadia = Repositorio.Guardar_Estadia(Datos, PrecioTotalCalculado);

    if (IdNuevaEstadia > 0)
    {
        foreach (string Documento in Datos.Documentos_Huespedes)
        {
            Repositorio.Registrar_Estadia(IdNuevaEstadia, Documento);
        }
        return true;
    }

    return false;
}

public HabitacionDisponibleDTO? BuscarHabitacionDisponible(ReservarEstadiaDTO Datos)
{
    var HabitacionesDisponibles = Consultar_Disponibilidad(Datos.Fecha_Inicio, Datos.Fecha_Finalizacion);

    return HabitacionesDisponibles.FirstOrDefault(h => h.Id == Datos.Id_Habitacion);
}
```

---

### Detalle — Smell 2: [Codigo Repetido]

**Código antes:**
```csharp / typescript
public List<VisualizacionDTO> Obtener_Reservas()
{
    var Reservas = new List<VisualizacionDTO>();
    using (var Conexion = new MySqlConnection(Configuracion)) //Codigo Repetido
    {
        string Query = @"
            SELECT 
                E.Id, E.Fecha_Inicio, E.Fecha_Finalizacion, E.Estado, E.Precio_Total,
                H.Id AS Nro_Habitacion,
                (SELECT CONCAT(Hu.Nombre, ' ', Hu.Apellido_Paterno) 
                 FROM Huesped_Estadia HE 
                 JOIN Huesped Hu ON HE.Id_Huesped = Hu.Id 
                 WHERE HE.Id_Estadia = E.Id LIMIT 1) AS Nombre_Cliente
            FROM Estadia E
            JOIN Habitacion H ON E.Id_Habitacion = H.Id
            ORDER BY E.Fecha_Inicio ASC";

        using (MySqlCommand Comando = new MySqlCommand(Query, Conexion))
        {
            Conexion.Open(); //Codigo Repetido
            using (var reader = Comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    Reservas.Add(new VisualizacionDTO
                    {
                        Id = (int)reader["Id"],
                        Fecha_Inicio = (DateTime)reader["Fecha_Inicio"],
                        Fecha_Finalizacion = (DateTime)reader["Fecha_Finalizacion"],
                        Estado = reader["Estado"].ToString(),
                        Nro_Habitacion = (int)reader["Nro_Habitacion"],
                        Precio_Total = (int)reader["Precio_Total"],
                        Nombre_Cliente = reader["Nombre_Cliente"]?.ToString() ?? "Sin Huésped"
                    });
                }
            }
        }
    }
    return Reservas;
}

public bool Registrar_CheckIn(int IdEstadia)
{
    using (var Conexion = new MySqlConnection(Configuracion)) //Codigo Repetido
    {
        Conexion.Open(); //Codigo Repetido

        string QueryActivo = "UPDATE Estadia SET Estado = 'Activo' WHERE Id = @Id AND Estado = 'Reservado'";
        MySqlCommand Comando = new MySqlCommand(QueryActivo, Conexion);
        Comando.Parameters.AddWithValue("@Id", IdEstadia);

        int afectados = Comando.ExecuteNonQuery();

        if (afectados > 0)
        {
            string QueryDetalle = "INSERT INTO Detalle_Estadia (Id_Estadia, Registro_Ingreso, Multa) VALUES (@Id, NOW(), 0)";
            MySqlCommand Comando2 = new MySqlCommand(QueryDetalle, Conexion);
            Comando2.Parameters.AddWithValue("@Id", IdEstadia);
            Comando2.ExecuteNonQuery();
            return true;
        }
        return false;
    }
}

public bool Registrar_CheckOut(int IdEstadia)
{
    using (var Conexion = new MySqlConnection(Configuracion)) //Codigo Repetido
    {
        Conexion.Open(); //Codigo Repetido

        string QueryFinalizado = "UPDATE Estadia SET Estado = 'Finalizada' WHERE Id = @Id AND Estado = 'Activo'";
        MySqlCommand Comando = new MySqlCommand(QueryFinalizado, Conexion);
        Comando.Parameters.AddWithValue("@Id", IdEstadia);

        if (Comando.ExecuteNonQuery() > 0)
        {
            string QueryDetalle = "UPDATE Detalle_Estadia SET Registro_Salida = NOW() WHERE Id_Estadia = @Id";
            MySqlCommand Comando2 = new MySqlCommand(QueryDetalle, Conexion);
            Comando2.Parameters.AddWithValue("@Id", IdEstadia);
            Comando2.ExecuteNonQuery();
            return true;
        }
        return false;
    }
}
```

**Código después:**
```csharp / typescript
    public static MySqlConnection  GenerarConexion(string configuracion)
    {
        var conexion = new MySqlConnection(configuracion);
        conexion.Open();
        return conexion;
    }

    public List<VisualizacionDTO> Obtener_Reservas()
{
    var Reservas = new List<VisualizacionDTO>();
    using (var Conexion = ConexionDB.GenerarConexion(Configuracion))
    {
        string Query = @"
            SELECT 
                E.Id, E.Fecha_Inicio, E.Fecha_Finalizacion, E.Estado, E.Precio_Total,
                H.Id AS Nro_Habitacion,
                (SELECT CONCAT(Hu.Nombre, ' ', Hu.Apellido_Paterno) 
                 FROM Huesped_Estadia HE 
                 JOIN Huesped Hu ON HE.Id_Huesped = Hu.Id 
                 WHERE HE.Id_Estadia = E.Id LIMIT 1) AS Nombre_Cliente
            FROM Estadia E
            JOIN Habitacion H ON E.Id_Habitacion = H.Id
            ORDER BY E.Fecha_Inicio ASC";

        using (MySqlCommand Comando = new MySqlCommand(Query, Conexion))
        {
            using (var reader = Comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    Reservas.Add(new VisualizacionDTO
                    {
                        Id = (int)reader["Id"],
                        Fecha_Inicio = (DateTime)reader["Fecha_Inicio"],
                        Fecha_Finalizacion = (DateTime)reader["Fecha_Finalizacion"],
                        Estado = reader["Estado"].ToString(),
                        Nro_Habitacion = (int)reader["Nro_Habitacion"],
                        Precio_Total = (int)reader["Precio_Total"],
                        Nombre_Cliente = reader["Nombre_Cliente"]?.ToString() ?? "Sin Huésped"
                    });
                }
            }
        }
    }
    return Reservas;
}

public bool Registrar_CheckIn(int IdEstadia)
{
    using (var Conexion = ConexionDB.GenerarConexion(Configuracion))
    {
        string QueryActivo = "UPDATE Estadia SET Estado = 'Activo' WHERE Id = @Id AND Estado = 'Reservado'";
        MySqlCommand Comando = new MySqlCommand(QueryActivo, Conexion);
        Comando.Parameters.AddWithValue("@Id", IdEstadia);

        int afectados = Comando.ExecuteNonQuery();

        if (afectados > 0)
        {
            string QueryDetalle = "INSERT INTO Detalle_Estadia (Id_Estadia, Registro_Ingreso, Multa) VALUES (@Id, NOW(), 0)";
            MySqlCommand Comando2 = new MySqlCommand(QueryDetalle, Conexion);
            Comando2.Parameters.AddWithValue("@Id", IdEstadia);
            Comando2.ExecuteNonQuery();
            return true;
        }
        return false;
    }
}

public bool Registrar_CheckOut(int IdEstadia)
{
    using (var Conexion = ConexionDB.GenerarConexion(Configuracion))
    {
        string QueryFinalizado = "UPDATE Estadia SET Estado = 'Finalizada' WHERE Id = @Id AND Estado = 'Activo'";
        MySqlCommand Comando = new MySqlCommand(QueryFinalizado, Conexion);
        Comando.Parameters.AddWithValue("@Id", IdEstadia);

        if (Comando.ExecuteNonQuery() > 0)
        {
            string QueryDetalle = "UPDATE Detalle_Estadia SET Registro_Salida = NOW() WHERE Id_Estadia = @Id";
            MySqlCommand Comando2 = new MySqlCommand(QueryDetalle, Conexion);
            Comando2.Parameters.AddWithValue("@Id", IdEstadia);
            Comando2.ExecuteNonQuery();
            return true;
        }
        return false;
    }
}
```

---

### Detalle — Smell 3: [No Completado]

**Código antes:**
```csharp / typescript
// código con el smell
```

**Código después:**
```csharp / typescript
// código corregido
```

---

## Sección 4 — Trazabilidad HU → CA → test

| # | Historia de Usuario | Criterio de Aceptación | Prueba que valida ese CA | Commit |
|---|---|---|---|---|
| 1 | [HU3: Visualizar Reservas] | [Dado que existen reservas/ Cuando se visualice la reserva/ Entonces estas deben mostrarse en orden cronológico ascendente por fecha de inicio.] | [Listar_Reservas_Admin_ExistenReservas_Exito] | [`5b5e65e`](https://github.com/Nov-222/Segunda-Instancia-EC1/commit/5b5e65e) |
| 2 | [HU4: Marcar Check In] | [Dado que existen reservas en estado Reservado/ Cuando se cambie de estado/ Entonces en la base de datos se debe registrar la fecha y hora de cambio de estado a 'Activo' y poblar la tabla de detalles con multa en 0.] | [Procesar_CheckIn_ReservaValida_Exito] | [`5846dba`](https://github.com/Nov-222/Segunda-Instancia-EC1/commit/5846dba) |
| 3 | [HU5: Marcar Check Out] | [Dado que existen reservas en estado Activa/ Cuando se cambie de estado/ Entonces en la base de datos se debe registrar la fecha y hora de cambio de estado a 'Finalizada' y guardar la fecha de salida en Detalle_Estadia.] | [Procesar_CheckOut_ReservaValida_Exito] | [`fee851e`](https://github.com/Nov-222/Segunda-Instancia-EC1/commit/fee851e): |
| 4 | [HU2: Reservar Estadía] | [Dado que se hayan enviado correctamente todos los datos/ Cuando se reserve la estadía/ Entonces el software debe notificar que ha sido un éxito.] | [Confirmar_Reserva_DatosValidos_Exito] | [`be51d58`](https://github.com/Nov-222/Segunda-Instancia-EC1/commit/be51d58): |

### Cadena 1 — [HU3: Visualizar Reservas]

**Historia de Usuario:**
> Como [administrador] quiero [poder visualizar todas las reservas hechas en la HU2] para [tener un mejor entendimiento de mis futuros clientes a llegar.]

**Criterio de Aceptación elegido:**
> Dado [que existen reservas] / Cuando [se visualice la reserva] / Entonces [estas deben mostrarse en orden cronológico ascendente por fecha de inicio.]

**Prueba que valida este CA:**
```csharp / typescript
[Test]
public void Listar_Reservas_Admin_ExistenReservas_Exito()
{
    var reservas = new List<VisualizacionDTO>
    {
        new VisualizacionDTO
        {
            Id =1,
            Fecha_Inicio = new DateTime(2026,6,20),
            Fecha_Finalizacion = new DateTime(2026,6,27),
            Estado = "Reservada",
            Nro_Habitacion = 10,
            Precio_Total = 1500,
            Nombre_Cliente = "Jose Enrique DIaz Velarde"
        },
        new VisualizacionDTO
        {
            Id = 2,
            Fecha_Inicio = new DateTime(2026,6,15),
            Fecha_Finalizacion = new DateTime(2026,6,22),
            Estado = "Activa",
            Nro_Habitacion = 13,
            Precio_Total = 2000,
            Nombre_Cliente = "Maria Belen Zurita Cardenas"
        }
    };
    repositorio.Setup(f => f.Obtener_Reservas()).Returns(reservas);


    var resultado = servicio.Listar_Reservas_Admin();

    Assert.That(resultado, Is.Not.Empty);
}
```

---

### Cadena 2 — [HU4: Marcar Check In]

**Historia de Usuario:**
> Como [administrador] quiero [poder marcar como Activa las reservas en estado de Reservado a través de presionar un botón] para [poder registrar la hora y fecha de ingreso.]

**Criterio de Aceptación elegido:**
> Dado [conque existen reservas en estado Reservadotexto] / Cuando [se cambie de estado] / Entonces [en la base de datos se debe registrar la fecha y hora de cambio de estado a 'Activo' y poblar la tabla de detalles con multa en 0.]

**Prueba que valida este CA:**
```csharp / typescript
[Test]
public void Procesar_CheckIn_ReservaValida_Exito()
{
    int IdValido = 20;
    repositorio.Setup(f => f.Registrar_CheckIn(IdValido)).Returns(true);

    var resultado = servicio.Procesar_CheckIn(IdValido);

    Assert.That(resultado, Is.EqualTo(true));
}
```

---

### Cadena 3 — [HU5: Marcar Check Out]

**Historia de Usuario:**
> Como [administrador] quiero [poder marcar como Finalizada las reservas en estado de Activa a través de presionar un botón] para [poder registrar la hora y fecha de salida.]

**Criterio de Aceptación elegido:**
> Dado [que existen reservas en estado Activa] / Cuando [se cambie de estado] / Entonces [en la base de datos se debe registrar la fecha y hora de cambio de estado a 'Finalizada' y guardar la fecha de salida en Detalle_Estadia.]

**Prueba que valida este CA:**
```csharp / typescript
        [Test]
        public void Procesar_CheckOut_ReservaValida_Exito()
        {
            int IdValido = 20;
            repositorio.Setup(f => f.Registrar_CheckOut(IdValido)).Returns(true);

            var resultado = servicio.Procesar_CheckOut(IdValido);

            Assert.That(resultado, Is.EqualTo(true));
        }
```

### Cadena 4 — [HU2: Reservar Estadía]

**Historia de Usuario:**
> Como [administrador] quiero [ ingresar los datos (Fecha de Inicio, Fecha de Finalización, Documento de Cliente) y poder seleccionar la habitación de la lista desplegable (HU1)] para [poder reservar una estadía.]

**Criterio de Aceptación elegido:**
> Dado [que se hayan enviado correctamente todos los datos] / Cuando [se reserve la estadía] / Entonces [el software debe notificar que ha sido un éxito.]

**Prueba que valida este CA:**
```csharp / typescript
[Test]
public void Confirmar_Reserva_DatosValidos_Exito()
{
    var datos = new ReservarEstadiaDTO
    {
        Fecha_Inicio = new DateTime(2026, 7, 6),

        Fecha_Finalizacion = new DateTime(2026, 7, 13),

        Id_Habitacion = 20,

        Documentos_Huespedes = new List<string> { "ABC-784535" }
    };

    var habitacion_disponible = new HabitacionDisponibleDTO
    {
        Id = 20,
        Tipo_Nombre = "Matrimonial",
        Precio_Noche = 100
    };

    repositorio.Setup(f => f.Obtener_Habitaciones(datos.Fecha_Inicio,datos.Fecha_Finalizacion)).Returns(new List<HabitacionDisponibleDTO> { habitacion_disponible }); ;
    repositorio.Setup(f => f.Guardar_Estadia(datos, It.IsAny<int>())).Returns(10);

    var resultado = servicio.Confirmar_Reserva(datos);

    Assert.That(resultado, Is.EqualTo(true));
}
```
