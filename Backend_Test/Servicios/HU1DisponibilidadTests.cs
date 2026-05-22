using NUnit.Framework;
using Moq;
using Backend.Servicios;
using Backend.Repositorios;
using Backend.DTOs;

namespace Backend_Test.Servicios
{
    [TestFixture]
    public class HU1DisponibilidadTests
    {
        private Mock<IReservarEstadia> repositorio;
        private ReservarServicio servicios;

        [SetUp]
        public void Setup()
        {
            repositorio = new Mock<IReservarEstadia>();

            servicios = new ReservarServicio(repositorio.Object);
        }

        [Test]
        public void Obtener_Habitaciones_ExistenHabitacionesDisponibles_DevolverLista()
        {
            var lista = new List<HabitacionDisponibleDTO>
            {
                new HabitacionDisponibleDTO
                {
                    Id = 5,
                    Tipo_Nombre = "Matrimonial",
                    Precio_Noche = 500
                }
            };

            repositorio.Setup(f => f.Obtener_Habitaciones(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(lista);


            var Fecha_Inicio = DateTime.Now;
            var Fecha_Fin = DateTime.Now.AddDays(2);
            var resultado = servicios.Consultar_Disponibilidad(Fecha_Inicio, Fecha_Fin);

            Assert.That(resultado, Is.Not.Empty);
        }

        [Test]
        public void Obtener_Habitaciones_NoExistenHabitacionesDisponibles_DevolverListaVacia()
        {
            var lista = new List<HabitacionDisponibleDTO> { };

            repositorio.Setup(f => f.Obtener_Habitaciones(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(lista);

            var Fecha_Inicio = DateTime.Now;
            var Fecha_Fin = DateTime.Now.AddDays(2);
            var resultado = servicios.Consultar_Disponibilidad(Fecha_Inicio, Fecha_Fin);

            Assert.That(resultado, Is.Empty);
        }

        [Test]
        public void Obtener_Habitaciones_FechaFinEsMenorFechaInicio_DevolverListaVacia()
        {
            var Fecha_Inicio = DateTime.Now.AddDays(3);
            var Fecha_Fin = DateTime.Now.AddDays(2);

            var resultado = servicios.Consultar_Disponibilidad(Fecha_Inicio, Fecha_Fin);

            Assert.That(resultado, Is.Empty);

            repositorio.Verify(f => f.Obtener_Habitaciones(It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Never);
        }
    }
}