using Npgsql;
using RepositoryPattern.Data.Configuration;
using System.Data;

namespace RepositoryPattern.Data.Repositories.Bases;

public class BasePostgreSqlRepository : BaseRepository
{
    private readonly PostgresDatabaseConfiguration _postgresDatabaseConfiguration;

    private static string dbSchema = "public";

    public BasePostgreSqlRepository(DatabaseConfiguration databaseConfiguration) : base(databaseConfiguration)
    {
        _postgresDatabaseConfiguration = databaseConfiguration.PostgresDatabase;
    }

    public IDbConnection GetConnection()
    {
        var conn = new NpgsqlConnection(_postgresDatabaseConfiguration.ConnectionString);
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
