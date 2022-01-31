﻿using KafkaModels.Models.Customer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using RepositoryPattern.Data.Services;
using RepositoryPattern.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RepositoryPattern.Controllers;

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
    public async Task<IActionResult> Edit(string id)
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
    public async Task<IActionResult> Edit(string id, Customer model)
    {
        try
        {
            // hack: doesn't map id to dynamic property.
            model.Id = id;
            model.CustomerIdMg = ObjectId.Parse(id);
            await _dataService.SaveCustomerAsync(model, false);
            TempData["MessageSuccess"] = "Customer saved!";
            return RedirectToAction(nameof(Edit), new { id = id });
        }
        catch (Exception ex)
        {
            throw new Exception("Unable to edit customer. Please refresh the page.");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(Customer model)
    {
        await _dataService.CreateCustomerAsync(model);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(string id)
    {
        var data = await _dataService.GetCustomerByIdAsync(id);
        return View(data);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAction(string id)
    {
        await _dataService.DeleteCustomerByIdAsync(id);
        return RedirectToAction(nameof(Index));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}