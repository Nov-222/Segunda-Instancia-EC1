const API_URL = "http://localhost:5192/api/consulta/reservas";

async function cargarReservas() {
    try {
        const respuesta = await fetch(API_URL);
        if (!respuesta.ok) throw new Error("No se pudo conectar con el API");
        
        const datos = await respuesta.json();

        const divReservado = document.getElementById("lista-reservado");
        const divActivo = document.getElementById("lista-activo");
        const divFinalizado = document.getElementById("lista-finalizado");

        divReservado.innerHTML = "";
        divActivo.innerHTML = "";
        divFinalizado.innerHTML = "";

        datos.forEach(reserva => {
            let botonAccion = "";

            if (reserva.estado === "Reservado") {
                botonAccion = `
                    <button onclick="ejecutarCheckIn(${reserva.id})" 
                    style="width:100%; background:#d35400; color:white; border:none; padding:8px; margin-top:10px; border-radius:4px; cursor:pointer; font-weight:bold;">
                        Registrar Check-In
                    </button>`;
            } 
            else if (reserva.estado === "Activo") {
                botonAccion = `
                    <button onclick="ejecutarCheckOut(${reserva.id})" 
                    style="width:100%; background:#27ae60; color:white; border:none; padding:8px; margin-top:10px; border-radius:4px; cursor:pointer; font-weight:bold;">
                        Registrar Check-Out
                    </button>`;
            }

            const cardHTML = `
                <div class="card" style="background: white; border: 1px solid #ddd; border-radius: 8px; padding: 15px; margin-bottom: 15px; box-shadow: 2px 2px 5px rgba(0,0,0,0.05);">
                    <p><strong>Cliente:</strong> ${reserva.nombre_Cliente}</p>
                    <p><strong>Habitación:</strong> ${reserva.nro_Habitacion}</p>
                    <p><strong>Total:</strong> $${reserva.precio_Total}</p>
                    <p style="font-size: 0.85em; color: #666; margin-top: 5px;">
                        📅 ${new Date(reserva.fecha_Inicio).toLocaleDateString()} - ${new Date(reserva.fecha_Finalizacion).toLocaleDateString()}
                    </p>
                    ${botonAccion} 
                </div>
            `;

            if (reserva.estado === "Reservado") {
                divReservado.innerHTML += cardHTML;
            } else if (reserva.estado === "Activo") {
                divActivo.innerHTML += cardHTML;
            } else if (reserva.estado === "Finalizada") {
                divFinalizado.innerHTML += cardHTML;
            }
        });

    } catch (error) {
        console.error("Error al cargar reservas:", error);
    }
}

async function ejecutarCheckIn(id) {
    if (!confirm("¿Desea confirmar el Check-In?")) return;

    try {
        const respuesta = await fetch(`http://localhost:5192/api/consulta/checkin/${id}`, {
            method: 'PUT'
        });

        if (respuesta.ok) {
            alert("Check-In exitoso");
            cargarReservas(); 
        } else {
            alert("No se pudo procesar el Check-In");
        }
    } catch (error) {
        console.error("Error en Check-In:", error);
    }
}

async function ejecutarCheckOut(id) {
    if (!confirm("¿Desea confirmar el Check-Out?")) return;

    try {
        const respuesta = await fetch(`http://localhost:5192/api/consulta/checkout/${id}`, {
            method: 'PUT'
        });

        if (respuesta.ok) {
            alert("Check-Out exitoso");
            cargarReservas(); 
        } else {
            alert("No se pudo procesar el Check-Out.");
        }
    } catch (error) {
        console.error("Error en Check-Out:", error);
    }
}

document.addEventListener("DOMContentLoaded", cargarReservas);