using KafkaModels.Models.Customer;
using Microsoft.Extensions.DependencyInjection;
using RepositoryPattern.Data.Repositories;
using RepositoryPattern.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RepositoryPattern.Data.Types;

namespace RepositoryPattern.Data.Services
{
    public class DataService : IDataService
    {
        private readonly ICustomersRepository _customersRepository;
        public IConfiguration Configuration { get; }
        public readonly DataProviderType ProviderType;
        

        public DataService()
        {

        }

        public DataService(/*IConfiguration configuration, */ICustomersRepository customersRepository) : this()
        {
            //Configuration = configuration;
            _customersRepository = customersRepository;

            ProviderType = GetProviderType();
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IDataService, DataService>();

            var providerType = GetProviderType();
            
            if (providerType == DataProviderType.MSSQL) {
                // mssql
                services.AddTransient<ICustomersRepository, SqlCustomersRepository>();
            } else if (providerType == DataProviderType.MongoDB) {
                // mongodb
                services.AddTransient<ICustomersRepository, MongoCustomersRepository>();
            } else if (providerType == DataProviderType.Postgres) {
                // postgres
                services.AddTransient<ICustomersRepository, PostgreSqlCustomersRepository>();
            } else {
                // default
                services.AddTransient<ICustomersRepository, SqlCustomersRepository>();
            }
        }

        public static DataProviderType GetProviderType()
        {
            // hack: should read from config
            return DataProviderType.MongoDB;
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
            if (customer?.Birthdate != null)
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

        private Task<int?> GetCustomerAgeAsync(DateTime? birthdate)
        {
            if (birthdate == null)
                return null;

            int? age = DateTime.Now.Year - birthdate.Value.Year;
            if (DateTime.Now.DayOfYear < birthdate.Value.DayOfYear)
                age = age - 1;

            return Task.FromResult(age);
        }

        #endregion
    }
}
