using Mvc2Inlupp2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc2Inlupp2.Data
{
    public interface IMoneyManager
    {
        string TransferMoney(int fromAccount, int toAccount, decimal amountOfMoney, TransactionReceipt transaction);
        string TakeOutMoney(int fromAccount, decimal amountOfMoney, TransactionReceipt transaction);
        string InsertMoney(int toAccount, decimal amountOfMoney, TransactionReceipt transaction);
    }
    public class TransactionReceipt
    {
        public DateTime Date { get; set; }
        public string Bank { get; set; }
        public string Symbol { get; set; }
        public decimal Amount { get; set; }
        public string Operation { get; set; }
        public string Type { get; set; }
        public int FromAccount { get; set; }
        public decimal FromAccountBalance { get; set; }
        public int ToAccount { get; set; }
        public decimal ToAccountBalance { get; set; }
        public decimal Balance { get; set; }
    }
}
