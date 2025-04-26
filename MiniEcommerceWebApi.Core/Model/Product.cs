using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniEcommerceWebApi.Core.Model
{
    public class Product : Entity
    {
        public string ProductName { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal UnitPrice { get; set; }


        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
