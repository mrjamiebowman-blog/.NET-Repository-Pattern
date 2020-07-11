using RepositoryPattern.Data.Repositories;
using System.Threading.Tasks;
using KafkaModels.Models.Customer;
using Xunit;

namespace RepositoryPattern.Data.Tests.Live
{
    public class SqlCustomersRepoTests
    {
        public SqlCustomersRepoTests()
        {

        }

        //[Fact]
        public async Task GetCustomersAsyncTests()
        {
            // arrange
            var _customerRepo = new SqlCustomersRepository();

            // fyi: you may need to update this id
            int id = 1;

            // act
            var customer = await _customerRepo.GetByIdAsync(id);
        }

        //[Fact]
        public async Task DeleteCustomerAsyncTests()
        {
            // arrange
            var _customerRepo = new SqlCustomersRepository();
            
            // fyi
            int id = 1002;

            await _customerRepo.DeleteByIdAsync(id);
        }

        [Fact]
        public async Task SaveAsync()
        {
            // arrange
            var _customerRepo = new SqlCustomersRepository();

            var model = new Customer();
            model.CustomerId = 1003;
            model.FirstName = "Jamie";
            model.LastName = "Bowman";
            model.Email = "tes2t@test.com";

            // act
            model = await _customerRepo.SaveAsync(model);
        }
    }
}
