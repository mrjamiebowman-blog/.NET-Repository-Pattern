using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RepositoryPattern.Models;
using System.Diagnostics;
using RepositoryPattern.Data.Services;

namespace RepositoryPattern.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly IDataService _dataService;

        public CustomersController(ILogger<CustomersController> logger, IDataService dataService)
        {
            _logger = logger;
            _dataService = dataService;
        }

        public IActionResult Index()
        {
            // load all customers

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}