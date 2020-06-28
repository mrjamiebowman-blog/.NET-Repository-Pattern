using KafkaModels.Models.Customer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoryPattern.Data.Repositories.Interfaces
{
    public interface ICustomersRepository
    {
        Task<Customer> GetByIdAsync(int id);
        Task<List<Customer>> GetCustomersAsync();
        Task DeleteByIdAsync(int id);
        Task<Customer> CreateAsync(Customer model);
        Task SaveAsync(Customer model);
    }
}
