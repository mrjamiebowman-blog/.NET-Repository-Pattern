using MrJB.RepositoryPattern.Data.Configuration;
using MrJB.RepositoryPattern.Data.Services;
using MrJB.RepositoryPattern.Data.Tests.Repositories;
using System.Threading.Tasks;
using Xunit;

namespace MrJB.RepositoryPattern.Data.Tests;

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

        // database configuration
        var databaseConfig = new DatabaseConfiguration();

        // fake customer repo
        var fakeCustomerRepo = new FakeCustomersRepository();

        // create data service
        _dataService = new DataService(fakeCustomerRepo, databaseConfig);

        // act
        var customers = await _dataService.GetCustomersAsync();

        // assert
        Assert.True(customers.Count.Equals(3));
    }
}
