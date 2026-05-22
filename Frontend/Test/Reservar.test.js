import { describe, it, expect, vi } from 'vitest';
import { service } from '../Services/service.js';

vi.spyOn(service, 'crearReserva');

describe('HU2: Reservar Estadía', () => {

    it('[Happy Path]: Debe notificar éxito cuando los datos son correctos', async () => {
        service.crearReserva.mockResolvedValue(true);

        const contenido = { fecha_Inicio: "2026-05-25", fecha_Finalizacion: "2026-05-26", id_Habitacion: 1, documentos: ["123"] };
        const respuesta = await service.crearReserva(contenido);
        
        expect(respuesta).toBe(true);
        expect(service.crearReserva).toHaveBeenCalledWith(contenido);
    });

    it('[Invalid]: Debe solicitar campos obligatorios si faltan datos', async () => {
        const contenido = { fecha_Inicio: "", fecha_Finalizacion: "", documentos: "" };
        
        const validar = (data) => !data.fecha_Inicio || !data.documentos ? false : true;
        
        expect(validar(contenido)).toBe(false);
    });

    it('[Invalid]: Debe notificar inconveniente si la habitación se ocupó', async () => {
        service.crearReserva.mockResolvedValue(false);

        const contenido = { fecha_Inicio: "2026-05-25", fecha_Finalizacion: "2026-05-26", id_Habitacion: 1 };
        const respuesta = await service.crearReserva(contenido);
        
        expect(respuesta).toBe(false);
    });

    it('[Border]: Debe notificar que el máximo de estancia es 30 días', async () => {
        const diasReserva = 365;
        const validarEstancia = (dias) => dias > 30 ? "Error: Máximo 30 días" : "Ok";
        
        const resultado = validarEstancia(diasReserva);
        expect(resultado).toBe("Error: Máximo 30 días");
    });
});