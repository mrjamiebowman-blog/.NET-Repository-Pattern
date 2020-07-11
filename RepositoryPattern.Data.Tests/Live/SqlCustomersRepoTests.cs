using RepositoryPattern.Data.Repositories;
using System.Threading.Tasks;

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
    }
}
