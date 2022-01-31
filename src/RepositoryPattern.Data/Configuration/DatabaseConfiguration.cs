using RepositoryPattern.Data.Types;

namespace RepositoryPattern.Data.Configuration
{
    public class DatabaseConfiguration
    {
        public const string Position = "DatabaseConfiguration";

        public DataProviderType DataStore { get; set; }

        public SqlDatabaseConfiguration SqlDatabase { get; set; }

        public PostgresDatabaseConfiguration PostgresDatabase { get; set; }
    }
}
