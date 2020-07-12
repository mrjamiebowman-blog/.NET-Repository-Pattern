using KafkaModels.Models.Customer;
using RepositoryPattern.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoryPattern.Data.Services
{
    public class DataService : IDataService
    {
        private readonly ICustomersRepository _customersRepository;

        public DataService()
        {

        }

        public DataService(ICustomersRepository customersRepository) : this()
        {
            _customersRepository = customersRepository;
        }

        #region Customers

        public Task<Customer> CreateCustomerAsync(Customer model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCustomerByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _customersRepository.GetByIdAsync(id);
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
             return await _customersRepository.GetCustomersAsync();
        }

        public async Task<Customer> SaveCustomerAsync(Customer model)
        {
            return await _customersRepository.SaveAsync(model);
        }

        #endregion
    }
}
