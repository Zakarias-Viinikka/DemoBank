using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc2Inlupp2.ViewModels
{
    public class CustomerViewModel
    {
        public Customer customer { get; set; } = new Customer();
        public class Customer
        {
            public int id { get; set; }
            public List<Accounts> accounts { get; set; }
            public string name { get; set; }
            public string gender { get; set; }
            public string address { get; set; }
            public string city { get; set; }
            public string country { get; set; }
            public string ssn { get; set; }
            public int sumOfAllAccounts { get; set; }
            public string telephoneNumber { get; set; }
            public string email { get; set; }
            public decimal totalBalance { get; set; }
        }
        public class Accounts
        {
            public int id { get; set; }
            public decimal balance { get; set; }
            public string dispositionType { get; set; }
        }
    }
}
