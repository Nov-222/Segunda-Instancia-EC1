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
Yo como administrador quiero ingresar la fecha de ingreso y salida de mi estancia para poder visualizar las habitaciones disponibles en una lista desplegable,
C.A:
- Dado que existe alguna habitación disponible dentro del rango de fechas, cuando el software devuelva los datos, entonces se visualizará el tipo de habitación + precio por noche.
- Dado que no existe alguna habitación disponible dentro del rango de fechas, cuando el software devuelva los datos, entonces devolverá una lista vacía y no será posible interactuar con la lista desplegable.

# HU2: Reservar Estadia
Yo como administrador quiero ingresar los siguientes datos (Fecha de Inicio, Fecha de Finalización, Documento de Cliente) y poder seleccionar la habitación de la lista desplegable (HU1) para poder reservar una estadía,
C.A:
- Dado que no se hayan los datos de Fecha de Inicio, Fecha de Finalización y Documento de Cliente, cuando se presione el botón de enviar, entonces se solicitara que se llenen los campos.
- Dado que se hayan enviado correctamente todos los datos, cuando se reserve la estadía, entonces el software debe notificar si ha sido un éxito o hubo un inconveniente.

# HU3: Visualizar Reservas
Yo como administrador quiero poder visualizar todas las reservas hechas en la HU2 para tener un mejor entendimiento de mis futuros clientes a llegar,
C.A:
- Dado que existen reservas, cuando se visualice la reserva, entonces estas deben mostrarse en orden cronológico.
- Dado que existen reservas, cuando se visualice la reserva, entonces estas deben estar en el formato (Fecha de Inicio, Fecha de Finalización, Estado, Nro de Habitación, precio total y el nombre de la persona a cargo de la reserva) y separadas en bloques de acuerdo a  su estado.

# HU4: Marcar Check In
Yo como administrador quiero poder marcar como Activa las reservas en estado de Reservado a través de presionar un botón para poder registrar la hora y fecha de ingreso,
C.A:
- Dado que existen reservas en estado Reservado, cuando se cambie de estado, entonces en la base de datos se debe registrar la fecha y hora de cambio de estado.
- Dado que existen reservas, cuando se presione dos veces el botón, entonces no debe ser marcada dos veces como Activa.

# HU5: Marcar Check Out
Yo como administrador quiero poder marcar como Finalizada las reservas en estado de Activa a través de presionar un botón para poder registrar la hora y fecha de salida,
C.A:
- Dado que existen reservas en estado Activa, cuando se cambie de estado, entonces en la base de datos se debe registrar la fecha y hora de cambio de estado.
- Dado que existen reservas, cuando se presione dos veces el botón, entonces no debe ser marcada dos veces como Finalizada.

# HU6: Registrar Nuevo Cliente
Yo como administrador quiero poder registrar un nuevo cliente a través de los siguientes datos (Nombre, Apellido Paterno, Apellido Materno, Documento, Teléfono, Email y Fecha de Nacimiento) para actualizar mi base de datos de clientes,
C.A:
- Dado que el cliente no haya llenado todos los campos, cuando envíe la solicitud de registro, entonces el Sistema le notificar que todos los campos son obligatorios.
- Dado que el Documento enviado ya se encuentre registrado, cuando se intente registrar, el Sistema debe notificar que ya existe un cliente en la base de datos con este documento.

# Arquitectura
Adjunto como Diagrama_Arquitectura

# Modelo de Base De Datos
Adjunta en la Capa de Datos.
