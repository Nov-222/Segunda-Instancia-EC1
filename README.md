# Segunda-Instancia-EC1

# Contexto:
Se ha presentado el desarrollo de una página para la administración de un hotel que actualmente tiene problemas al reservar estadías, registrar usuarios, organizar cuartos para determinar cuáles están disponibles y marcar la hora de ingreso y salida de sus huéspedes (Check-In y Check-Out), debido a que todo se maneja en papel, por ello se necesita una mejor manera de facilitar estos trabajos a través de una página que cuente con visualización y registro de estadías e usuarios.

# Usuarios:
- Administrador (Usuario Principal): Se encarga de administrar todos los demás servicios, como registrar estadía, verificar disponibilidad, registrar check in, check out y guardar a nuevos clientes.
- Clientes del Hotel (Usuario Secundario): Solamente se registra en la base de datos para auditoría de ingreso de personas al hotel. 

# Alcance:
- Incluye: 
1. Crear Estadía: Registrar los datos básicos como datos del usuario, inicio de Estadía, Finalización de Estadía y habitación.
2. Realizar Check-In: Marcar la hora de ingreso de los clientes en su estadía y pasar estado a Activa.
3. Realizar Check-Out: Marcar la hora de salida de los clientes en su estadía y pasar estado a Finalizada.  
- No Incluye: 
1. Registro de Mora: No se tomará en cuenta las políticas de la empresa respecto al estado de multas.

# Historias de Usuario:

# HU1: Disponibilidad
Yo como administrador quiero ingresar la fecha de ingreso y salida de mi estancia para poder visualizar las habitaciones disponibles en una lista desplegable.

- Dado que existe alguna habitación disponible dentro del rango de fechas, cuando el software devuelva los datos, entonces se visualizará el tipo de habitación + precio por noche.

- Dado que no existe alguna habitación disponible dentro del rango de fechas, cuando el software devuelva los datos, entonces devolverá una lista vacía y no será posible interactuar con la lista desplegable.

- Dado que el administrador ingresa una fecha de finalización menor o igual a la fecha de inicio, cuando se solicite la disponibilidad, entonces el sistema devolverá una lista vacía y no procesará la búsqueda en la base de datos.

- Dado que el administrador busca disponibilidad para el mismo día actual (Check-in hoy y Check-out mañana en el límite de cambio de fecha), cuando el software procese la solicitud, entonces debe calcular exactamente 1 noche de estadía y mostrar las habitaciones disponibles.

# HU2: Reservar Estadía
Yo como administrador quiero ingresar los datos (Fecha de Inicio, Fecha de Finalización, Documento de Cliente) y poder seleccionar la habitación de la lista desplegable (HU1) para poder reservar una estadía.

- Dado que se hayan enviado correctamente todos los datos, cuando se reserve la estadía, entonces el software debe notificar que ha sido un éxito.

- Dado que no se hallen los datos de Fecha de Inicio, Fecha de Finalización y Documento de Cliente, cuando se presione el botón de enviar, entonces se solicitará a través de una alerta que se llenen los campos obligatorios.

- Dado que se intente reservar una habitación que fue ocupada por otra transacción en el último segundo, cuando se procese la confirmación, entonces el software notificará que hubo un inconveniente de disponibilidad y no guardará los datos.

- Dado que una reserva se realice para una estancia extremadamente larga (ej. 365 días continuos), cuando se confirme la reserva, entonces el software debe notificar que el maximo de enstancia es un mes.

# HU3: Visualizar Reservas
Yo como administrador quiero poder visualizar todas las reservas hechas en la HU2 para tener un mejor entendimiento de mis futuros clientes a llegar.

- Dado que existen reservas, cuando se visualice la reserva, entonces estas deben mostrarse en orden cronológico ascendente por fecha de inicio.

- Dado que existen reservas, cuando se visualice la reserva, entonces estas deben estar en el formato (Fecha de Inicio, Fecha de Finalización, Estado, Nro de Habitación, precio total y el nombre de la persona a cargo de la reserva) y separadas en bloques de acuerdo a su estado.

