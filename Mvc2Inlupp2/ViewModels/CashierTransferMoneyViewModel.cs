using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc2Inlupp2.ViewModels
{
    public class CashierTransferMoneyViewModel
    {
        //public Account fromAccount { get; set; } = new Account();
        //public Account toAccount { get; set; } = new Account();
        [Required]
        public int fromAccount { get; set; }
        [Required]
        public int toAccount { get; set; }
        [Required]
        [StringLength(50)]
        public string type { get; set; }
        public string operation { get; set; }
        public string symbol { get; set; }
        public string? bank { get; set; }
        [Required]
        [Range(0.000000000000000001, Double.PositiveInfinity, ErrorMessage = "Amount has to be more than 0")]
        public decimal amount { get; set; }
        /*public class Account
        {
            public int accountId { get; set; }
        }*/
    }
}
