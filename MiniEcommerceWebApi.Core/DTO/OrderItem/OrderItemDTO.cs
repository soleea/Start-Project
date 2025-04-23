using MiniEcommerceWebApi.Core.DTO.Order;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniEcommerceWebApi.Core.DTO.OrderItem
{
    public class OrderItemDTO : DTO
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public OrderDTO Order { get; set; }

        public int ProductId { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        [NotMapped]
        public decimal Total => Quantity * UnitPrice;
    }
}
