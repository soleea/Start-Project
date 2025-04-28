using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MiniEcommerceWebApi.Core.Model
{
    public class Order : Entity
    {

        public int CustomerId { get; set; }

        public string OrderNumber { get; set; } = null!;

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public string OrderStatus { get; set; }

        public virtual Customer Customer { get; set; }

       
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
       
}
