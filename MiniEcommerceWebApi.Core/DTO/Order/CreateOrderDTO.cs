using MiniEcommerceWebApi.Core.Interface;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniEcommerceWebApi.Core.DTO.Order
{
    public class CreateOrderDTO : IDTONoID
    {
        [Required]
        public List<CreateOrderItem> OrderItems { get; set; }

    }

    public class CreateOrderItem : IDTONoID
    {

        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }


}
