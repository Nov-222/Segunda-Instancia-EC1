using NUnit.Framework;
using Moq;
using Backend.Servicios;
using Backend.Repositorios;
using Backend.DTOs;

namespace Backend_Test.Servicios
{
    [TestFixture]
    public class RefactorTests
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
        public void Calcular_Costo_6Nocher1000PorNoche_DebeDevolver6000()
        {
            //Arrange
            int Noches_Totales = 6;
            int Precio_Por_Noche = 1000;



            //Act
            int resultado = servicio.Calcular_Costo(Noches_Totales, Precio_Por_Noche);

            //Assert
            Assert.That( resultado , Is.EqualTo(6000) );
        }
    }
}