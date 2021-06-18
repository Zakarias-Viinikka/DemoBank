using JW;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mvc2Inlupp2.Data;
using Mvc2Inlupp2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc2Inlupp2.Controllers
{
    [Authorize(Roles = "Admin,Cashier")]
    public class CashierController : Controller
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ApplicationDbContext dbc;
        private readonly IMoneyManager moneyManager;

        public CashierController(ILogger<CustomerController> logger, ApplicationDbContext dbContext, IMoneyManager MoneyManager)
        {
            _logger = logger;
            dbc = dbContext;
            moneyManager = MoneyManager;
        }

        public IActionResult TransferMoney()
        {
            var model = new CashierTransferMoneyViewModel();

            return View(model);
        }

        [HttpPost]
        public IActionResult TransferMoney(CashierTransferMoneyViewModel model)
        {
            var transaction = new TransactionReceipt
            {
                Date = DateTime.Now,
                Bank = model.bank,
                Symbol = model.symbol,
                Amount = model.amount,
                Operation = "Transfering money between accounts",
                Type = model.type,
                FromAccount = model.fromAccount,
                ToAccount = model.toAccount,
                ToAccountBalance = dbc.Accounts.FirstOrDefault(r => r.AccountId == model.toAccount).Balance,
                FromAccountBalance = dbc.Accounts.FirstOrDefault(r => r.AccountId == model.fromAccount).Balance,
            };

            if (ModelState.IsValid)
            {
                if (model.type == "Debit" || model.type == "Credit")
                {
                    var result = moneyManager.TransferMoney(model.fromAccount, model.toAccount, model.amount, transaction);
                    if (result != "success")
                    {
                        ModelState.AddModelError("amount", result);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }

                } else
                {
                    ModelState.AddModelError("type", "Transaction type has to be either Debit or Credit");
                }
            }

            return View(model);
        }

        public IActionResult TakeOutMoney()
        {
            var model = new CashierTakeOutMoneyViewModel();

            return View(model);
        }

        [HttpPost]
        public IActionResult TakeOutMoney(CashierTakeOutMoneyViewModel model)
        {
            var transaction = new TransactionReceipt
            {
                Date = DateTime.Now,
                Bank = model.bank,
                Symbol = model.symbol,
                Amount = model.amount,
                Operation = "Withdrawing Money",
                Type = model.type,
                FromAccount = model.fromAccount,
                FromAccountBalance = dbc.Accounts.FirstOrDefault(r => r.AccountId == model.fromAccount).Balance
            };

            if (ModelState.IsValid)
            {
                if (model.type == "Debit" || model.type == "Credit")
                {
                    var result = moneyManager.TakeOutMoney(model.fromAccount, model.amount, transaction);
                    if (result != "success")
                    {
                        ModelState.AddModelError("amount", result);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }

                }
                else
                {
                    ModelState.AddModelError("type", "Transaction type has to be either Debit or Credit");
                }
            }

            return View(model);
        }
        public IActionResult InsertMoney()
        {
            var model = new CashierInsertMoneyViewModel();

            return View(model);
        }

        [HttpPost]
        public IActionResult InsertMoney(CashierInsertMoneyViewModel model)
        {
            var transaction = new TransactionReceipt
            {
                Date = DateTime.Now,
                Bank = model.bank,
                Symbol = model.symbol,
                Amount = model.amount,
                Operation = "Withdrawing Money",
                Type = model.type,
                ToAccountBalance = dbc.Accounts.FirstOrDefault(r => r.AccountId == model.toAccount).Balance,
                ToAccount = model.toAccount
            };

            if (ModelState.IsValid)
            {
                if (model.type == "Debit" || model.type == "Credit")
                {
                    var result = moneyManager.InsertMoney(model.toAccount, model.amount, transaction);
                    if (result != "success")
                    {
                        ModelState.AddModelError("amount", result);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }

                }
                else
                {
                    ModelState.AddModelError("type", "Transaction type has to be either Debit or Credit");
                }
            }

            return View(model);
        }
    }
}
