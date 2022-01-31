using Npgsql;
using RepositoryPattern.Data.Configuration;
using System.Data;

namespace RepositoryPattern.Data.Repositories.Bases;

public class BasePostgreSqlRepository : BaseRepository
{
    private static string dbSchema = "public";

    public IDbConnection GetConnection(PostgresDatabaseConfiguration postgresDatabaseConfiguration)
    {
        var conn = new NpgsqlConnection(postgresDatabaseConfiguration.ConnectionString);
        return conn;
    }

    public static class StoredProcedures
    {
        public static string CreateCustomer = $"{dbSchema}.create_customer";
        public static string GetCustomer = $"{dbSchema}.get_customer";
        public static string DeleteCustomer = $"{dbSchema}.delete_customer";
        public static string SaveCustomer = $"{dbSchema}.save_customer";
        public static string GetCustomers = $"{dbSchema}.get_customers";
    }
}
