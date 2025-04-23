using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniEcommerceWebApi.Core.Model
{
    public class Customer : Entity
    {
        public string CustomerName { get; set; }
        public string Email { get; set; }
       
        public string Password { get; set; }
        public string Phone { get; set; }
        public string BillingAddress { get; set; }
        public List<Order> Orders { get; set; }
    }
}
