using Microsoft.AspNetCore.Mvc;
using raycommerceproject.Models;
using raycommerceproject.Services;

namespace raycommerceproject.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpGet("{userId}")]
        public IActionResult GetCartItems(int userId)
        {
            var result = _shoppingCartService.GetCartItems(userId);

            if (result.Success)
            {
                return Ok(new { Success = true, Message = result.Message, CartItems = result.CartItems });
            }

            return BadRequest(new { Success = false, Message = result.Message, CartItems = new List<ShoppingCartItem>() });
        }

        [HttpPost("add/{userId}")]
        public IActionResult AddToCart(int userId, [FromBody] ShoppingCartItemRequest request)
        {
            var result = _shoppingCartService.AddToCart(userId, request.ProductId, request.Quantity);

            if (result.Success)
            {
                return Ok(new { Success = true, Message = result.Message });
            }

            return BadRequest(new { Success = false, Message = result.Message });
        }

        [HttpPut("update/{cartItemId}")]
        public IActionResult UpdateCartItemQuantity(int cartItemId, [FromBody] ShoppingCartItemRequest request)
        {
            var result = _shoppingCartService.UpdateCartItemQuantity(request.UserId,cartItemId, request.Quantity);

            if (result.Success)
            {
                return Ok(new { Success = true, Message = result.Message });
            }

            return BadRequest(new { Success = false, Message = result.Message });
        }

        [HttpDelete("remove/{userId}/{cartItemId}")]
        public IActionResult RemoveFromCart(int userId, int cartItemId)
        {
            var result = _shoppingCartService.RemoveFromCart(userId, cartItemId);

            if (result.Success)
            {
                return Ok(new { Success = true, Message = result.Message });
            }

            return BadRequest(new { Success = false, Message = result.Message });
        }

        [HttpPost("clear/{userId}")]
        public IActionResult ClearCart(int userId)
        {
            var result = _shoppingCartService.ClearCart(userId);

            if (result.Success)
            {
                return Ok(new { Success = true, Message = result.Message });
            }

            return BadRequest(new { Success = false, Message = result.Message });
        }
    }
    }
