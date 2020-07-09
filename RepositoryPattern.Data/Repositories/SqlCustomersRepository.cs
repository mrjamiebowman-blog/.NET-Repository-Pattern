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
                    //parameters.Add("@PARAM", "");

                    var data = (await conn.QueryAsync<Customer>(StoredProcedures.CreateCustomer, parameters, commandType: CommandType.StoredProcedure)).SingleOrDefault();
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            try
            {
                using (IDbConnection conn = GetConnection())
                {
                    conn.Open();

                    var parameters = new DynamicParameters();
                    //parameters.Add("@PARAM", "");

                    await conn.QueryAsync(StoredProcedures.DeleteCustomer, parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            try
            {
                using (IDbConnection conn = GetConnection())
                {
                    conn.Open();

                    var parameters = new DynamicParameters();
                    //parameters.Add("@PARAM", "");

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
                    //parameters.Add("@PARAM", "");

                    var data = (await conn.QueryAsync<Customer>(StoredProcedures.GetCustomers, parameters, commandType: CommandType.StoredProcedure)).ToList();
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Customer> SaveAsync(Customer model)
        {
            try
            {
                using (IDbConnection conn = GetConnection())
                {
                    conn.Open();

                    var parameters = new DynamicParameters();
                    //parameters.Add("@PARAM", "");

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
