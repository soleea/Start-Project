using System.ComponentModel.DataAnnotations;

namespace MiniEcommerceWeb.Models
{
    public class CreateOrderDTO 
    {
        [Required]
        public List<CreateOrderItem> OrderItems { get; set; }

    }

    public class CreateOrderItem 
    {

        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
