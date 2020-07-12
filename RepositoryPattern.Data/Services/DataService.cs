using KafkaModels.Models.Customer;
using Microsoft.Extensions.DependencyInjection;
using RepositoryPattern.Data.Repositories;
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

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IDataService, DataService>();
            //services.AddTransient<ICustomersRepository, SqlCustomersRepository>();
            services.AddTransient<ICustomersRepository, MongoCustomersRepository>();
            //services.AddTransient<ICustomersRepository, PostgreSqlCustomersRepository>();
        }

        public DataService(ICustomersRepository customersRepository) : this()
        {
            _customersRepository = customersRepository;
        }

        #region Customers

        public async Task<Customer> CreateCustomerAsync(Customer model)
        {
            return await _customersRepository.CreateAsync(model);
        }

        public async Task DeleteCustomerByIdAsync(dynamic id)
        {
            await _customersRepository.DeleteByIdAsync(id);
        }

        public async Task<Customer> GetCustomerByIdAsync(dynamic id)
        {
            var customer = await _customersRepository.GetByIdAsync(id);
            customer.Age = await GetCustomerAgeAsync(customer.Birthdate);
            return customer;
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
             var customers = await _customersRepository.GetCustomersAsync();

            // this is a terrible idea but it's for demonstrable purposes
            customers.ForEach(async x => x.Age = await GetCustomerAgeAsync(x.Birthdate));

            return customers;
        }

        public async Task<Customer> SaveCustomerAsync(Customer model, bool upsert = true)
        {
            return await _customersRepository.SaveAsync(model, upsert);
        }

        private Task<int> GetCustomerAgeAsync(DateTime birthdate)
        {
            int age = DateTime.Now.Year - birthdate.Year;
            if (DateTime.Now.DayOfYear < birthdate.DayOfYear)
                age = age - 1;
            return Task.FromResult(age);
        }

        #endregion
    }
}
