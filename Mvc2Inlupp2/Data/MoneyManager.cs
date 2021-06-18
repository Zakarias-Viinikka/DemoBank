using Mvc2Inlupp2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc2Inlupp2.Data
{
    public class MoneyManager : IMoneyManager
    {
        private ApplicationDbContext _dbc;
        public MoneyManager(ApplicationDbContext dbc)
        {
            _dbc = dbc;
        }
        public string TransferMoney(int fromAccount, int toAccount, decimal amountOfMoney, TransactionReceipt transaction)
        {
            var checkIfAccountsExistsResult = checkIfAccountsExists(fromAccount, toAccount);
            if (checkIfAccountsExistsResult == "bothExists")
            {
                if (checkIfAccountHasEnoughMoney(fromAccount, amountOfMoney))
                {
                    if (AmountIsAllowed(amountOfMoney))
                    {
                        moveMoney(transaction);
                        return "success";
                    }
                    return "Amount has to be more than 0.";
                }
                return "Account does not have sufficient funds.";
            }
            return checkIfAccountsExistsResult;
        }
        public string TakeOutMoney(int fromAccount, decimal amountOfMoney, TransactionReceipt transaction)
        {
            var checkIfAccountsExistsResult = checkIfAccountsExists(fromAccount, fromAccount);
            if (checkIfAccountsExistsResult == "bothExists")
            {
                if (checkIfAccountHasEnoughMoney(fromAccount, amountOfMoney))
                {
                    if (AmountIsAllowed(amountOfMoney))
                    {
                        moveMoney(transaction);
                        return "success";
                    }
                    return "Amount has to be more than 0.";
                }
                return "Account does not have sufficient funds.";
            }
            return checkIfAccountsExistsResult;
        }
        public string InsertMoney(int toAccount, decimal amountOfMoney, TransactionReceipt transaction)
        {
            var checkIfAccountsExistsResult = checkIfAccountsExists(toAccount, toAccount);
            if (checkIfAccountsExistsResult == "bothExists")
            {
                if (AmountIsAllowed(amountOfMoney))
                {
                    moveMoney(transaction);
                    return "success";
                }
                return "Amount withdrawn has to be more than 0.";
            }
            return checkIfAccountsExistsResult;
        }

        private bool checkIfAccountHasEnoughMoney(int account, decimal amount)
        {
            var newAccountBalance = _dbc.Accounts.FirstOrDefault(r => r.AccountId == account).Balance - amount;
            if (newAccountBalance > 0)
            {
                return true;
            }
            return false;
        }

        private bool AmountIsAllowed(decimal amount)
        {
            if (amount > 0)
            {
                return true;
            }
            return false;
        }

        private string checkIfAccountsExists(int fromAccount, int toAccount)
        {
            if (_dbc.Accounts.FirstOrDefault(r => r.AccountId == fromAccount) == null)
            {
                return "Account you are trying to move money from does not exist.";
            }
            if (_dbc.Accounts.FirstOrDefault(r => r.AccountId == toAccount) == null)
            {
                return "Account you are trying to insert money into does not exist.";
            }
            return "bothExists";
        }

        private void moveMoney(TransactionReceipt transaction)
        {
            if (transaction.FromAccount != 0)
            {
                int account = transaction.FromAccount;
                _dbc.Accounts.FirstOrDefault(r => r.AccountId == account).Balance -= transaction.Amount;

                transaction.Balance = transaction.FromAccountBalance;
                transaction.Amount = -transaction.Amount;
                createTransactionReceipt(account, transaction);
                transaction.Amount = -transaction.Amount;
            }
            if (transaction.ToAccount != 0)
            {
                int account = transaction.ToAccount;
                _dbc.Accounts.FirstOrDefault(r => r.AccountId == account).Balance += transaction.Amount;

                transaction.Balance = transaction.ToAccountBalance;
                createTransactionReceipt(account, transaction);
            }

            _dbc.SaveChanges();
        }

        private void createTransactionReceipt(int account, TransactionReceipt transaction)
        {
            _dbc.Transactions.Add(new Transactions
            {
                Date = transaction.Date,
                Bank = transaction.Bank,
                Symbol = transaction.Symbol,
                Amount = transaction.Amount,
                Operation = transaction.Operation,
                AccountId = account,
                Type = transaction.Type,
                Balance = transaction.Balance
            });
        }

    }
}
