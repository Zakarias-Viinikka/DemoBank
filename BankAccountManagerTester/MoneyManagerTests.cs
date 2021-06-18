using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mvc2Inlupp2.Data;
using Moq;
using Mvc2Inlupp2.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;

namespace BankAccountManagerTester
{

    [TestClass]
    public class MoneyManagerTests
    {
        private MoneyManager sut;
        private ApplicationDbContext mockContext;

        public MoneyManagerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            mockContext = new ApplicationDbContext(options);
            sut = new MoneyManager(mockContext);
        }



        [TestMethod]
        public void Check_if_it_is_possible_to_add_money_to_account()
        {
            var account = new Accounts();
            mockContext.Accounts.Add(account);
            mockContext.SaveChanges();

            var originalBalance = account.Balance;
            var amountToAdd = 100M;

            var transaction = new TransactionReceipt
            {
                Amount = amountToAdd,
                Type = "Credit",
                Date = DateTime.Now,
                ToAccount = account.AccountId
            };
            sut.InsertMoney(account.AccountId, amountToAdd, transaction);
            Assert.AreEqual(originalBalance + amountToAdd, account.Balance);
        }

        [TestMethod]
        public void Check_that_it_is_not_possible_to_move_more_money_than_an_account_has()
        {
            var account = new Accounts();
            var account2 = new Accounts();
            account.Balance = 50M;
            mockContext.Accounts.Add(account);
            mockContext.Accounts.Add(account2);
            mockContext.SaveChanges();

            var originalBalance = account.Balance;
            var amountToTakeOut = 100M;

            var transaction = new TransactionReceipt
            {
            };
            ;
            Assert.AreEqual(sut.TakeOutMoney(account.AccountId, amountToTakeOut, transaction), "Account does not have sufficient funds.");
            Assert.AreEqual(sut.TransferMoney(account.AccountId, account2.AccountId, amountToTakeOut, transaction), "Account does not have sufficient funds.");
        }
        [TestMethod]
        public void Check_that_it_is_not_possible_to_insert_or_withdraw_negative_amounts_of_money()
        {
            var account = new Accounts();
            var account2 = new Accounts();
            account.Balance = 100M;
            mockContext.Accounts.Add(account);
            mockContext.Accounts.Add(account2);
            mockContext.SaveChanges();

            var amountOfMoney = -50M;

            var transaction = new TransactionReceipt
            {
            };
            Assert.AreEqual(sut.TakeOutMoney(account.AccountId, amountOfMoney, transaction), "Amount has to be more than 0.");
            Assert.AreEqual(sut.TransferMoney(account.AccountId, account2.AccountId, amountOfMoney, transaction), "Amount has to be more than 0.");
        }
        [TestMethod]
        public void Check_if_transactions_are_created_properly()
        {
            var fromAccount = new Accounts();
            var toAccount = new Accounts();
            fromAccount.Balance = 100M;

            mockContext.Accounts.Add(fromAccount);
            mockContext.Accounts.Add(toAccount);
            mockContext.SaveChanges();

            var fromAccountOriginalBalance = fromAccount.Balance;
            var toAccountOriginalBalance = fromAccount.Balance;

            var amountOfMoney = 50M;

            var transaction = new TransactionReceipt
            {
                Date = DateTime.Now,
                Bank = "AB",
                Amount = amountOfMoney,
                Operation = "Transfering money between accounts",
                Type = "Credit",
                FromAccount = fromAccount.AccountId,
                ToAccount = toAccount.AccountId,
                ToAccountBalance = toAccount.Balance,
                FromAccountBalance = fromAccount.Balance,
            };

            sut.TransferMoney(fromAccount.AccountId, toAccount.AccountId, amountOfMoney, transaction);

            Assert.AreEqual(mockContext.Transactions.Count(), 2);

            var toAccountTransaction = mockContext.Transactions.FirstOrDefault(r => r.AccountId == toAccount.AccountId);
            var fromAccountTransaction = mockContext.Transactions.FirstOrDefault(r => r.AccountId == fromAccount.AccountId);

            Assert.AreEqual(toAccountTransaction.Amount, amountOfMoney);
            Assert.AreEqual(toAccountTransaction.Balance, toAccount.Balance - amountOfMoney);
            Assert.AreEqual(fromAccountTransaction.Amount, -amountOfMoney);
            Assert.AreEqual(fromAccountTransaction.Balance, fromAccount.Balance + amountOfMoney);
        }
    }
}
