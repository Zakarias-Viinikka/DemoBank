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
    public class PartialController : Controller
    {
        private readonly ILogger<PartialController> _logger;
        private readonly ApplicationDbContext context;

        public PartialController(ILogger<PartialController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            context = dbContext;
        }

        public IActionResult _LoggedInAsPartial()
        {
            var model = new _PartialLoggedInAsViewModel();
            if (User.Identity.IsAuthenticated)
            {
                model.userIsLoggedIn = true;
                var IdentityRoles = context.UserRoles.OrderByDescending(r => r.RoleId).ToList();

                foreach (var identityRole in IdentityRoles)
                {
                    string role = context.Roles.FirstOrDefault(r => r.Id == identityRole.RoleId).Name;
                    if (User.IsInRole(role))
                    {
                        model.userRole = role;
                        break;
                    }
                    Debug.WriteLine(role);
                }
            }
            else
                model.userIsLoggedIn = false;


            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
