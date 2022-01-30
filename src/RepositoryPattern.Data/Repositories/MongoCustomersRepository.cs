using KafkaModels.Models.Customer;
using MongoDB.Bson;
using MongoDB.Driver;
using RepositoryPattern.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using RepositoryPattern.Data.Repositories.Bases;

namespace RepositoryPattern.Data.Repositories
{
    public class MongoCustomersRepository : BaseMongoRepository, ICustomersRepository
    {
        public MongoCustomersRepository() : base()
        {

        }

        public async Task InitDb()
        {
            try
            {
                var db = _mongo.GetDatabase(_dbName);

                IMongoCollection<BsonDocument> collCustomers = db.GetCollection<BsonDocument>(Databases.Customers);

                // customer
                var model = new Customer();
                model.FirstName = "Jamie";
                model.LastName = "Bowman";
                model.Email = "tes2t@test.com";
                model.Birthdate = new DateTime(1983, 08, 07, 0, 0, 0);

                // save
                await collCustomers.InsertOneAsync(model.ToBsonDocument());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Customer> CreateAsync(Customer model)
        {
            try
            {
                var db = _mongo.GetDatabase(_dbName);
                IMongoCollection<BsonDocument> collCustomers = db.GetCollection<BsonDocument>(Databases.Customers);
                await collCustomers.InsertOneAsync(model.ToBsonDocument());
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteByIdAsync(dynamic id)
        {
            try
            {
                var db = _mongo.GetDatabase(_dbName);
                IMongoCollection<BsonDocument> collCustomers = db.GetCollection<BsonDocument>(Databases.Customers);
                var deleteFilter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse((string)id));
                await collCustomers.DeleteOneAsync(deleteFilter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Customer> GetByIdAsync(dynamic id)
        {
            try
            {
                var db = _mongo.GetDatabase(_dbName);
                var collection = db.GetCollection<BsonDocument>(Databases.Customers);
                var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse((string)id));
                var filterResult = collection.Find(filter).FirstOrDefault();
                if (filterResult == null)
                    return null;
                var customer = BsonSerializer.Deserialize<Customer>(filterResult);
                return await Task.FromResult(customer);
            } catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            try
            {
                var db = _mongo.GetDatabase(_dbName);
                IMongoCollection<BsonDocument> collCustomers = db.GetCollection<BsonDocument>(Databases.Customers);
                var data = collCustomers.Find(new BsonDocument()).ToList();
                var custs = new List<Customer>();
                data.ForEach(x => custs.Add(BsonSerializer.Deserialize<Customer>(x)));
                return custs;
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<Customer> SaveAsync(Customer model, bool upsert = true)
        {
            try
            {
                var db = _mongo.GetDatabase(_dbName);
                IMongoCollection<BsonDocument> collCustomers = db.GetCollection<BsonDocument>(Databases.Customers);
                var filter = Builders<BsonDocument>.Filter.Eq("_id", model.CustomerIdMg);
                collCustomers.ReplaceOne(filter, model.ToBsonDocument());
                return Task.FromResult(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
