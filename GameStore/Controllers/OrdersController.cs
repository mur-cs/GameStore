using GameStore.Interfaces;
using GameStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
	public class OrdersController : Controller
	{
		private IProduct _products;
		private IOrder _orders;
		public OrdersController(IProduct products, IOrder orders)
		{
			_products=products;
			_orders=orders;
		}


		public IActionResult Index()
		{
			return View(_orders.GetAllOrders());
		}

		public IActionResult EditOrder(int id)
		{
			var products = _products.GetAllProducts();
			Order order = id == 0 ? new Order() : _orders.GetOrder(id);
			IDictionary<int, OrderLine> lineMaps = order.Lines?.ToDictionary(x=>x.ProductId) ?? new Dictionary<int, OrderLine>();
			ViewBag.Lines = products.Select(x => lineMaps.ContainsKey(x.Id) ? lineMaps[x.Id] : new OrderLine()
			{
				Product=x,
				ProductId=x.Id,
				Quantity=0
			});
			return View(order);
		}
		[HttpPost]
		public IActionResult AddOrUpdateOrder(Order order)
		{
			order.Lines=order.Lines.Where(x => x.Id > 0 || (x.Id==0 && x.Quantity>0)).ToArray();
			if(order.Id==0)
			{
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
