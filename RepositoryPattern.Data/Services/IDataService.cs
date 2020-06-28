using KafkaModels.Models.Customer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoryPattern.Data.Services
{
    public interface IDataService
    {
        Task<List<Customer>> GetCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task DeleteCustomerByIdAsync(int id);
        Task<Customer> CreateCustomerAsync(Customer model);
        Task SaveCustomerAsync(Customer model);
    }
}
