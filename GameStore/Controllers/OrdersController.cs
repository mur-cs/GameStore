using GameStore.Interfaces;
using GameStore.Models;
using GameStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    public class OrdersController : Controller
    {
        private IProduct _products;
        private IOrder _orders;
        
        private AuthService _authService;
        private ICart _cart;
        public OrdersController(IProduct products, IOrder orders, AuthService authService, ICart cart)
        {
            _products=products;
            _orders=orders;
            _authService=authService;
            _cart=cart;
        }


        public IActionResult Index()
        {
            if (_authService.GetLoggedUser().UserName=="admin")
            {
                return View(_orders.GetAllOrders().ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult EditOrder(int id)
        {
            var products = _products.GetAllProducts();
            Order order = id == 0 ? new Order() : _orders.GetOrder(id);
            IDictionary<int, OrderLine> lineMaps = order.Lines?.ToDictionary(x => x.ProductId) ?? new Dictionary<int, OrderLine>();
            ViewBag.Lines = products.Select(x => lineMaps.ContainsKey(x.Id) ? lineMaps[x.Id] : new OrderLine()
            {
                Product=x,
                ProductId=x.Id,
                Quantity=0
            });
            if (_authService.GetLoggedUser().UserName.ToLower()=="admin")
            {
                return View(order);

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public IActionResult AddOrUpdateOrder(Order order)
        { 
            order.UserId = _authService.GetLoggedUser().Id;

            if (order.Lines == null)
            {
                order.Lines = new List<OrderLine>();
            }
            else
            {
                order.Lines = order.Lines
                    .Where(x => x.Id > 0 || (x.Id == 0 && x.Quantity > 0))
                    .ToList();
            }

            if (order.Id == 0)
            {

                var carts = _cart.GetAllCarts()
                    .Where(x => x.UserId == order.UserId)
                    .ToList();
                foreach (var cart in carts)
                {
                    order.Lines.ToList().Add(new OrderLine
                    {
                        ProductId = cart.ProductId,
                        Quantity = 1, 
                        OrderId = order.Id
                    });

                    _cart.RemoveCart(cart.Id);
                }
                _orders.AddOrder(order);
            }
            else
            {
                _orders.UpdateOrder(order);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult DeleteOrder(Order order)
        {
            _orders.DeleteOrder(order);
            return RedirectToAction(nameof(Index));
        }
    }
}
