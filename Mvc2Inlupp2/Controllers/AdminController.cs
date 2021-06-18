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
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext dbc;

        public AdminController(ApplicationDbContext dbContext)
        {
            dbc = dbContext;
        }
        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any)]
        public IActionResult ManageUsers()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
