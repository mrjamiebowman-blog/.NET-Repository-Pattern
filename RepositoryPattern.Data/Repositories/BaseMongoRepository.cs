using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;

namespace RepositoryPattern.Data.Repositories
{
    public class BaseMongoRepository : BaseRepository
    {
        protected MongoClient _mongo;
        
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
