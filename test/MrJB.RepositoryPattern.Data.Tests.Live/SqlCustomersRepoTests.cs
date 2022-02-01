//using MrJB.RepositoryPattern.Data.Repositories;
//using System.Threading.Tasks;
//using MrJB.KafkaModels.Models.Customer;
//using MrJB.RepositoryPattern.Data.Repositories.Interfaces;
//using Xunit;

//namespace MrJB.RepositoryPattern.Data.Tests.Live;

//public class SqlCustomersRepoTests
//{
//    protected ICustomersRepository _customerRepo;

//    public SqlCustomersRepoTests()
//    {
//        _customerRepo = new SqlCustomersRepository();
//    }

//    [Fact]
//    public async Task GetCustomersAsyncTests()
//    {
//        // fyi: you may need to update this id
//        string id = "5f0a888694e331568c891831";

//        // act
//        var customer = await _customerRepo.GetByIdAsync(id);
//    }

//    //[Fact]
//    public async Task DeleteCustomerAsyncTests()
//    {
//        // fyi
//        int id = 1002;

//        await _customerRepo.DeleteByIdAsync(id);
//    }

//    [Fact]
//    public async Task SaveAsync()
//    {
//        var model = new Customer();
//        model.CustomerId = 1003;
//        model.FirstName = "Jamie";
//        model.LastName = "Bowman";
//        model.Email = "tes2t@test.com";

//        // act
//        model = await _customerRepo.SaveAsync(model);
//    }
//}
