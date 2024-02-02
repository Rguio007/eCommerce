namespace raycommerceproject.Models
{
    public class UserShoppingCart
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
