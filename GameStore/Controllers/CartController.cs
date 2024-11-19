using GameStore.Data;
using GameStore.Interfaces;
using GameStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    public class CartController : Controller
    {
        private ICart _cart;
        public CartController(ICart cart)
        {
            _cart=cart;
        }
        [HttpPost]
        public IActionResult Index(int userId, int productId)
        {
            _cart.AddCart(new Cart()
            {
                UserId = userId,
                ProductId = productId
            });
            var result = _cart.GetAllCarts().Where(x=>x.UserId == userId).ToList();
			return RedirectToAction(nameof(OpenCart), new { userId = userId });
		}
        [HttpGet]
        public IActionResult OpenCart(int userId)
        {
            var result = _cart.GetAllCarts().Where(x => x.UserId == userId).ToList();
            return View(result);
        }

        public IActionResult Delete(int cartId, int userId)
        {
            _cart.RemoveCart(cartId);
            return RedirectToAction(nameof(OpenCart), new { userId = userId });
        }
    }
}
