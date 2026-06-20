using NUnit.Framework;
using Moq;
using Backend.Servicios;
using Backend.Repositorios;
using Backend.DTOs;

namespace Backend_Test.Pruebas
{
    [TestFixture]
    public class PruebasTests
    {
        private Mock<IConsultaReservas> repositorio;
        private ConsultaServicio servicio;

        [SetUp]
        public void Setup()
        {
            repositorio = new Mock<IConsultaReservas> ();
            servicio = new ConsultaServicio(repositorio.Object);
        }

        [Test]
        public void Listar_Reservas_Admin_ExistenReservas_Exito()
        {
            var reservas = new List<VisualizacionDTO>
            {
                new VisualizacionDTO
                {
                    Id =1,
                    Fecha_Inicio = new DateTime(2026,6,20),
                    Fecha_Finalizacion = new DateTime(2026,6,27),
                    Estado = "Reservada",
                    Nro_Habitacion = 10,
                    Precio_Total = 1500,
                    Nombre_Cliente = "Jose Enrique DIaz Velarde"
                },
                new VisualizacionDTO
                {
                    Id = 2,
                    Fecha_Inicio = new DateTime(2026,6,15),
                    Fecha_Finalizacion = new DateTime(2026,6,22),
                    Estado = "Activa",
                    Nro_Habitacion = 13,
                    Precio_Total = 2000,
                    Nombre_Cliente = "Maria Belen Zurita Cardenas"
                }
            };
            repositorio.Setup(f => f.Obtener_Reservas()).Returns(reservas);


            var resultado = servicio.Listar_Reservas_Admin();

            Assert.That(resultado, Is.Not.Empty);
        }

        [Test]
        public void Procesar_CheckIn_ReservaValida_Exito()
        {
            int IdValido = 20;
            repositorio.Setup(f => f.Registrar_CheckIn(IdValido)).Returns(true);

            var resultado = servicio.Procesar_CheckIn(IdValido);

            Assert.That(resultado, Is.EqualTo(true));
        }

        [Test]
        public void Procesar_CheckOut_ReservaValida_Exito()
        {
            int IdValido = 20;
            repositorio.Setup(f => f.Registrar_CheckOut(IdValido)).Returns(true);

            var resultado = servicio.Procesar_CheckOut(IdValido);

            Assert.That(resultado, Is.EqualTo(true));
        }
    }
}