using Microsoft.EntityFrameworkCore;
using raycommerceproject.Data;
using raycommerceproject.Models;

namespace raycommerceproject.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly AppDbContext _dbContext;

        public ShoppingCartService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ShoppingCartResult GetCartItems(int userId)
        {
            var userShoppingCart = _dbContext.UserShoppingCarts
                .Include(usc => usc.ShoppingCartItems)
                .FirstOrDefault(usc => usc.UserId == userId);

            if (userShoppingCart == null)
            {
                return new ShoppingCartResult
                {
                    Success = false,
                    Message = "Shopping cart not found",
                    CartItems = new List<ShoppingCartItem>()
                };
            }

            var cartItems = userShoppingCart.ShoppingCartItems;

            if (cartItems == null || cartItems.Count == 0)
            {
                return new ShoppingCartResult
                {
                    Success = true,
                    Message = "No products found in the cart",
                    CartItems = new List<ShoppingCartItem>()
                };
            }

            return new ShoppingCartResult
            {
                Success = true,
                Message = "Products retrieved successfully",
                CartItems = cartItems
            };
        }


        public ResultMessage AddToCart(int userId, int productId, int quantity)
        {
            var userShoppingCart = _dbContext.UserShoppingCarts
                .Include(usc => usc.ShoppingCartItems)
                .FirstOrDefault(usc => usc.UserId == userId);

            if (userShoppingCart == null)
            {
                userShoppingCart = new UserShoppingCart
                {
                    UserId = userId,
                    ShoppingCartItems = new List<ShoppingCartItem>()
                };
                _dbContext.UserShoppingCarts.Add(userShoppingCart);
            }

            var product = _dbContext.Products.Find(productId);

            if (product == null)
            {
                return new ResultMessage { Success = false, Message = "Product not found" }; ;
            }

            var shoppingCartItem = new ShoppingCartItem
            {
                ProductId = productId,
                Quantity = quantity,
                Price = product.Price,
            };

            userShoppingCart.ShoppingCartItems.Add(shoppingCartItem);
            _dbContext.SaveChanges();

            return new ResultMessage { Success = true, Message = "Product added to cart successfully" };
        }

        public ResultMessage UpdateCartItemQuantity(int userId, int cartItemId, int quantity)
        {
            var userShoppingCart = _dbContext.UserShoppingCarts
                .Include(usc => usc.ShoppingCartItems)
                .FirstOrDefault(usc => usc.UserId == userId);

            if (userShoppingCart == null)
            {
                return new ResultMessage { Success = false, Message = "User's shopping cart not found" };
            }

            var cartItem = userShoppingCart.ShoppingCartItems.FirstOrDefault(item => item.Id == cartItemId);

            if (cartItem == null)
            {
                return new ResultMessage { Success = false, Message = "Cart item not found in user's shopping cart" };
            }

            cartItem.Quantity = quantity;
            _dbContext.SaveChanges();

            return new ResultMessage { Success = true, Message = "Cart item quantity updated successfully" };
        }

        public ResultMessage RemoveFromCart(int userId, int cartItemId)
        {
            var userShoppingCart = _dbContext.UserShoppingCarts
                .Include(usc => usc.ShoppingCartItems)
                .FirstOrDefault(usc => usc.UserId == userId);

            if (userShoppingCart == null)
            {
                return new ResultMessage { Success = false, Message = "User's shopping cart not found" };
            }

            var cartItemToRemove = userShoppingCart.ShoppingCartItems.FirstOrDefault(item => item.Id == cartItemId);

            if (cartItemToRemove == null)
            {
                return new ResultMessage { Success = false, Message = "Cart item not found" };
            }

            userShoppingCart.ShoppingCartItems.Remove(cartItemToRemove);
            _dbContext.SaveChanges();

            return new ResultMessage { Success = true, Message = "Product removed from cart successfully" };
        }

        public ResultMessage ClearCart(int userId)
        {
            var userShoppingCart = _dbContext.UserShoppingCarts
                .Include(usc => usc.ShoppingCartItems)
                .FirstOrDefault(usc => usc.UserId == userId);

            if (userShoppingCart == null)
            {
                return new ResultMessage { Success = false, Message = "Shopping cart not found" };
            }

            // Clear all items from the shopping cart
            userShoppingCart.ShoppingCartItems.Clear();
            _dbContext.SaveChanges();

            return new ResultMessage { Success = true, Message = "Shopping cart cleared successfully" };
        }

        public class ResultMessage
        {
            public bool Success { get; set; }
            public string Message { get; set; }
        }

        public class ShoppingCartResult : ResultMessage
        {
            public List<ShoppingCartItem> CartItems { get; set; }
        }
    }
}
