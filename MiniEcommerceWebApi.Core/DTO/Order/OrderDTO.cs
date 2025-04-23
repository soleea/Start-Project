using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniEcommerceWebApi.Core.DTO.Order
{
    public class OrderDTO : DTO
    {

        public int CustomerId { get; set; }

        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }


        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        public string OrderStatus { get; set; }

    }

}
