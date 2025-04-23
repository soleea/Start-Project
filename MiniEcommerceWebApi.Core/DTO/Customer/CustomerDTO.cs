namespace MiniEcommerceWebApi.Core.DTO.Customer
{
    public class CustomerDTO : DTO
    {

        public string CustomerName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
        public string Phone { get; set; }
        public string BillingAddress { get; set; }

    }
}
