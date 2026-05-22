import {service} from '../Services/service.js'

const Reservado = document.getElementById("lista-reservado");
const Activo = document.getElementById("lista-activo");
const Finalizado = document.getElementById("lista-finalizado");

async function solicitarCheckIn(id) {
    if (!confirm("¿Desea confirmar el Check-In?")) return;

    try {
        const exito = await service.registrarCheckIn(id);
        if (exito) {
            alert("Check-In exitoso");
            ObtenerYMostrarReservas(); 
        } else {
            alert("No se pudo procesar el Check-In");
        }
    } catch (error) {
        console.error("Error en Check-In:", error);
    }
}

async function solicitarCheckOut(id) {
    if (!confirm("¿Desea confirmar el Check-Out?")) return;

    try {
        const exito = await service.registrarCheckOut(id);
        if (exito) {
            alert("Check-Out exitoso");
            ObtenerYMostrarReservas(); 
        } else {
            alert("No se pudo procesar el Check-Out.");
        }
    } catch (error) {
        console.error("Error en Check-Out:", error);
    }
}

function crearTarjetaReserva(reserva) {
    const tarjeta = document.createElement("div");
    tarjeta.className = "card";
    tarjeta.style.cssText = "background: white; border: 1px solid #ddd; border-radius: 8px; padding: 15px; margin-bottom: 15px; box-shadow: 2px 2px 5px rgba(0,0,0,0.05);";
    const nombreCliente = reserva.nombre_Cliente ? reserva.nombre_Cliente : "Sin Huésped";

    tarjeta.innerHTML = `
        <p><strong>Cliente:</strong> ${nombreCliente}</p>
        <p><strong>Habitación:</strong> ${reserva.nro_Habitacion}</p>
        <p><strong>Total:</strong> $${reserva.precio_Total}</p>
        <p style="font-size: 0.85em; color: #666; margin-top: 5px;">
            ${new Date(reserva.fecha_Inicio).toLocaleDateString()} - ${new Date(reserva.fecha_Finalizacion).toLocaleDateString()}
        </p>
    `;

    if (reserva.estado === "Reservado") {
        const botonCheckIn = document.createElement("button");
        botonCheckIn.textContent = "Registrar Check-In";
        botonCheckIn.style.cssText = "width:100%; background:#d35400; color:white; border:none; padding:8px; margin-top:10px; border-radius:4px; cursor:pointer; font-weight:bold;";
        
        // Conexión directa del evento (ESLint ahora sabe que la función se usa)
        botonCheckIn.addEventListener("click", () => solicitarCheckIn(reserva.id));
        tarjeta.appendChild(botonCheckIn);

    } else if (reserva.estado === "Activo") {
        const botonCheckOut = document.createElement("button");
        botonCheckOut.textContent = "Registrar Check-Out";
        botonCheckOut.style.cssText = "width:100%; background:#27ae60; color:white; border:none; padding:8px; margin-top:10px; border-radius:4px; cursor:pointer; font-weight:bold;";
        
        botonCheckOut.addEventListener("click", () => solicitarCheckOut(reserva.id));
        tarjeta.appendChild(botonCheckOut);
    }

    return tarjeta;
}

async function ObtenerYMostrarReservas() {
    try {
        const datos = await service.listarReservas();

        Reservado.innerHTML = "";
        Activo.innerHTML = "";
        Finalizado.innerHTML = "";

        // Distribuimos las tarjetas en sus respectivos contenedores según el estado
        datos.forEach(reserva => {
            const nuevaTarjeta = crearTarjetaReserva(reserva);

            if (reserva.estado === "Reservado") {
                Reservado.appendChild(nuevaTarjeta);
            } else if (reserva.estado === "Activo") {
                Activo.appendChild(nuevaTarjeta);
            } else if (reserva.estado === "Finalizada") {
                Finalizado.appendChild(nuevaTarjeta);
            }
        });

    } catch (error) {
        console.error("Error al cargar reservas:", error);
    }
}

ObtenerYMostrarReservas();