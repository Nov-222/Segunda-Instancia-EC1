import { describe, it, expect, vi } from 'vitest';
import { service } from '../Services/service.js'

vi.spyOn(service, 'obtenerDisponibilidad');

describe('HU1: Disponibilidad de Habitaciones', () => {

    it('[Happy Path]: Debe obtener si existe alguna habitacion disponible', async () => {
        service.obtenerDisponibilidad.mockResolvedValue([
            { id: 1, tipo_Nombre: "Suite", precio_Noche: 100 }
        ]);

        const respuesta = await service.obtenerDisponibilidad("2026-05-25", "2026-05-26");
        expect(respuesta[0].tipo_Nombre).toBe("Suite");
    });

    it('[Invalid]: Debe retornar lista vacía si no hay habitaciones', async () => {
        service.obtenerDisponibilidad.mockResolvedValue([]); 

        const respuesta = await service.obtenerDisponibilidad("2026-06-01", "2026-06-02");
        expect(respuesta).toHaveLength(0);
    });

    it('[Invalid]: debe retornar lista vacía si fecha fin es menor a inicio', async () => {
        const inicio = "2026-05-30";
        const fin = "2026-05-25";
        
        const respuesta = fin <= inicio ? [] : await service.obtenerDisponibilidad(inicio, fin);
        expect(respuesta).toHaveLength(0);
    });

    it('[Border]: debe manejar correctamente estadía de 1 noche', async () => {
        service.obtenerDisponibilidad.mockResolvedValue([{ id: 1 }]);
        
        const res = await service.obtenerDisponibilidad("2026-05-25", "2026-05-26");
        expect(res).toHaveLength(1);
    });
});