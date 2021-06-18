using JW;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mvc2Inlupp2.Data;
using Mvc2Inlupp2.Models;
using Mvc2Inlupp2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc2Inlupp2.Controllers
{
    [Authorize(Roles = "Admin,Cashier")]
    public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ApplicationDbContext dbc;

        public CustomerController(ILogger<CustomerController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            dbc = dbContext;
        }
        public IActionResult DeleteCustomer(int customerId)
        {
            var customer = dbc.Customers.FirstOrDefault(r => r.CustomerId == customerId);
            dbc.Customers.Remove(customer);
            dbc.SaveChanges();
            return RedirectToAction("Customers");
        }
        public IActionResult NewCustomer()
        {
            var model = new CustomerEditViewModel();

            return View(model);
        }
        [HttpPost]
        public IActionResult NewCustomer(CustomerEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = new Customers
                {
                    Birthday = model.Birthday,
                    Gender = model.Gender,
                    Givenname = model.Givenname,
                    Surname = model.Surname,
                    Streetaddress = model.Streetaddress,
                    City = model.City,
                    Zipcode = model.Zipcode,
                    Country = model.Country,
                    CountryCode = model.CountryCode,
                    NationalId = model.NationalId,
                    Telephonecountrycode = model.Telephonecountrycode,
                    Telephonenumber = model.Telephonenumber,
                    Emailaddress = model.Emailaddress,
                };
                /*
                   CustomerId	int	Unchecked
                   Gender	nvarchar(6)	Unchecked
                   Givenname	nvarchar(100)	Unchecked
                   Surname	nvarchar(100)	Unchecked
                   Streetaddress	nvarchar(100)	Unchecked
                   City	nvarchar(100)	Unchecked
                   Zipcode	nvarchar(15)	Unchecked
                   Country	nvarchar(100)	Unchecked
                   CountryCode	nvarchar(2)	Unchecked
                   Birthday	date	Checked
                   NationalId	nvarchar(20)	Checked
                   Telephonecountrycode	nvarchar(10)	Checked
                   Telephonenumber	nvarchar(25)	Checked
                   Emailaddress	nvarchar(100)	Checked
               */
                dbc.Customers.Add(customer);
                dbc.SaveChanges();
                return RedirectToAction("Customers");
            }
            return View(model);
        }

        public IActionResult EditCustomer(int customerId)
        {
            var model = new CustomerEditViewModel();
            var customer = dbc.Customers.FirstOrDefault(r => r.CustomerId == customerId);

            model.CustomerId = customerId;
            model.Birthday = customer.Birthday;
            model.Gender = customer.Gender;
            model.Givenname = customer.Givenname;
            model.Surname = customer.Surname;
            model.Streetaddress = customer.Streetaddress;
            model.City = customer.City;
            model.Zipcode = customer.Zipcode;
            model.Country = customer.Country;
            model.CountryCode = customer.CountryCode;
            model.NationalId = customer.NationalId;
            model.Telephonecountrycode = customer.Telephonecountrycode;
            model.Telephonenumber = customer.Telephonenumber;
            model.Emailaddress = customer.Emailaddress;

            return View(model);
        }
        [HttpPost]
        public IActionResult EditCustomer(CustomerEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = dbc.Customers.FirstOrDefault(r => r.CustomerId == model.CustomerId);

                customer.Birthday = model.Birthday;
                customer.Gender = model.Gender;
                customer.Givenname = model.Givenname;
                customer.Surname = model.Surname;
                customer.Streetaddress = model.Streetaddress;
                customer.City = model.City;
                customer.Zipcode = model.Zipcode;
                customer.Country = model.Country;
                customer.CountryCode = model.CountryCode;
                customer.NationalId = model.NationalId;
                customer.Telephonecountrycode = model.Telephonecountrycode;
                customer.Telephonenumber = model.Telephonenumber;
                customer.Emailaddress = model.Emailaddress;
                /*
                   CustomerId	int	Unchecked
                   Gender	nvarchar(6)	Unchecked
                   Givenname	nvarchar(100)	Unchecked
                   Surname	nvarchar(100)	Unchecked
                   Streetaddress	nvarchar(100)	Unchecked
                   City	nvarchar(100)	Unchecked
                   Zipcode	nvarchar(15)	Unchecked
                   Country	nvarchar(100)	Unchecked
                   CountryCode	nvarchar(2)	Unchecked
                   Birthday	date	Checked
                   NationalId	nvarchar(20)	Checked
                   Telephonecountrycode	nvarchar(10)	Checked
                   Telephonenumber	nvarchar(25)	Checked
                   Emailaddress	nvarchar(100)	Checked
               */
                dbc.SaveChanges();
                return RedirectToAction("Customer", new { id = model.CustomerId });
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult AddDisponent(int customerId, int accountId)
        {
            var model = new CustomerAddDisponentViewModel();
            model.CustomerId = customerId;
            model.AccountId = accountId;
            return View(model);
        }
        [HttpPost]
        public IActionResult AddDisponent(CustomerAddDisponentViewModel model)
        {
            if (dbc.Customers.FirstOrDefault(r => r.CustomerId == model.DisponentId) == null)
            {
                ModelState.AddModelError("DisponentId", "Disponent you are trying to add does not exist.");
            }

            if (dbc.Dispositions.FirstOrDefault(r => r.CustomerId == model.DisponentId && r.AccountId == model.AccountId) != null)
            {
                ModelState.AddModelError("DisponentId", "Customer already has a disposition to that account.");
            }

            if (ModelState.IsValid)
            {
                dbc.Dispositions.Add(new Dispositions
                {
                    AccountId = model.AccountId,
                    Type = "DISPONENT",
                    CustomerId = model.DisponentId,
                });

                dbc.SaveChanges();
                return RedirectToAction("Customer", new { id = model.CustomerId });
            }
            return View(model);
        }

        public IActionResult DeleteAccount(int customerId, int accountId)
        {
            //var account = dbc.Accounts.FirstOrDefault(r => r.CustomerId == CustomerId);
            var account = dbc.Accounts.FirstOrDefault(r => r.AccountId == accountId);
            if (account != null)
            {
                if (dbc.Dispositions.FirstOrDefault(r => r.CustomerId == customerId && r.AccountId == accountId).Type == "OWNER")
                {
                    var accountDisponents = dbc.Dispositions.Where(r => r.AccountId == accountId).ToList();
                    foreach (var disponent in accountDisponents)
                    {
                        dbc.Dispositions.Remove(disponent);
                    }
                }
                else
                {
                    dbc.Dispositions.Remove(dbc.Dispositions.FirstOrDefault(r => r.CustomerId == customerId && r.AccountId == accountId));
                }

                dbc.SaveChanges();
            }
            return RedirectToAction("Customer", new { id = customerId });
        }
        [HttpGet]
        public IActionResult AddAccount(int id)
        {
            var model = new CustomerAddAccountViewModel();
            model.CustomerId = id;
            return View(model);
        }
        [HttpPost]
        public IActionResult AddAccount(CustomerAddAccountViewModel model)
        {
            //([Frequency] = 'AfterTransaction' OR[Frequency] = 'Weekly' OR[Frequency] = 'Monthly')
            if (model.Frequency != "AfterTransaction" && model.Frequency != "Weekly" && model.Frequency != "Monthly")
            {
                ModelState.AddModelError("Frequency", "Frequency has to be 'AfterTransaction', 'Weekly' or 'Monthly'");
            }

            if (ModelState.IsValid)
            {
                if (dbc.Customers.FirstOrDefault(r => r.CustomerId == model.CustomerId) != null)
                {
                    var account = new Accounts
                    {
                        Created = DateTime.Now,
                        Frequency = model.Frequency,
                    };
                    dbc.Accounts.Add(account);

                    var disposition = new Dispositions
                    {
                        Account = account,
                        CustomerId = model.CustomerId,
                        Type = "OWNER",
                    };
                    dbc.Dispositions.Add(disposition);

                    dbc.SaveChanges();
                }
                return RedirectToAction("Customer", new { id = model.CustomerId });
            }
            return View(model);
        }
        public IActionResult _Fetch20Transactions(int id, int ctr)
        {
            var model = new List<CustomerAccountViewModel.Transaction>();
            model = dbc.Transactions.Where(r => r.AccountId == id).OrderByDescending(r => r.TransactionId).Skip(ctr * 20).Take(20).Select(r => new CustomerAccountViewModel.Transaction
            {
                amount = r.Amount,
                bank = r.Bank,
                date = r.Date,
                operation = r.Operation,
                type = r.Type

            }).ToList();

            return View(model);
        }
        public IActionResult Account(int id)
        {
            var model = new CustomerAccountViewModel();
            var account = dbc.Accounts.FirstOrDefault(r => r.AccountId == id);

            model.account.id = account.AccountId;
            model.account.balance = account.Balance;

            model.account.transactions = dbc.Transactions.Where(r => r.AccountId == id).OrderByDescending(r => r.TransactionId).Take(20).Select(r => new CustomerAccountViewModel.Transaction
            {
                amount = r.Amount,
                bank = r.Bank,
                date = r.Date,
                operation = r.Operation,
                type = r.Type

            }).ToList();
            return View(model);
        }
        public IActionResult Customer(int id)
        {
            var model = new CustomerViewModel();

            var customer = dbc.Customers.FirstOrDefault(r => r.CustomerId == id);

            model.customer.name = customer.Givenname + " " + customer.Surname;
            model.customer.gender = customer.Gender;
            model.customer.address = customer.Streetaddress;
            model.customer.country = customer.Country;
            model.customer.city = customer.City;
            model.customer.email = customer.Emailaddress;
            model.customer.ssn = customer.NationalId;
            model.customer.telephoneNumber = customer.Telephonenumber;
            model.customer.id = customer.CustomerId;

            model.customer.accounts = dbc.Dispositions.Where(r => r.CustomerId == customer.CustomerId).Select(r => new CustomerViewModel.Accounts
            {
                id = r.AccountId,
                balance = r.Account.Balance,
                dispositionType = r.Type,
            }).ToList();


            model.customer.totalBalance = model.customer.accounts.Sum(r => r.balance);

            /*
    public List<Accounts> account { get; set; }
    public string name { get; set; }
    public string gender { get; set; }
    public string address { get; set; }
    public string city { get; set; }
    public string country { get; set; }
    public string ssn { get; set; }
    public int sumOfAllAccounts { get; set; }
    public string telephoneNumber { get; set; }*/
            return View(model);
        }

        public IActionResult Customers(string? q, int? pageNumber, string? orderBy, bool? descending)
        {
            var model = new CustomerCustomersViewModel();

            model.q = q;
            model.orderBy = orderBy;
            model.descending = descending;

            IQueryable<CustomerCustomersViewModel.Customers> query;

            if (pageNumber.HasValue == false)
            {
                pageNumber = 1;
            }


            if (q != null)
            {
                int.TryParse(q, out int qAsInt);
                if (qAsInt == 0)
                {
                    qAsInt = 0;
                }
                qAsInt = 0;
                query = dbc.Customers.Where(r =>
                    r.Streetaddress.Contains(q) ||
                    r.Givenname.Contains(q) ||
                    r.CustomerId == qAsInt ||
                    r.City.Contains(q)
                )
                    .Select(r => new CustomerCustomersViewModel.Customers
                    {
                        id = r.CustomerId,
                        ssn = r.NationalId,
                        customerName = r.Givenname + " " + r.Surname,
                        city = r.City,
                        address = r.Streetaddress
                    });
            }
            else
            {
                query = dbc.Customers.Select(r => new CustomerCustomersViewModel.Customers
                {
                    id = r.CustomerId,
                    ssn = r.NationalId,
                    customerName = r.Givenname + " " + r.Surname,
                    city = r.City,
                    address = r.Streetaddress
                });
            }


            switch (orderBy)
            {
                case "customers":
                    query = query.OrderBy(r => r.id);
                    if (descending == true)
                    {
                        query = query.OrderByDescending(r => r.id);
                    }
                    break;
                case "ssn":
                    query = query.OrderBy(r => r.ssn);
                    if (descending == true)
                    {
                        query = query.OrderByDescending(r => r.ssn);
                    }
                    break;
                case "name":
                    query = query.OrderBy(r => r.customerName);
                    if (descending == true)
                    {
                        query = query.OrderByDescending(r => r.customerName);
                    }
                    break;
                case "address":
                    query = query.OrderBy(r => r.address);
                    if (descending == true)
                    {
                        query = query.OrderByDescending(r => r.address);
                    }
                    break;
                case "city":
                    query = query.OrderBy(r => r.city);
                    if (descending == true)
                    {
                        query = query.OrderByDescending(r => r.city);
                    }
                    break;
            }


            model.customers = query.ToList();
            model.pager = new Pager(model.customers.Count(), pageNumber.Value, 50);
            /*
        public int id { get; set; }
        public string ssn { get; set; }
        public string customerName { get; set; }
        public string city { get; set; }
        public string address { get; set; }*/
            return View(model);
        }
    }
}
