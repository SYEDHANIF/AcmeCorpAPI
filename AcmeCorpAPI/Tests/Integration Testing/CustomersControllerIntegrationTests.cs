
using AcmeCorpAPI.Entities;
using Newtonsoft.Json;
using NUnit.Framework;
using Xunit;

namespace AcmeCorpAPI.Tests.Integration_Testing
{
    public class CustomersControllerIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public CustomersControllerIntegrationTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Test]
        public async Task GetAllCustomers_ShouldReturnAllCustomers()
        {
            // Arrange
            var client = _factory.CreateClient();
            var apiKey = "your-api-key";
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            // Act
            var response = await client.GetAsync("/api/customers");
            var content = await response.Content.ReadAsStringAsync();
            var customers = JsonConvert.DeserializeObject<IEnumerable<Customer>>(content);

            // Assert
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.AreEqual(2, customers.Count());
        }

        // Write other integration tests
    }

}
