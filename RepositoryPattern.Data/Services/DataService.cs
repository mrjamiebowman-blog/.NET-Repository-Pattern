using System;
using RepositoryPattern.Data.Repositories;

namespace RepositoryPattern.Data.Services
{
    public class DataService : IDataService
    {
        private readonly ICustomersRepository _customersRepository;

        public DataService()
        {

        }

        public DataService(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }
    }
}
