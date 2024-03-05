using API.Controllers;
using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Application.UnitTests;

public class CustomerServiceTests
{
    [Fact]
    public async Task GetAllAsync_ReturnsAllCustomers()
    {
        // Arrange
        var mockRepo = new Mock<ICustomerRepository>();
        mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(GetTestCustomers());
        var service = new CustomerService(mockRepo.Object);

        // Act
        var result = await service.GetAllAsync();

        // Assert
        var customers = Assert.IsAssignableFrom<IEnumerable<CustomerDto>>(result);
        Assert.Equal(2, customers.Count()); 
    }

    public static List<Customer> GetTestCustomers()
    {
        var customers = new List<Customer>
        {
            Customer.Create(
                Guid.NewGuid(),
                new Name("John Doe"),
                new Dni("12345678A"),
                new Address("Main St", "State", "City"),
                new ContactInfo("123456789", "987654321", "john.doe@example.com")
            ),
            Customer.Create(
                Guid.NewGuid(),
                new Name("Jane Doe"),
                new Dni("87654321B"),
                new Address("Second St", "AnotherState", "AnotherCity"),
                new ContactInfo("987654321", "123456789", "jane.doe@example.com")
            )
        };

        return customers;
    }

    [Fact]
    public async Task GetByIdAsync_WithExistingId_ReturnsCorrectCustomer()
    {
        // Arrange
        var mockRepo = new Mock<ICustomerRepository>();
        var testId = Guid.NewGuid();
        var testCustomer = Customer.Create(testId, 
            new Name("Test Name"), 
            new Dni("Test Dni"), 
            new Address("Test Street", "Test City", "Test State"), 
            new ContactInfo("Test Phone", "Test Mobile", "test@example.com"));
        mockRepo.Setup(repo => repo.GetByIdAsync(testId)).ReturnsAsync(testCustomer);
        var service = new CustomerService(mockRepo.Object);

        // Act
        var result = await service.GetByIdAsync(testId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(testId, result.Id);
    }

    [Fact]
    public async Task CreateAsync_ThrowsException_IfRepositoryFails()
    {
        // Arrange
        var mockRepo = new Mock<ICustomerRepository>();
        var customerDto = new CustomerDto(Guid.NewGuid(), 
            new NameDto("Test"), 
            new DniDto("Test Dni"), 
            new AddressDto("Test Street", "Test City", "Test State"), 
            new ContactInfoDto("Test Phone", "Test Mobile", "test@example.com"));
        mockRepo.Setup(repo => repo.AddAsync(It.IsAny<Customer>())).ThrowsAsync(new Exception("Repository failure")); 
        var service = new CustomerService(mockRepo.Object);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => service.CreateAsync(customerDto));
    }

    [Fact]
    public async Task Create_ReturnsCreatedAtAction_WithValidData()
    {
        // Arrange
        var mockService = new Mock<ICustomerService>();
        var customerDto = new CustomerDto(Guid.NewGuid(), 
            new NameDto("Valid Name"), 
            new DniDto("Valid Dni"), 
            new AddressDto("Valid Street", "Valid City", "Valid State"), 
            new ContactInfoDto("Valid Phone", "Valid Mobile", "valid@example.com"));
        mockService.Setup(service => service.CreateAsync(customerDto)).ReturnsAsync(customerDto);
        var controller = new CustomersController(mockService.Object);

        // Act
        var result = await controller.Create(customerDto);

        // Assert
        var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal("Get", actionResult.ActionName);
        Assert.Equal(customerDto.Id, ((CustomerDto)actionResult.Value).Id);
    }

    [Fact]
    public async Task Update_ReturnsBadRequest_WhenIdMismatch()
    {
        // Arrange
        var mockService = new Mock<ICustomerService>();
        var id = Guid.NewGuid();
        var customerDto = 
            new CustomerDto(id, new NameDto("Name"), 
                new DniDto("Dni"), 
                new AddressDto("Street", "City", "State"), 
                new ContactInfoDto("Phone", "Mobile", "email@example.com"));
        var controller = new CustomersController(mockService.Object);

        // Act ID mismatch
        var result = await controller.Update(Guid.NewGuid(), customerDto); 

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }


}