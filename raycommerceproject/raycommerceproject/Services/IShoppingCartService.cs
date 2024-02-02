using raycommerceproject.Models;
using static raycommerceproject.Services.ShoppingCartService;

namespace raycommerceproject.Services
{
    public interface IShoppingCartService
    {
        ShoppingCartResult GetCartItems(int userId);
        ResultMessage AddToCart(int userId, int productId, int quantity);
        ResultMessage UpdateCartItemQuantity(int userId, int cartItemId, int quantity);
        ResultMessage RemoveFromCart(int userId, int cartItemId);
        ResultMessage ClearCart(int userId);
    }
}
