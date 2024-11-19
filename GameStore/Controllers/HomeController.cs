using GameStore.Interfaces;
using GameStore.Models;
using GameStore.Models.Pages;
using GameStore.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GameStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProduct _products;
        private readonly ICategory _categories;
        private readonly IUser _users;
        private readonly AuthService _authService;
        public HomeController(IProduct products, ICategory categories, IUser users, AuthService authService)
        {
            _products=products;
            _categories=categories;
            _users=users;
            _authService=authService;
        }
        public IActionResult UnLogin()
        {
            _authService.Logout();
            return RedirectToAction(nameof(Login));
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View(new User());
        }
        [HttpGet]
        public IActionResult Index(QueryOptions options, User user)
        {
            IEnumerable<Product> products = _products.GetProducts(options);
            User search = new User();
            if(_authService.GetLoggedUser()==null)
            {
                search = _users.GetUser(user.UserName);
            }
            else
            {
                search = _authService.GetLoggedUser();
            }

            if(search == null)
            {
                return RedirectToAction(nameof(Login));
            }
            else
            {
                _authService.Login(search);
            }

			return View(products);
        }

        [HttpGet]
        public IActionResult UpdateProduct(int id)
        {
            ViewBag.Categories = _categories.GetAllCategories();
            User user = _users.GetLoggedUser();
			ViewBag.User = _users.GetLoggedUser();
            return View(id==0 ? new Product() : _products.GetProduct(id));
        }
        [HttpPost]
        public IActionResult UpdateProduct(Product product)
        {
            ViewBag.User = _users.GetLoggedUser();
            if (product.Id==0)
            {
                _products.AddProduct(product);
            }
            else
            {
                _products.UpdateProduct(product);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult DeleteProduct(Product product)
        {
            ViewBag.User = _users.GetLoggedUser();
            _products.DeleteProduct(product);
            return RedirectToAction(nameof(Index));
        }
    }
}
