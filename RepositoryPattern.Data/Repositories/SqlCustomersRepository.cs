using Dapper;
using KafkaModels.Models.Customer;
using RepositoryPattern.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RepositoryPattern.Data.Repositories
{
    public class SqlCustomersRepository : BaseSqlRepository, ICustomersRepository
    {
        public async Task<Customer> CreateAsync(Customer model)
        {
            try
            {
                using (IDbConnection conn = GetConnection())
                {
                    conn.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("@FirstName", model.FirstName);
                    parameters.Add("@LastName", model.LastName);
                    parameters.Add("@Email", model.Email);
                    if (model.BillingAddress.AddressId.HasValue)
                        parameters.Add("@BillingAddressId", model.BillingAddress.AddressId);
                    if (model.ShippingAddress.AddressId.HasValue)
                        parameters.Add("@ShippingAddressId", model.ShippingAddress.AddressId);

                    var data = (await conn.QueryAsync<Customer>(StoredProcedures.CreateCustomer, parameters, commandType: CommandType.StoredProcedure)).SingleOrDefault();
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteByIdAsync(dynamic id)
        {
            try
            {
                using (IDbConnection conn = GetConnection())
                {
                    conn.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("@CustomerId", id);

                    await conn.QueryAsync(StoredProcedures.DeleteCustomer, parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Customer> GetByIdAsync(dynamic id)
        {
            try
            {
                using (IDbConnection conn = GetConnection())
                {
                    conn.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("@CustomerId", id);

                    var data = (await conn.QueryAsync<Customer>(StoredProcedures.GetCustomer, parameters, commandType: CommandType.StoredProcedure)).SingleOrDefault();
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            try
            {
                using (IDbConnection conn = GetConnection())
                {
                    conn.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("@FirstName", "");
                    parameters.Add("@LastName", "");
                    parameters.Add("@Email", "");
                    parameters.Add("@City", "");
                    parameters.Add("@State", "");
                    parameters.Add("@Country", "");

                    var data = (await conn.QueryAsync<Customer>(StoredProcedures.GetCustomers, parameters, commandType: CommandType.StoredProcedure)).ToList();
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task InitDb()
        {
            throw new NotImplementedException();
        }

        public async Task<Customer> SaveAsync(Customer model, bool upsert = true)
        {
            try
            {
                using (IDbConnection conn = GetConnection())
                {
                    conn.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("@Upsert", upsert);
                    parameters.Add("@CustomerId", model.CustomerId);
                    parameters.Add("@FirstName", model.FirstName);
                    parameters.Add("@LastName", model.LastName);
                    parameters.Add("@Email", model.Email);
                    if (model.BillingAddress != null && model.BillingAddress.AddressId.HasValue)
                        parameters.Add("@BillingAddressId", model.BillingAddress.AddressId);
                    if (model.ShippingAddress != null && model.ShippingAddress.AddressId.HasValue)
                        parameters.Add("@ShippingAddressId", model.ShippingAddress.AddressId);
                    parameters.Add("@BirthDate", model.Birthdate);

                    var data = (await conn.QueryAsync<Customer>(StoredProcedures.SaveCustomer, parameters, commandType: CommandType.StoredProcedure)).SingleOrDefault();
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
