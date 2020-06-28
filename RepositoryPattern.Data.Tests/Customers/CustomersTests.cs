using RepositoryPattern.Data.Services;
using RepositoryPattern.Data.Tests.Repositories;
using System.Threading.Tasks;
using Xunit;

namespace RepositoryPattern.Data.Tests
{
    public class CustomersTests
    {
        private IDataService _dataService;

        public CustomersTests()
        {

        }

        [Fact]
        public async Task GetCustomersAsyncTests()
        {
            // arrange
            var fakeCustomerRepo = new FakeCustomersRepository();
            _dataService = new DataService(fakeCustomerRepo);

            // act
            var customers = await _dataService.GetCustomersAsync();

            // assert
            Assert.True(customers.Count.Equals(3));
        }
    }
}
