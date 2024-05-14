using CarFactoryLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactory_UnitTeating
{
    public class CarFactoryTests
    {
        #region Type Tests

        [Fact]
        public void NewCar_ReturnsBMWCar()
        {
            // Arrange
            CarTypes BMWCar = CarTypes.BMW;

            // Act
            Car result = CarFactory.NewCar(BMWCar);

            // Assert
            Assert.IsType<BMW>(result);
            Assert.IsNotType<Toyota>(result);
        }
        [Fact]
        public void NewCar_ReturnsNullForAudi()
        {
            // Arrange
            CarTypes AudiCar = CarTypes.Audi;

            // Act
            Car result = CarFactory.NewCar(AudiCar);

            // Assert
            Assert.Null(result);
        }
        #endregion Type Tests
        #region Exception Tests
        [Fact]
        public void NewCar_ThrowsNotImplementedException()
        {
            // Arrange
            CarTypes hondaCar = CarTypes.Honda;

            // Act & Assert
            Assert.Throws<NotImplementedException>
                (() => CarFactory.NewCar(hondaCar));
        }
        #endregion Exception Tests
    }
}
