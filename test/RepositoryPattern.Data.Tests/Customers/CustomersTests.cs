using RepositoryPattern.Data.Services;
using RepositoryPattern.Data.Tests.Repositories;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Xunit;
using Moq;

namespace RepositoryPattern.Data.Tests
{
    public class CustomersTests
    {
        private IConfiguration _connfiguration;
        private IDataService _dataService;

        public CustomersTests()
        {

        }

        [Fact]
        public async Task GetCustomersAsyncTests()
        {
            // arrange
            //_connfiguration = new Mock<IConfiguration>();

            var fakeCustomerRepo = new FakeCustomersRepository();
            _dataService = new DataService(fakeCustomerRepo);

            // act
            var customers = await _dataService.GetCustomersAsync();

            // assert
            Assert.True(customers.Count.Equals(3));
        }
    }
}
