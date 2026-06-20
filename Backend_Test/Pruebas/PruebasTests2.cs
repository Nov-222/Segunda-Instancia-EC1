using NUnit.Framework;
using Moq;
using Backend.Servicios;
using Backend.Repositorios;
using Backend.DTOs;

namespace Backend_Test.Pruebas
{
    [TestFixture]
    public class PruebasTests2
    {
        private Mock<IReservarEstadia> repositorio;
        private ReservarServicio servicio;

        [SetUp]
        public void Setup()
        {
            repositorio = new Mock<IReservarEstadia>();
            servicio = new ReservarServicio(repositorio.Object);
        }

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
    }
}