using KafkaModels.Models.Customer;
using RepositoryPattern.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoryPattern.Data.Repositories
{
    public class PostgreSqlCustomersRepository : BasePostgreSqlRepository, ICustomersRepository
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

        public Task<List<Customer>> GetCustomersAsync()
        {
            throw new NotImplementedException();
        }

        public Task InitDb()
        {
            throw new NotImplementedException();
        }

        public Task<Customer> SaveAsync(Customer model, bool upsert = true)
        {
            throw new NotImplementedException();
        }
    }
}
