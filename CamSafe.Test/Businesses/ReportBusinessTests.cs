using CamSafe.Business.Businesses;
using CamSafe.Entity.CustomExceptions;
using CamSafe.Entity.DTOs;
using CamSafe.Repository.Interfaces;
using Moq;

namespace CamSafe.Test.Businesses
{
    public class ReportBusinessTests
    {
        [Fact]
        public async void ShouldCreateReport()
        {
            // Arrange
            var repositoryMoq = new Mock<IReportRepository>();
            repositoryMoq.Setup(r => r.InsertReportAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(1);
            var business = new ReportBusiness(repositoryMoq.Object);
            var request = new ReportRequest("01/02/2024", "1");

            // Act
            var result = await business.CreateReportAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("1", result.CameraId);
            Assert.Equal("2024-02-01T00:00:00", result.OccurredAt);
        }

        [Fact]
        public async void ShouldFailInvalidDateTimeReport()
        {
            // Arrange
            var repositoryMoq = new Mock<IReportRepository>();
            repositoryMoq.Setup(r => r.InsertReportAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(1);
            var business = new ReportBusiness(repositoryMoq.Object);
            var request = new ReportRequest("99/02/2024", "1");

            // Act and Assert
            await Assert.ThrowsAsync<CustomException>(() => business.CreateReportAsync(request));
        }

        [Fact]
        public async void ShouldFailInvalidCameraIdReport()
        {
            // Arrange
            var repositoryMoq = new Mock<IReportRepository>();
            repositoryMoq.Setup(r => r.InsertReportAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(1);
            var business = new ReportBusiness(repositoryMoq.Object);
            var request = new ReportRequest("01/02/2024", "NotAnInt");

            // Act and Assert
            await Assert.ThrowsAsync<CustomException>(() => business.CreateReportAsync(request));
        }

        [Fact]
        public async void ShouldFilterReportsByRangeDate()
        {
            // Arrange
            var repositoryMoq = new Mock<IReportRepository>();
            var business = new ReportBusiness(repositoryMoq.Object);

            var request = new ReportFilterRequest("01/02/2024", "10/02/2024", null, null);

            // Act
            var result = await business.GetReportsWithFiltersAsync(request);

            // Assert
            repositoryMoq.Verify(r => r.FilterReportsByRangeDateAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            repositoryMoq.Verify(r => r.FilterReportsByCustomerIdAsync(It.IsAny<string>()), Times.Never);
            repositoryMoq.Verify(r => r.FilterReportsByDayAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async void ShouldFailFilterReportsByRangeDate()
        {
            // Arrange
            var repositoryMoq = new Mock<IReportRepository>();
            var business = new ReportBusiness(repositoryMoq.Object);

            var request = new ReportFilterRequest("11/02/2024", "10/02/2024", null, null);

            // Act
            await Assert.ThrowsAsync<CustomException>(() => business.GetReportsWithFiltersAsync(request));
        }

        [Fact]
        public async void ShouldFilterReportsByCustomerId()
        {
            // Arrange
            var repositoryMoq = new Mock<IReportRepository>();
            var business = new ReportBusiness(repositoryMoq.Object);

            var request = new ReportFilterRequest(null, null, "1", null);

            // Act
            var result = await business.GetReportsWithFiltersAsync(request);

            // Assert
            repositoryMoq.Verify(r => r.FilterReportsByRangeDateAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            repositoryMoq.Verify(r => r.FilterReportsByCustomerIdAsync(It.IsAny<string>()), Times.Once);
            repositoryMoq.Verify(r => r.FilterReportsByDayAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async void ShouldFailtFilterReportsByCustomerId()
        {
            // Arrange
            var repositoryMoq = new Mock<IReportRepository>();
            var business = new ReportBusiness(repositoryMoq.Object);

            var request = new ReportFilterRequest(null, null, "NotAnInt", null);

            // Act
            await Assert.ThrowsAsync<CustomException>(() => business.GetReportsWithFiltersAsync(request));
        }

        [Fact]
        public async void ShouldFilterReportsByDay()
        {
            // Arrange
            var repositoryMoq = new Mock<IReportRepository>();
            var business = new ReportBusiness(repositoryMoq.Object);

            var request = new ReportFilterRequest(null, null, null, "01/02/2024");

            // Act
            var result = await business.GetReportsWithFiltersAsync(request);

            // Assert
            repositoryMoq.Verify(r => r.FilterReportsByRangeDateAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            repositoryMoq.Verify(r => r.FilterReportsByCustomerIdAsync(It.IsAny<string>()), Times.Never);
            repositoryMoq.Verify(r => r.FilterReportsByDayAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async void ShouldFailFilterReportsByDay()
        {
            // Arrange
            var repositoryMoq = new Mock<IReportRepository>();
            var business = new ReportBusiness(repositoryMoq.Object);

            var request = new ReportFilterRequest(null, null, null, "99/02/2024");

            // Act
            await Assert.ThrowsAsync<CustomException>(() => business.GetReportsWithFiltersAsync(request));
        }

        [Fact]
        public async void ShouldFailWhenMoreThanOneFilterIsUsed()
        {
            // Arrange
            var repositoryMoq = new Mock<IReportRepository>();
            var business = new ReportBusiness(repositoryMoq.Object);

            var request = new ReportFilterRequest(null, null, "1", "01/02/2024");

            // Act and Assert
            await Assert.ThrowsAsync<CustomException>(() => business.GetReportsWithFiltersAsync(request));
        }
    }
}
