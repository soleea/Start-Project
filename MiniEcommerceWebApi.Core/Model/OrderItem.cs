using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniEcommerceWebApi.Core.Model
{
    public class OrderItem : Entity
    {
      
        public int OrderId { get; set; }
        public Order Order { get; set; }
      
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
       
        [NotMapped]
        public decimal Total => Quantity * UnitPrice;
    }
}
