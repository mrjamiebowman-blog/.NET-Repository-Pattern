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

            int id = 1;

            // act
            var customer = await _customerRepo.GetByIdAsync(id);
        }
    }
}
