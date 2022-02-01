using KafkaModels.Models.Customer;
using MrJB.RepositoryPattern.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrJB.RepositoryPattern.Data.Tests.Repositories;

public class FakeCustomersRepository : ICustomersRepository
{
    public Task<Customer> CreateAsync(Customer model)
    {
        throw new NotImplementedException();
    }

    public Task DeleteByIdAsync(dynamic id)
    {
        throw new NotImplementedException();
    }

    public Task<Customer> GetByIdAsync(dynamic id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Customer>> GetCustomersAsync()
    {
        throw new NotImplementedException();
    }

    public Task InitDb()
    {
        throw new NotImplementedException();
    }

    public Task<Customer> SaveAsync(Customer model, bool upsert = true)
    {
        throw new NotImplementedException();
    }
}
