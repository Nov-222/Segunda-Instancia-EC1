const BASE_URL = "http://localhost:5192/api";

export const service = {
    async obtenerDisponibilidad(inicio, fin) {
        const res = await fetch(`${BASE_URL}/reserva/disponibilidad?Inicio=${inicio}&Fin=${fin}`);
        if (!res.ok) throw new Error("Error al consultar disponibilidad");
        return await res.json();
    },

    async crearReserva(payload) {
        const res = await fetch(`${BASE_URL}/reserva/reservar`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(payload)
        });
        return res.ok;
    },

    async listarReservas() {
        const res = await fetch(`${BASE_URL}/consulta/reservas`);
        if (!res.ok) throw new Error("Error al traer las reservas");
        return await res.json();
    },

    async registrarCheckIn(id) {
        const res = await fetch(`${BASE_URL}/consulta/checkin/${id}`, { method: "PUT" });
        return res.ok;
    },

    async registrarCheckOut(id) {
        const res = await fetch(`${BASE_URL}/consulta/checkout/${id}`, { method: "PUT" });
        return res.ok;
    }
};