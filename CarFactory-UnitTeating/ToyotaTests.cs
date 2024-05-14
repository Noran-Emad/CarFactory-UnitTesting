using CarFactoryLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactory_UnitTeating
{
    public class ToyotaTests
    {
        #region Boolean Tests
        [Fact]
        public void IsStopped_velocity0_true()
        {
            // Arrange
            Toyota toyota = new Toyota();
            toyota.velocity = 0;

            // Act
            bool actualResult = toyota.IsStopped();

            // Boolean Assert
            Assert.True(actualResult);
        }
        [Fact]
        public void IsStopped_velocitynot0_false()
        {
            // Arrange
            Toyota toyota = new Toyota();
            toyota.velocity = 50;

            // Act
            bool actualResult = toyota.IsStopped();

            // Boolean Assert
            Assert.False(actualResult);
        }
        #endregion

        #region Numeric Tests
        [Fact]
        public void IncreaseVelocity_IntialVelocity20_AddedVelocity20_Velocity40()
        {
            // Arrange
            Toyota toyota = new Toyota() { velocity = 20 };
            double addedVelocity = 20;

            // Act
            toyota.IncreaseVelocity(addedVelocity);

            // Assert
            Assert.Equal(40, toyota.velocity);
        }
        [Theory]
        [InlineData(20, 20, 40)]
        public void IncreaseVelocity_useTestData_useTestResult
            (double intialVelocity, double AddedVelocity, double ExpectedResult)
        {
            // Arrange
            Toyota toyota = new Toyota() { velocity = intialVelocity };

            // Act
            toyota.IncreaseVelocity(AddedVelocity);

            // Equality Assert
            Assert.Equal(ExpectedResult, toyota.velocity);
            //Assert.NotEqual(40, toyota.velocity);
        }
        #endregion

        #region Reference Tests
        [Fact]
        public void GetMyCar_ReturnsReference_Same()
        {
            // Arrange
            Toyota toyota1 = new Toyota();
            Toyota toyota2 = new Toyota();

            // Act
            Car result1 = toyota1.GetMyCar();
            Car result2 = toyota2.GetMyCar();

            // Assert
            Assert.Same(toyota1, result1);
        }
        [Fact]
        public void GetMyCar_ReturnsReference_NotSame()
        {
            // Arrange
            Toyota toyota1 = new Toyota();
            Toyota toyota2 = new Toyota();

            // Act
            Car result1 = toyota1.GetMyCar();
            Car result2 = toyota2.GetMyCar();

            // Assert
            Assert.NotSame(toyota1, toyota2);
            Assert.NotSame(result1, result2);
        }
        #endregion Reference Tests

        #region Sting Tests
        [Fact]
        public void GetDirection_DirectionBackward_Backward()
        {
            // Arrange
            Toyota toyota = new Toyota() { drivingMode = DrivingMode.Backward };

            // Act
            string result = toyota.GetDirection();

            // String Assert
            Assert.Equal("Backward", result);
            Assert.StartsWith("Back", result);
            Assert.Contains("war", result);
        }
        [Fact]
        public void GetDirection_DirectionForward_Forward()
        {
            // Arrange
            Toyota toyota = new Toyota() { drivingMode = DrivingMode.Forward };

            // Act
            string result = toyota.GetDirection();

            // String Assert
            Assert.Equal("Forward", result);
            Assert.EndsWith("ward", result);
            Assert.DoesNotMatch("F[A-Z][a-z]{1}", result);
        }
        [Fact]
        public void GetDirection_DirectionStopped_Stopped()
        {
            // Arrange
            Toyota toyota = new Toyota() { drivingMode = DrivingMode.Stopped };

            // Act
            string result = toyota.GetDirection();

            // String Assert
            Assert.Equal("Stopped", result);
            Assert.EndsWith("ed", result);
            Assert.DoesNotMatch("S[a-z]{8}", result);
        }
        #endregion

    }
}
