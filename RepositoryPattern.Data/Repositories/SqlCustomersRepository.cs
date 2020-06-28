using KafkaModels.Models.Customer;
using System;
using System.Threading.Tasks;

namespace RepositoryPattern.Data.Repositories
{
    public class SqlCustomersRepository : ICustomersRepository
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
