using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace RepositoryPattern.Data.Repositories
{
    public abstract class BaseSqlRepository : BaseRepository
    {
        private IConfigurationSection _databaseSettings;
        private IConfigurationSection _settings;

        protected bool? _debug { get; set; }
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
            public static string GetCustomers = $"{dbSchema}.uspGetCustomers";
        }
    }
}
