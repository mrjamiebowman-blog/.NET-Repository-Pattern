using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KafkaModels.Models.Customer;

namespace RepositoryPattern.Data.Repositories
{
    public interface ICustomersRepository
    {
        Task<Customer> GetByIdAsync(int id);
        Task DeleteByIdAsync(int id);
        Task<Customer> CreateAsync(Customer model);
        Task SaveAsync(Customer model);
    }
}
