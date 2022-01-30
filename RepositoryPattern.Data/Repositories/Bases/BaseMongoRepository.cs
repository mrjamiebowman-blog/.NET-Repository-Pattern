using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;

namespace RepositoryPattern.Data.Repositories.Bases
{
    public class BaseMongoRepository : BaseRepository
    {
        protected MongoClient _mongo;

        protected string _dbName = "mrjb_repopattern_mongo";

        public static class Databases 
        {
            public static string Customers = $"customers";

        }

        public BaseMongoRepository()
        {
            try
            {
                _mongo = new MongoClient("mongodb://root:555gpah4jzetczpf@127.0.0.1:27017");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
