using Microsoft.Extensions.Configuration;
using RepositoryPattern.Data.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace RepositoryPattern.Data.Repositories.Bases;

public abstract class BaseSqlRepository : BaseRepository
{
    private readonly SqlDatabaseConfiguration configuration;

    private static string dbSchema = "dbo";

    public BaseSqlRepository(DatabaseConfiguration databaseConfiguration) : base(databaseConfiguration)
    {
        configuration = databaseConfiguration.SqlDatabase;
    }

    public IDbConnection GetConnection()
    {
        var conn = new SqlConnection(configuration.ConnectionString);
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
