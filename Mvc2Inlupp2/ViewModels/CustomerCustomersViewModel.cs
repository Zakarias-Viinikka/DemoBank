using JW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc2Inlupp2.ViewModels
{
    public class CustomerCustomersViewModel
    {
        public string? q { get; set; }
        public string? orderBy { get; set; }
        public bool? descending { get; set; }

        public List<Customers> customers = new List<Customers>();
        public class Customers
        {
            public int id { get; set; }
            public string ssn { get; set; }
            public string customerName { get; set; }
            public string city { get; set; }
            public string address { get; set; }
        }

        public Pager pager { get; set; }
    }
}
