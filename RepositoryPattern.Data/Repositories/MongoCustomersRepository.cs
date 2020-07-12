using KafkaModels.Models.Customer;
using MongoDB.Bson;
using MongoDB.Driver;
using RepositoryPattern.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;

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

        public Task DeleteByIdAsync(dynamic id)
        {
            try
            {
                var db = _mongo.GetDatabase(_dbName);
                IMongoCollection<BsonDocument> collCustomers = db.GetCollection<BsonDocument>(Databases.Customers);
                var deleteFilter = Builders<BsonDocument>.Filter.Eq("CustomerIdMg", id);
                collCustomers.DeleteOne(deleteFilter);
                return null;
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
                IMongoCollection<BsonDocument> collCustomers = db.GetCollection<BsonDocument>(Databases.Customers);
                var filter = Builders<BsonDocument>.Filter.Eq("CustomerIdMg", id);
                var customer = await collCustomers.FindSync<Customer>(filter);
                return customer;
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
                var filter = Builders<BsonDocument>.Filter.Eq("CustomerIdMg", model.CustomerId);
                collCustomers.UpdateOne(filter, model.ToBsonDocument());
                return Task.FromResult(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