- Dado que la base de datos no tenga ninguna reserva registrada, cuando el administrador ingrese a la vista de consultas, entonces el sistema mostrará un mensaje indicando "No se encontraron registros de reservas actuales".

- Dado que una reserva no cuente con un huésped asignado temporalmente en la tabla relacional, cuando se liste en el panel del administrador, entonces el sistema debe mostrar la fila de la reserva sustituyendo el nombre del cliente con el texto por defecto "Sin Huésped" sin romper la interfaz gráfica.

# HU4: Marcar Check In
Yo como administrador quiero poder marcar como Activa las reservas en estado de Reservado a través de presionar un botón para poder registrar la hora y fecha de ingreso.

- Dado que existen reservas en estado Reservado, cuando se cambie de estado, entonces en la base de datos se debe registrar la fecha y hora de cambio de estado a 'Activo' y poblar la tabla de detalles con multa en 0.

- Dado que existen reservas activas, cuando se presione dos veces el botón, entonces no debe ser marcada dos veces como Activa ni duplicar el registro en la base de datos.

- Dado que una reserva se encuentre en estado 'Cancelada' o 'Finalizada', cuando se intente presionar el botón de Check-in, entonces el sistema deshabilitará la acción y el backend rechazará la edición del estado.

- Dado que el administrador marca el Check-in exactamente en el último minuto del día de la reserva (23:59), cuando se procese el ingreso, entonces el sistema debe registrar el ingreso con la hora exacta del servidor sin mover la fecha programada de la estancia.

# HU5: Marcar Check Out
Yo como administrador quiero poder marcar como Finalizada las reservas en estado de Activa a través de presionar un botón para poder registrar la hora y fecha de salida.

- Dado que existen reservas en estado Activa, cuando se cambie de estado, entonces en la base de datos se debe registrar la fecha y hora de cambio de estado a 'Finalizada' y guardar la fecha de salida en Detalle_Estadia.

- Dado que existen reservas finalizadas, cuando se presione dos veces el botón, entonces no debe ser marcada dos veces como Finalizada.

- Dado que la reserva esté todavía en estado 'Reservado' (sin haber hecho Check-in previo), cuando se intente ejecutar el Check-out, entonces el sistema bloqueará la operación exigiendo el flujo de ingreso reglamentario.

- Dado que el cliente realice el Check-out de manera anticipada (ej. contrató 5 días pero se retira al día 1), cuando se presione el botón de finalizar, entonces el software debe cerrar la estancia inmediatamente registrando la hora de salida real sin alterar el cobro total ya facturado.

# HU6: Registrar Nuevo Cliente
Yo como administrador quiero poder registrar un nuevo cliente a través de sus datos personales para actualizar mi base de datos de clientes.

- Dado que todos los campos requeridos (Nombre, Apellidos, Documento, Teléfono, Email, Fecha de Nacimiento) son válidos y el documento no existe, cuando se envíe el formulario, entonces el sistema guardará el registro del cliente exitosamente.

- Dado que el cliente no haya llenado todos los campos, cuando envíe la solicitud de registro, entonces el Sistema le notificará que todos los campos son obligatorios y detendrá la inserción.

- Dado que el Documento enviado ya se encuentre registrado, cuando se intente registrar, el Sistema debe notificar que ya existe un cliente en la base de datos con este documento.

- Dado que el cliente a registrar nació el día de hoy (edad 0 años, un recién nacido registrado bajo el documento de tutor o un cliente nacido en año bisiesto el 29 de febrero), cuando el backend valide el formato de la fecha de nacimiento, entonces la base de datos debe almacenar la fecha correctamente sin generar excepciones de desbordamiento de calendario.

# Arquitectura
Adjunto como Diagrama_Arquitectura

# Modelo de Base De Datos
Adjunta en la Capa de Datos.
