using AbilitySystem.BL;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AbilitySystem.API.Controllers.Cart
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartsManager _cartManager;

        public CartsController(ICartsManager cartManager)
        {
            _cartManager = cartManager;
        }

        [HttpGet("{userId}")]
        public ActionResult GetCartByUserId(string userId)
        {
            var cartItems = _cartManager.GetCartByUserId(userId);
            return Ok(cartItems);
        }
        [HttpPost]
        public ActionResult AddToCart(CartDto cartToAdd)
        {
            _cartManager.AddToCart(cartToAdd);
            return Ok(new { message = "Product added to cart successfully" });
        }

        [HttpDelete]
        public ActionResult RemoveFromCart(string userId, int productId)
        {
            removeCartDto cartToremoved = new removeCartDto(userId, productId);
            _cartManager.RemoveFromCart(cartToremoved); return Ok(new { message = "Product removed from cart successfully" });
        }
        [HttpDelete("{userId}")]
        public ActionResult EmptyUserCart(string userId)
        {
            _cartManager.EmptyUserCart(userId);
            return Ok(new { message = "Product removed from cart successfully" });
        }
        [HttpPatch]
        public ActionResult EditCart(editCartDto editCartRequest)
        {
            _cartManager.Edit(editCartRequest);


            return NoContent();
        }
        [HttpGet("count/{userId}")]
        public int? GetUserCartCount(string userId)
        {
            var count = _cartManager.GetUserCartCount(userId);
            return count;
        }



    }
}
