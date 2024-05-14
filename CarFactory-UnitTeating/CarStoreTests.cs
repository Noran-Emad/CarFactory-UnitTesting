using CarFactoryLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactory_UnitTeating
{
    public class CarStoreTests
    {
        #region Collection Tests
        [Fact]
        public void AddCar_AddsCarToList()
        {
            // Arrange
            CarStore carStore = new CarStore();
            Car toyota = new Toyota();

            // Act
            carStore.AddCar(toyota);

            // Assert
            Assert.Contains(toyota, carStore.cars);
            Assert.NotEmpty(carStore.cars);

        }
        [Fact]
        public void AddCars_AddsCarsCollectionToList()
        {
            // Arrange
            CarStore carStore = new CarStore();
            List<Car> carsCollection = new List<Car> { new Toyota(), new BMW() };

            // Act
            carStore.AddCars(carsCollection);

            // Assert
            Assert.All(carsCollection,c => Assert.Contains(c,carStore.cars));
            
        }

        #endregion
    }
}
