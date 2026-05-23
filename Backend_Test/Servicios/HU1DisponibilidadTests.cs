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

        [Test]
        public void Consultar_Disponibilidad_UnDia_DevolverListaConDato()
        {
            var hoy = DateTime.Today;
            var manana = hoy.AddDays(1);

            var Lista = new List<HabitacionDisponibleDTO>
    {
        new HabitacionDisponibleDTO { Id = 1, Tipo_Nombre = "Standard", Precio_Noche = 100 }
    };

            repositorio.Setup(r => r.Obtener_Habitaciones(hoy, manana)).Returns(Lista);

            var resultado = servicios.Consultar_Disponibilidad(hoy, manana);

            Assert.That(resultado, Is.Not.Empty);


            repositorio.Verify(r => r.Obtener_Habitaciones(hoy, manana), Times.Once);
        }
    }
}