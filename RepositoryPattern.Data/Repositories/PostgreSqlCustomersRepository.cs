using KafkaModels.Models.Customer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Data.Repositories
{
    public class PostgreSqlCustomersRepository : ICustomersRepository
    {
        public Task<Customer> CreateAsync(Customer model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync(Customer model)
        {
            throw new NotImplementedException();
        }
    }
}
