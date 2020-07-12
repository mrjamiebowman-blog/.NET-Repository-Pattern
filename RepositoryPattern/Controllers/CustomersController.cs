using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RepositoryPattern.Models;
using System.Diagnostics;
using System.Threading.Tasks;
using KafkaModels.Models.Customer;
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

        public async Task<IActionResult> Index()
        {
            try
            {
                var data = await _dataService.GetCustomersAsync();
                return View(data);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to load customers. Please refresh the page.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var data = await _dataService.GetCustomerByIdAsync(id);
                return View(data);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to load customer. Please refresh the page.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Customer model)
        {
            try
            {
                await _dataService.SaveCustomerAsync(model);
                TempData["MessageSuccess"] = "Customer saved!";
                return RedirectToAction(nameof(Edit), new { id = model.CustomerId });
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to edit customer. Please refresh the page.");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}