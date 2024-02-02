namespace raycommerceproject.Models
{
    public class ShoppingCartItemRequest
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }
    }

}
