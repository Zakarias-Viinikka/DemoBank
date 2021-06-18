using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc2Inlupp2.ViewModels
{
    public class CustomerAccountViewModel
    {
        public Account account { get; set; } = new Account();
        public class Account 
        {

            public int id { get; set; }
            public decimal balance { get; set; }
            public List<Transaction> transactions { get; set; } = new List<Transaction>();
        }

        public class Transaction
        {
            public DateTime date { get; set; }
            public string type { get; set; }
            public string operation { get; set; }
            public decimal amount { get; set; }
            public string bank { get; set; }
        }
    }
}

