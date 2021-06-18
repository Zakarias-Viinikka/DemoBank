using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mvc2Inlupp2.Data;
using Mvc2Inlupp2.Models;
using Mvc2Inlupp2.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc2Inlupp2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext dbc;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            dbc = dbContext;
        }
        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any)]
        public IActionResult Index()
        {
            var model = new HomeIndexViewModel();
            model.numberOfCustomers = dbc.Customers.Count();
            model.numberOfAccounts = dbc.Accounts.Count();
            model.sumOfBalanceFromAllAccounts = dbc.Accounts.Sum(r => r.Balance);
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
