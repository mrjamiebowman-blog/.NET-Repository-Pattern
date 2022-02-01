using MrJB.RepositoryPattern.Data.Configuration;
using RepositoryPattern.Data.Types;

namespace RepositoryPattern.Data.Configuration;
    
public class DatabaseConfiguration
{
    public const string Position = "DatabaseConfiguration";

    public DataStoreType DataStore { get; set; }

    public SqlDatabaseConfiguration SqlDatabase { get; set; }

    public MongoDbConfiguration MongoDb { get; set; }

    public PostgresDatabaseConfiguration PostgresDatabase { get; set; }
}
