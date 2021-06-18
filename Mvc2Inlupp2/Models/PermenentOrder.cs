using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Mvc2Inlupp2.Models
{
    public partial class PermenentOrder
    {
        public int OrderId { get; set; }
        public int AccountId { get; set; }
        public string BankTo { get; set; }
        public string AccountTo { get; set; }
        public decimal? Amount { get; set; }
        public string Symbol { get; set; }

        public virtual Accounts Account { get; set; }
    }
}
