using KafkaModels.Models.Customer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoryPattern.Data.Repositories.Interfaces
{
    public interface ICustomersRepository
    {
        Task<Customer> GetByIdAsync(dynamic id);
        Task<List<Customer>> GetCustomersAsync();
        Task DeleteByIdAsync(dynamic id);
        Task<Customer> CreateAsync(Customer model);
        Task<Customer> SaveAsync(Customer model, bool upsert = true);
        Task InitDb();
    }
}
