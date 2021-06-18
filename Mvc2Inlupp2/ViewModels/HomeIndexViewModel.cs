using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc2Inlupp2.ViewModels
{
    public class HomeIndexViewModel
    {
        public int numberOfAccounts { get; set; }
        public int numberOfCustomers { get; set; }
        public decimal sumOfBalanceFromAllAccounts { get; set; }
    }
}
