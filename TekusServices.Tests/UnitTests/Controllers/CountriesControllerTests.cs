using Microsoft.AspNetCore.Mvc;
using Moq;
using TekusServices.API.Controllers;
using TekusServices.Domain.Entities;
using TekusServices.Domain.Interfaces;

namespace TekusServices.Tests.UnitTests.Controllers
{
    public class CountriesControllerTests
    {
        private Mock<ICountryService> _countryServiceMock = null!;
        private CountriesController _controller = null!;

        [SetUp]
        public void Setup()
        {
            _countryServiceMock = new Mock<ICountryService>();
            _controller = new CountriesController(_countryServiceMock.Object);
        }

        [Test]
        public async Task GetAll_ReturnsOkWithCountries()
        {
            // Arrange
            var data = new List<Country> { new Country { Code = "CO", Name = "Colombia" } };
            _countryServiceMock.Setup(s => s.GetAllCountriesAsync()).ReturnsAsync(data);

            // Act
            var result = await _controller.GetAll();

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var ok = result as OkObjectResult;
            Assert.That(data, Is.EqualTo(ok!.Value));
        }
    }
}