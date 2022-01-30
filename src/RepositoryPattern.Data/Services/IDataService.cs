using KafkaModels.Models.Customer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoryPattern.Data.Services
{
    public interface IDataService
    {
        Task<List<Customer>> GetCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(dynamic id);
        Task DeleteCustomerByIdAsync(dynamic id);
        Task<Customer> CreateCustomerAsync(Customer model);
        Task<Customer> SaveCustomerAsync(Customer model, bool upsert = true);
    }
}
