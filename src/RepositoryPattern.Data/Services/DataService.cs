using KafkaModels.Models.Customer;
using Microsoft.Extensions.DependencyInjection;
using RepositoryPattern.Data.Repositories;
using RepositoryPattern.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RepositoryPattern.Data.Types;
using RepositoryPattern.Data.Configuration;

namespace RepositoryPattern.Data.Services
{
    public class DataService : IDataService
    {
        private readonly ICustomersRepository _customersRepository;

        private readonly DatabaseConfiguration _databaseConfiguration;


        public DataService(ICustomersRepository customersRepository, DatabaseConfiguration databaseConfiguration)
        {
            _customersRepository = customersRepository;
            _databaseConfiguration = databaseConfiguration;
        }

        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDataService, DataService>();

            // bind database configuration
            DatabaseConfiguration databaseConfiguration = new DatabaseConfiguration();
            configuration.GetSection(DatabaseConfiguration.Position).Bind(databaseConfiguration);
            services.AddSingleton<DatabaseConfiguration>(databaseConfiguration);

            DataStoreType dataProviderType = DataStoreType.MSSQL;

            switch (dataProviderType)
            {
               case DataStoreType.Postgres:
                    // postgres
                    services.AddTransient<ICustomersRepository, PostgreSqlCustomersRepository>();
                    break;
                case DataStoreType.MongoDB:
                    // mongodb
                    services.AddTransient<ICustomersRepository, MongoCustomersRepository>();
                    break;
                default:
                    // default to MSSQL
                    services.AddTransient<ICustomersRepository, SqlCustomersRepository>();
                    break;
            }
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
