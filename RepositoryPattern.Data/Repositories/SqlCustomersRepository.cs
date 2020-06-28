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
        public Task<Customer> CreateAsync(Customer model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
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

        public Task SaveAsync(Customer model)
        {
            throw new NotImplementedException();
        }
    }
}
