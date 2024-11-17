using GameStore.Data;
using GameStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Controllers
{
	public class SeedController : Controller
	{
		private readonly ApplicationContext _context;
		public SeedController(ApplicationContext context)
		{
			_context=context;
		}

		public IActionResult Index()
		{
			ViewBag.Count = _context.Products.Count();
			return View(_context.Products.Include(x => x.Category).OrderBy(x => x.Id).Take(20));
		}

		[HttpPost]
		public IActionResult ClearData()
		{
			_context.Database.SetCommandTimeout(TimeSpan.FromMinutes(10));
			_context.Database.BeginTransaction();
			_context.Database.ExecuteSqlRaw("DELETE FROM Products");
			_context.Database.ExecuteSqlRaw("DELETE FROM Categories");
			_context.Database.CommitTransaction();
			return RedirectToAction("Index");
		}

		[HttpPost]
		public IActionResult CreateSeedData()
		{
			ClearData();

				List<Category> categories = new List<Category>()
					{
						new Category { Name = "Electronics", Description = "Devices and gadgets" },
						new Category { Name = "Home Appliances", Description = "Household equipment" },
						new Category { Name = "Books", Description = "Various genres of books" },
						new Category { Name = "Clothing", Description = "Men's and women's clothing" },
						new Category { Name = "Toys", Description = "Toys for children of all ages" },
						new Category { Name = "Sports Equipment", Description = "Gear for various sports" },
						new Category { Name = "Groceries", Description = "Food and beverage items" },
						new Category { Name = "Beauty Products", Description = "Skincare and makeup" },
						new Category { Name = "Furniture", Description = "Home and office furniture" },
						new Category {  Name = "Automotive", Description = "Car parts and accessories" }
					};

				_context.Categories.AddRange(categories);
				_context.SaveChanges();

				List<Product> products = new List<Product>()
				{
					new Product { Name = "Smartphone", CategoryId = 1, PurchasePrice = 300.00m, RetailPrice = 450.00m },
			new Product { Name = "Laptop", CategoryId = 1, PurchasePrice = 800.00m, RetailPrice = 1100.00m },
			new Product { Name = "Headphones", CategoryId = 1, PurchasePrice = 50.00m, RetailPrice = 80.00m },
			new Product { Name = "4K TV", CategoryId = 1, PurchasePrice = 500.00m, RetailPrice = 700.00m },
			new Product { Name = "Microwave Oven", CategoryId = 2, PurchasePrice = 100.00m, RetailPrice = 150.00m },
			new Product { Name = "Refrigerator", CategoryId = 2, PurchasePrice = 600.00m, RetailPrice = 850.00m },
			new Product { Name = "Washing Machine", CategoryId = 2, PurchasePrice = 400.00m, RetailPrice = 550.00m },
			new Product { Name = "Vacuum Cleaner", CategoryId = 2, PurchasePrice = 150.00m, RetailPrice = 220.00m },
			new Product { Name = "Cookbook", CategoryId = 3, PurchasePrice = 15.00m, RetailPrice = 25.00m },
			new Product {  Name = "Science Fiction Novel", CategoryId = 3, PurchasePrice = 10.00m, RetailPrice = 20.00m },
			new Product {  Name = "Thriller Book", CategoryId = 3, PurchasePrice = 12.00m, RetailPrice = 18.00m },
			new Product {  Name = "History Book", CategoryId = 3, PurchasePrice = 20.00m, RetailPrice = 30.00m },
			new Product {  Name = "Men's T-Shirt", CategoryId = 4, PurchasePrice = 8.00m, RetailPrice = 15.00m },
			new Product {  Name = "Women's Dress", CategoryId = 4, PurchasePrice = 20.00m, RetailPrice = 40.00m },
			new Product {  Name = "Jeans", CategoryId = 4, PurchasePrice = 25.00m, RetailPrice = 50.00m },
			new Product {  Name = "Children's Toy Car", CategoryId = 5, PurchasePrice = 10.00m, RetailPrice = 18.00m },
			new Product {  Name = "Doll", CategoryId = 5, PurchasePrice = 15.00m, RetailPrice = 25.00m },
			new Product {  Name = "Board Game", CategoryId = 5, PurchasePrice = 20.00m, RetailPrice = 35.00m },
			new Product {  Name = "Soccer Ball", CategoryId = 6, PurchasePrice = 15.00m, RetailPrice = 30.00m },
			new Product {  Name = "Tennis Racket", CategoryId = 6, PurchasePrice = 40.00m, RetailPrice = 65.00m },
			new Product {  Name = "Protein Powder", CategoryId = 6, PurchasePrice = 25.00m, RetailPrice = 45.00m },
			new Product {  Name = "Pasta", CategoryId = 7, PurchasePrice = 2.00m, RetailPrice = 4.00m },
			new Product {  Name = "Olive Oil", CategoryId = 7, PurchasePrice = 5.00m, RetailPrice = 8.00m },
			new Product {  Name = "Face Cream", CategoryId = 8, PurchasePrice = 12.00m, RetailPrice = 20.00m },
			new Product {  Name = "Lipstick", CategoryId = 8, PurchasePrice = 7.00m, RetailPrice = 15.00m },
			new Product {  Name = "Office Chair", CategoryId = 9, PurchasePrice = 50.00m, RetailPrice = 80.00m },
			new Product {  Name = "Wooden Table", CategoryId = 9, PurchasePrice = 120.00m, RetailPrice = 200.00m },
			new Product {  Name = "Car Battery", CategoryId = 10, PurchasePrice = 70.00m, RetailPrice = 120.00m },
			new Product {  Name = "Windshield Wipers", CategoryId = 10, PurchasePrice = 10.00m, RetailPrice = 20.00m }
				};


				
				_context.Products.AddRange(products);
				_context.SaveChanges();
			
			return RedirectToAction(nameof(Index));
		}
	}
}
