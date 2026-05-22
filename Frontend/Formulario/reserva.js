import {service} from '../Services/service.js'

const Formulario = document.getElementById("FormReserva");
const FechaInicio = document.getElementById("FechaInicio");
const FechaFin = document.getElementById("FechaFin");
const Habitacion = document.getElementById("SelectHabitacion");
const Documentos = document.getElementById("Documentos");

async function BuscarHabitaciones() {
    const inicio = FechaInicio.value;
    const fin = FechaFin.value;

    if (!inicio || !fin) return;

    try {
        const habitaciones = await service.obtenerDisponibilidad(inicio, fin);

        Habitacion.innerHTML = '';
        const opcionDefecto = document.createElement("option");
        opcionDefecto.value = "";
        opcionDefecto.textContent = "-- Habitación --";
        Habitacion.appendChild(opcionDefecto);

        habitaciones.forEach(h => {
            const option = document.createElement("option");
            option.value = h.id;
            option.textContent = `${h.tipo_Nombre} ($${h.precio_Noche})`;
            Habitacion.appendChild(option);
        });

        Habitacion.disabled = false;
    } catch (error) {
        console.error("Error al buscar disponibilidad:", error);
    }
}

async function ProcesarReserva(evento) {
    evento.preventDefault();

    const contenido_reserva = {
        fecha_Inicio: FechaInicio.value,
        fecha_Finalizacion: FechaFin.value,
        id_Habitacion: parseInt(Habitacion.value),
        documentos_Huespedes: Documentos.value.split(",").map(doc => doc.trim())
    };

    try {
        const exito = await service.crearReserva(contenido_reserva);

        if (exito) {
            alert("Ha sido Reservado Exitosamente");
            Formulario.reset();
            Habitacion.disabled = true;
        } else {
            alert("Inconveniente Inesperado o Restricción de Estadía");
        }
    } catch (error) {
        console.error("Error de conexión al reservar:", error);
    }
}


function IniciarEventos() {
    FechaInicio.addEventListener("change", BuscarHabitaciones);
    FechaFin.addEventListener("change", BuscarHabitaciones);
    Formulario.addEventListener("submit", ProcesarReserva);
}

IniciarEventos();