﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniEcommerceWebApi.Core.Model
{
    public class Customer : Entity
    {
        public string CustomerName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string BillingAddress { get; set; } = null!;


        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
