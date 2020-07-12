using KafkaModels.Models.Customer;
using MongoDB.Bson;
using MongoDB.Driver;
using RepositoryPattern.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoryPattern.Data.Repositories
{
    public class MongoCustomersRepository : BaseMongoRepository, ICustomersRepository
    {
        public MongoCustomersRepository() : base()
        {

        }

        public async Task InitDb()
        {
            var db = _mongo.GetDatabase("mrjb_repopattern_mongo");
            IMongoCollection<BsonDocument> customers = db.GetCollection<BsonDocument>("customers");

            // customer
            var model = new Customer();
            model.FirstName = "Jamie";
            model.LastName = "Bowman";
            model.Email = "tes2t@test.com";
            model.Birthdate = new DateTime(1983, 08, 07, 0, 0, 0);

            // save
            await customers.InsertOneAsync(model.ToBsonDocument());
        }

        public async Task<Customer> CreateAsync(Customer model)
        {
            var db = _mongo.GetDatabase("mrjb_repopattern_mongo");
            IMongoCollection<BsonDocument> customers = db.GetCollection<BsonDocument>("customers");
            await customers.InsertOneAsync(model.ToBsonDocument());
            return model;
        }

        public Task DeleteByIdAsync(dynamic id)
        {
            throw new NotImplementedException();
        }

        public async Task<Customer> GetByIdAsync(dynamic id)
        {
            try
            {
                var db = _mongo.GetDatabase("mrjb_repopattern_mongo");
                IMongoCollection<BsonDocument> customers = db.GetCollection<BsonDocument>("customers");

                var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
                var customer = await customers.FindSync<Customer>(filter);
                return customer;
            } catch (Exception ex) {
                throw ex;
            }
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
