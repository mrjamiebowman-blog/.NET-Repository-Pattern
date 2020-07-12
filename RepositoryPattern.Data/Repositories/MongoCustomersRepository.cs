using KafkaModels.Models.Customer;
using RepositoryPattern.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace RepositoryPattern.Data.Repositories
{
    public class MongoCustomersRepository : BaseMongoRepository, ICustomersRepository
    {
        protected MongoClient _mongo;

        public MongoCustomersRepository()
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

        public async Task InitDb()
        {
            var db = _mongo.GetDatabase("mrjb_repopattern_mongo");

            IMongoCollection<BsonDocument> customers = db.GetCollection<BsonDocument>("customers");

            // customer
            var model = new Customer();
            model.CustomerId = 1003;
            model.FirstName = "Jamie";
            model.LastName = "Bowman";
            model.Email = "tes2t@test.com";

            // save
            await customers.InsertOneAsync(model.ToBsonDocument());
        }

        public Task<Customer> CreateAsync(Customer model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Customer>> GetCustomersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Customer> SaveAsync(Customer model, bool upsert = true)
        {
            throw new NotImplementedException();
        }
    }
}
