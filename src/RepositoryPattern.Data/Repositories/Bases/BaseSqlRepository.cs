using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace RepositoryPattern.Data.Repositories.Bases;

public abstract class BaseSqlRepository : BaseRepository
{
    private IConfigurationSection _databaseSettings;
    private IConfigurationSection _settings;

    private static string dbSchema = "dbo";

    public BaseSqlRepository()
    {
        _databaseSettings = GetConfigurationSection("SqlDatabase");
    }

    public IDbConnection GetConnection()
    {
        var conn = new SqlConnection(_databaseSettings["ConnectionString"]);
        return conn;
    }

    public static class StoredProcedures
    {
        public static string CreateCustomer = $"{dbSchema}.uspCustomerCreate";
        public static string GetCustomer = $"{dbSchema}.uspCustomerGet";
        public static string DeleteCustomer = $"{dbSchema}.uspCustomerDelete";
        public static string SaveCustomer = $"{dbSchema}.uspCustomerSave";
        public static string GetCustomers = $"{dbSchema}.uspCustomersGet";
    }
}
