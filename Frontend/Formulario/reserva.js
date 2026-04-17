const URL = "http://localhost:5192/api/reserva"; 
const $inicio = document.getElementById("FechaInicio");
const $fin = document.getElementById("FechaFin");
const $select = document.getElementById("SelectHabitacion");
const $form = document.getElementById("FormReserva");

async function buscar() {
    if (!$inicio.value || !$fin.value) return;

    const res = await fetch(`${URL}/disponibilidad?Inicio=${$inicio.value}&Fin=${$fin.value}`);
    const habitaciones = await res.json();

    $select.innerHTML = '<option value="">-- Habitación --</option>';
    habitaciones.forEach(h => {
        $select.innerHTML += `<option value="${h.id}">${h.tipo_Nombre} ($${h.precio_Noche})</option>`;
    });
    $select.disabled = false;
}

$inicio.onchange = buscar;
$fin.onchange = buscar;

$form.onsubmit = async (e) => {
    e.preventDefault();

    const payload = {
        fecha_Inicio: $inicio.value,
        fecha_Finalizacion: $fin.value,
        id_Habitacion: parseInt($select.value),
        documentos_Huespedes: document.getElementById("Documentos").value.split(",").map(d => d.trim())
    };

    const res = await fetch(`${URL}/reservar`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(payload)
    });

    if (res.ok) {
        alert("Ha sido Reservado Exitosamente");
        $form.reset();
        $select.disabled = true;
    } else {
        alert("Inconveniente Inesperado");
    }
};