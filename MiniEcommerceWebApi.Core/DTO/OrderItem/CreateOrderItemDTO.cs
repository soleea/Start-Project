using MiniEcommerceWebApi.Core.Interface;

using System.ComponentModel.DataAnnotations;

namespace MiniEcommerceWebApi.Core.DTO.OrderItem
{
    public class CreateOrderItemDTO:  IDTONoID
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
