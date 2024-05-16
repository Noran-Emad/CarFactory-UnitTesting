using CarAPI.Entities;
using CarAPI.Models;
using CarAPI.Payment;
using CarAPI.Repositories_DAL;
using CarAPI.Services_BLL;
using CarFactoryAPI.Entities;
using CarFactoryAPI.Repositories_DAL;
using CarFactoryAPI_Tests.Stups;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace CarFactoryAPI_tests
{
    public class OwnersServiceTests : IDisposable
    {
        private readonly ITestOutputHelper testOutputHelper;

        Mock<ICarsRepository> carRepoMock;
        Mock<IOwnersRepository> OwnerRepoMock;
        Mock<ICashService> cashMock;

        OwnersService ownersService;

        public OwnersServiceTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;

            // test setup
            testOutputHelper.WriteLine("Test Set Up");

            carRepoMock = new();
            OwnerRepoMock = new();
            cashMock = new();

            ownersService =new(carRepoMock.Object, OwnerRepoMock.Object, cashMock.Object);

        }
        public void Dispose()
        {
            // test clean up
            testOutputHelper.WriteLine("Test Clean Up");
        }
        #region Skiped Test
        [Fact(Skip = "Fail due to fail in dependencies working on isolating Unit")]
        [Trait("Author", "Rehab")]
        [Trait("Priorty", "2")]
        public void BuyCar_CarNotExist_NotExist()
        {
            testOutputHelper.WriteLine("Test1 -BuyCar_CarNotExist_NotExist-");
            
            // Arrange
            OwnersService ownersService = new OwnersService(new CarRepoStup(), new OwnerRepoStup(), new CashServiceStup());

            BuyCarInput buyCarInput = new() { CarId = 10, OwnerId = 1, Amount = 5000 };

            // Act
            string result = ownersService.BuyCar(buyCarInput);

            // Assert
            Assert.Contains("n't exist", result);
        }
        #endregion

        [Fact]
        [Trait("Author", "Rehab")]
        [Trait("Priorty", "4")]
        public void BuyCar_CarwithOwner_Sold()
        {
            testOutputHelper.WriteLine("Test2 -BuyCar_CarwithOwner_Sold-");

            // Build the mock Data
            Car car = new Car() { Id = 10, Price = 1000, OwnerId = 15, Owner = new Owner() };

            // Setup the called methods
            carRepoMock.Setup(o => o.GetCarById(10)).Returns(car);

            BuyCarInput carInput = new() { CarId = 10, OwnerId = 10, Amount = 1000 };

            // Act
            string result = ownersService.BuyCar(carInput);

            // Assert
            Assert.Contains("sold", result);
        }


        [Fact]
        [Trait("Author", "Rehab")]
        [Trait("Priorty", "4")]
        public void BuyCar_OwnerNotExist_NotExist()
        {
            testOutputHelper.WriteLine("Test 3");

            // Arrange

            // Build the mock Data
            Car car = new Car() { Id = 10, Price = 1000 };
            Owner owner = null;

            // Setup called methods
            carRepoMock.Setup(o => o.GetCarById(10)).Returns(car);
            OwnerRepoMock.Setup(o => o.GetOwnerById(100)).Returns(owner);

            BuyCarInput carInput = new() { CarId = 10, OwnerId = 100, Amount = 1000 };

            // Act
            string result = ownersService.BuyCar(carInput);

            // Assert
            Assert.Contains("n't exist", result);
        }
        [Fact]
        [Trait("Author", "Noran")]
        [Trait("Priorty", "2")]
        public void BuyCar_OwnedCar_Alreadyhavecar()
        {
            testOutputHelper.WriteLine("Test4 -BuyCar_OwnedCar_Alreadyhavecar-");

            // Build the mock Data
            Car car = new Car() { Id = 10, Price = 1000 };
            Owner owner = new Owner() { Id = 15, Car = new Car() };

            // Setup called methods
            carRepoMock.Setup(o => o.GetCarById(10)).Returns(car);
            OwnerRepoMock.Setup(o => o.GetOwnerById(15)).Returns(owner);

            BuyCarInput carInput = new() { CarId = 10, OwnerId = 15, Amount = 1000 };

            // Act
            string result = ownersService.BuyCar(carInput);

            // Assert
            Assert.Contains("Already have car", result);
        }

        [Fact]
        [Trait("Author", "Noran")]
        [Trait("Priorty", "5")]
        public void BuyCar_CheckPrice_Insufficient()
        {
            testOutputHelper.WriteLine("Test5 -BuyCar_CheckPrice_Insufficient-");

            // Build the mock Data
            Car car = new Car() { Id = 10, Price = 20000 };
            Owner owner = new Owner() { Id = 15 };

            // Setup called methods
            carRepoMock.Setup(o => o.GetCarById(10)).Returns(car);
            OwnerRepoMock.Setup(o => o.GetOwnerById(15)).Returns(owner);

            BuyCarInput carInput = new() { CarId = 10, OwnerId = 15, Amount = 1000 };

            // Act
            string result = ownersService.BuyCar(carInput);

            // Assert
            Assert.Contains("Insufficient funds", result);
        }


        [Fact]
        [Trait("Author", "Noran")]
        [Trait("Priorty", "5")]

        public void BuyCar_AssigntoOwner_SomethingWrong()
        {
            testOutputHelper.WriteLine("Test6 -BuyCar_AssigntoOwner_SomethingWrong-");

            // Arrange

            // Build the mock Data
            Car car = new Car() { Id = 10, Price = 10000 };
            Owner owner = new Owner() { Id = 13 };

            // Setup called methods
            carRepoMock.Setup(o => o.GetCarById(10)).Returns(car);
            OwnerRepoMock.Setup(o => o.GetOwnerById(13)).Returns(owner);
            BuyCarInput carInput = new() { CarId = 10, OwnerId = 13, Amount = 100000 };

            // Act
            string result = ownersService.BuyCar(carInput);

            // Assert
            Assert.Contains("Something went wrong", result);
        }


        [Fact]
        [Trait("Author", "Noran")]
        [Trait("Priorty", "1")]

        public void BuyCar_AssigntoOwner_Successful()
        {
            testOutputHelper.WriteLine("Test7 -BuyCar_AssigntoOwner_SomethingWrong-");

            // Build the mock Data
            Car car = new Car() { Id = 10, Price = 10000, OwnerId = 11 };
            Owner owner = new Owner() { Id = 11 };

            // Setup called methods
            carRepoMock.Setup(o => o.GetCarById(10)).Returns(car);
            OwnerRepoMock.Setup(o => o.GetOwnerById(11)).Returns(owner);
            carRepoMock.Setup(o => o.AssignToOwner(10, 11)).Returns(true);

            BuyCarInput carInput = new() { CarId = 10, OwnerId = 11, Amount = 10000 };

            // Act
            string result = ownersService.BuyCar(carInput);

            // Assert
            Assert.Contains("Successfull", result);
        }

    }
}
