using AcmeCorpAPI.Entities;
using AcmeCorpAPI.EntitiesData;
using AcmeCorpAPI.Interface;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace AcmeCorpAPI.Tests.Unit_Testing
{
    [TestFixture]
    public class CustomerRepository
    {
        private ICustomerRepository _customerRepository;
        private Mock<CustomerDbContext> _dbContextMock;

        public object MockHelper { get; private set; }

        [SetUp]
        public void Setup()
        {
            _dbContextMock = new Mock<CustomerDbContext>(new DbContextOptions<CustomerDbContext>());
            _customerRepository = new CustomerRepository(_dbContextMock.Object);
        }

        [Test]
        public async Task GetAllCustomersAsync_ShouldReturnAllCustomers()
        {
            // Arrange
            var customers = new List<Customer>
        {
            new Customer { Id = 1, Name = "Customer 1" },
            new Customer { Id = 2, Name = "Customer 2" }
        };

            _dbContextMock.Setup(db => db.Customers)
                .Returns(MockHelper.CreateDbSetMock(customers).Object);

            // Act
            var result = await _customerRepository.GetAllCustomersAsync();

            // Assert
            Assert.AreEqual(customers.Count, result.Count());
            Assert.AreEqual(customers[0].Name, result.First().Name);
            Assert.AreEqual(customers[1].Name, result.Skip(1).First().Name);
        }

    }
}
