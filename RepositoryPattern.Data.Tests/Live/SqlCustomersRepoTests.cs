using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RepositoryPattern.Data.Repositories;
using RepositoryPattern.Data.Repositories.Interfaces;
using RepositoryPattern.Data.Services;
using RepositoryPattern.Data.Tests.Repositories;
using Xunit;

namespace RepositoryPattern.Data.Tests.Live
{
    public class SqlCustomersRepoTests
    {
        public SqlCustomersRepoTests()
        {

        }

        [Fact]
        public async Task GetCustomersAsyncTests()
        {
            // arrange
            var _customerRepo = new SqlCustomersRepository();

            // fyi: you may need to update this id
            int id = 1;

            // act
            var customer = await _customerRepo.GetByIdAsync(id);
        }

        [Fact]
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
