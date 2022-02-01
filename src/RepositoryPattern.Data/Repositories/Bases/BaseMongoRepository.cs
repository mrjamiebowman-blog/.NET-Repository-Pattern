using MongoDB.Driver;
using MrJB.RepositoryPattern.Data.Configuration;
using RepositoryPattern.Data.Configuration;
using System;

namespace RepositoryPattern.Data.Repositories.Bases;

public class BaseMongoRepository : BaseRepository
{
    private readonly MongoDbConfiguration configuration;

    protected MongoClient _mongo;

    protected string _dbName = "mrjb_repopattern_mongo";

    public static class Databases
    {
        public static string Customers = $"customers";

    }

    public BaseMongoRepository(DatabaseConfiguration databaseConfiguration) : base(databaseConfiguration)
    {
        try
        {
            configuration = databaseConfiguration.MongoDb;
            _mongo = new MongoClient(configuration.ConnectionString);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
