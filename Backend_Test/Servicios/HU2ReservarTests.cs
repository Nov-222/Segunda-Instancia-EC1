using NUnit.Framework;
using Moq;
using Backend.Servicios;
using Backend.Repositorios;
using Backend.DTOs;

namespace Backend_Test.Servicios
{
    [TestFixture]
    public class HU2ReservarTests
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
        public void Confirmar_Reserva_DatosValidos_GuardarEstadia()
        {
            var datos = new ReservarEstadiaDTO
            {
                Id_Habitacion = 1,
                Fecha_Inicio = DateTime.Today,
                Fecha_Finalizacion = DateTime.Today.AddDays(2),
                Documentos_Huespedes = new List<string> { "123" }
            };

            var lista = new List<HabitacionDisponibleDTO> { new HabitacionDisponibleDTO { Id = 1, Precio_Noche = 100 }};

            repositorio.Setup(r => r.Obtener_Habitaciones(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(lista);

            repositorio.Setup(r => r.Guardar_Estadia(It.IsAny<ReservarEstadiaDTO>(), It.IsAny<int>())).Returns(1);

            var resultado = servicio.Confirmar_Reserva(datos);

            Assert.That(resultado, Is.True);
            
            
            repositorio.Verify(r => r.Registrar_Estadia(1, "123"), Times.Once);
        }
    }
}