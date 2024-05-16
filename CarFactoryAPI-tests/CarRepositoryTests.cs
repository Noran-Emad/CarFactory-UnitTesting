using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using Moq.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Runtime.ConstrainedExecution;
using CarFactoryAPI.Entities;
using CarFactoryAPI.Repositories_DAL;
using CarAPI.Entities;
using Xunit.Abstractions;

namespace CarFactoryAPI_tests
{
    public class CarRepositoryTests :IDisposable
    {
        private readonly ITestOutputHelper testOutputHelper;

        // Create Mock of Dependencies
        Mock<FactoryContext> contextMock;

        // use fake object as dependency
        CarRepository carRepository;
        public CarRepositoryTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;

            // test setup
            testOutputHelper.WriteLine("Test Set Up");

            // Create Mock of Dependencies
            contextMock = new();

            // use fake object as dependency
            carRepository = new(contextMock.Object);
        }
        public void Dispose()
        {
            // test clean up
            testOutputHelper.WriteLine("Test Clean Up");
        }

        [Fact]
        [Trait("Author","Noran")]
        [Trait("Priorty", "3")]
        public void GetCarById_AskForCarId20_CarObject()
        {
            testOutputHelper.WriteLine("Test1 -GetCarById_AskForCarId20_CarObject-");
            // Build the mock data
            List<Car> cars = new List<Car>() {
                new Car() { Id = 10 },
                new Car() { Id = 20 },
                new Car() { Id = 30 }
            };

            // setup called Dbsets
            contextMock.Setup(c => c.Cars).ReturnsDbSet(cars);

            // Act
            Car result = carRepository.GetCarById(20);

            // Assert
            Assert.NotNull(result);
        }

    }
}