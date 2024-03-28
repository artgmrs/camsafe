using CamSafe.Business.Businesses;
using CamSafe.Entity.CustomExceptions;
using CamSafe.Entity.DTOs;
using CamSafe.Repository.Interfaces;
using Moq;

namespace CamSafe.Test.Businesses
{
    public class CustomerBusinessTests
    {
        [Fact]
        public async void ShouldCreateCustomer()
        {
            // Arrange
            var repositoryMoq = new Mock<ICustomerRepository>();
            repositoryMoq.Setup(r => r.InsertCustomerAsync(It.IsAny<CustomerRequest>())).ReturnsAsync(1);
            var business = new CustomerBusiness(repositoryMoq.Object);

            // Act
            var result = await business.CreateCustomerAsync(new CustomerRequest("Arthur"));

            // Assert
            Assert.Equal(1, result.Id);
            Assert.NotNull(result.Name);
        }

        [Fact]
        public async void ShouldFailWithoutName()
        {
            // Arrange
            var repository = new Mock<ICustomerRepository>();
            repository.Setup(r => r.InsertCustomerAsync(It.IsAny<CustomerRequest>())).ReturnsAsync(1);
            var business = new CustomerBusiness(repository.Object);

            // Act and Assert
            await Assert.ThrowsAsync<CustomException>(() => business.CreateCustomerAsync(new CustomerRequest("")));
        }
    }
}
