using CamSafe.Business.Businesses;
using CamSafe.Entity.CustomExceptions;
using CamSafe.Entity.DTOs;
using CamSafe.Entity.Entities;
using CamSafe.Repository.Interfaces;
using Moq;

namespace CamSafe.Test.Businesses
{
    public class CameraBusinessTests
    {
        [Fact]
        public async void ShouldCreateCamera()
        {
            // Arrange
            var repositoryMoq = new Mock<ICameraRepository>();
            repositoryMoq.Setup(r => r.CustomerHasGivenIpAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);
            repositoryMoq.Setup(r => r.InsertCameraAsync(It.IsAny<Camera>())).ReturnsAsync(1);

            var business = new CameraBusiness(repositoryMoq.Object);
            var request = new CameraRequest("Camera1", "1.1.1.1", "true", "1");

            // Act
            var result = await business.CreateCameraAsync(request);

            // Assert
            Assert.Equal(1, result.Id);
            Assert.Equal("Camera1", result.Name);
            Assert.Equal("1.1.1.1", result.Ip);
            Assert.Equal("true", result.IsEnabled);
            Assert.Equal("1", result.CustomerId);
        }

        [Fact]
        public async void ShouldFailBooleanCreateCamera()
        {
            // Arrange
            var repositoryMoq = new Mock<ICameraRepository>();
            repositoryMoq.Setup(r => r.CustomerHasGivenIpAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);
            repositoryMoq.Setup(r => r.InsertCameraAsync(It.IsAny<Camera>())).ReturnsAsync(1);

            var business = new CameraBusiness(repositoryMoq.Object);
            var request = new CameraRequest("Camera1", "1.1.1.1", "verdadeiro", "1");

            // Act and Assert
            await Assert.ThrowsAsync<CustomException>(() => business.CreateCameraAsync(request));
        }

        [Fact]
        public async void ShouldFailIpv4CreateCamera()
        {
            // Arrange
            var repositoryMoq = new Mock<ICameraRepository>();
            repositoryMoq.Setup(r => r.CustomerHasGivenIpAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);
            repositoryMoq.Setup(r => r.InsertCameraAsync(It.IsAny<Camera>())).ReturnsAsync(1);

            var business = new CameraBusiness(repositoryMoq.Object);
            var request = new CameraRequest("Camera1", "256.257.258.259", "true", "1");

            // Act and Assert
            await Assert.ThrowsAsync<CustomException>(() => business.CreateCameraAsync(request));
        }

        [Fact]
        public async void ShouldFailWhenCustomerHaveGivenIpWhenCreatingNewCamera()
        {
            // Arrange
            var repositoryMoq = new Mock<ICameraRepository>();
            repositoryMoq.Setup(r => r.CustomerHasGivenIpAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);
            repositoryMoq.Setup(r => r.InsertCameraAsync(It.IsAny<Camera>())).ReturnsAsync(1);

            var business = new CameraBusiness(repositoryMoq.Object);
            var request = new CameraRequest("Camera1", "1.1.1.1", "true", "1");

            // Act and Assert
            await Assert.ThrowsAsync<CustomException>(() => business.CreateCameraAsync(request));
        }

        [Fact]
        public async void ShouldFailGetAllCamerasByCustomerId()
        {
            // Arrange
            var repositoryMoq = new Mock<ICameraRepository>();
            repositoryMoq.Setup(r => r.InsertCameraAsync(It.IsAny<Camera>())).ReturnsAsync(1);

            var business = new CameraBusiness(repositoryMoq.Object);

            // Act and Assert
            await Assert.ThrowsAsync<CustomException>(() => business.GetAllCamerasByCustomerIdAsync("NotAnInt"));
        }

        [Fact]
        public async void ShouldFailGetAllCamerasByState()
        {
            // Arrange
            var repositoryMoq = new Mock<ICameraRepository>();
            repositoryMoq.Setup(r => r.InsertCameraAsync(It.IsAny<Camera>())).ReturnsAsync(1);

            var business = new CameraBusiness(repositoryMoq.Object);

            // Act and Assert
            await Assert.ThrowsAsync<CustomException>(() => business.GetAllCamerasByStateAsync("verdadeiro"));
        }

        [Fact]
        public async void ShouldFailActivateCamera()
        {
            // Arrange
            var repositoryMoq = new Mock<ICameraRepository>();
            repositoryMoq.Setup(r => r.InsertCameraAsync(It.IsAny<Camera>())).ReturnsAsync(1);

            var business = new CameraBusiness(repositoryMoq.Object);

            // Act and Assert
            await Assert.ThrowsAsync<CustomException>(() => business.ActivateCameraAsync("NotAnInt"));
        }

        [Fact]
        public async void ShouldFailDeactivateCamera()
        {
            // Arrange
            var repositoryMoq = new Mock<ICameraRepository>();
            repositoryMoq.Setup(r => r.InsertCameraAsync(It.IsAny<Camera>())).ReturnsAsync(1);

            var business = new CameraBusiness(repositoryMoq.Object);

            // Act and Assert
            await Assert.ThrowsAsync<CustomException>(() => business.DeactivateCameraAsync("NotAnInt"));
        }
    }
}
