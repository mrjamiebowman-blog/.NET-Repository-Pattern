using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace RepositoryPattern.Data.Repositories.Bases
{
    public class BasePostgreSqlRepository : BaseRepository
    {
        private IConfigurationSection _databaseSettings;
        private IConfigurationSection _settings;

        //private static string dbSchema = "dbo";

        public BasePostgreSqlRepository()
        {
            _databaseSettings = GetConfigurationSection("PostgresDatabase");
        }

        public IDbConnection GetConnection()
        {
            var conn = new NpgsqlConnection(_databaseSettings["ConnectionString"]);
            return conn;
        }

        //public static class StoredProcedures
        //{
        //    public static string CreateCustomer = $"{dbSchema}.uspCustomerCreate";
        //    public static string GetCustomer = $"{dbSchema}.uspCustomerGet";
        //    public static string DeleteCustomer = $"{dbSchema}.uspCustomerDelete";
        //    public static string SaveCustomer = $"{dbSchema}.uspCustomerSave";
        //    public static string GetCustomers = $"{dbSchema}.uspCustomersGet";
        //}
    }
}
