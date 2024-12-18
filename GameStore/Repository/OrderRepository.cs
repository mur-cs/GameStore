﻿using GameStore.Data;
using GameStore.Interfaces;
using GameStore.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Repository
{
	public class OrderRepository : IOrder
	{
		private ApplicationContext _context;
		public OrderRepository(ApplicationContext context)
		{
			_context = context;
		}

		public void AddOrder(Order order)
		{
			_context.Orders.Add(order);
			_context.OrderLines.AddRange(order.Lines);
			_context.SaveChanges();
		}

		public void DeleteOrder(Order order)
		{
			_context.Orders.Remove(order);	
			_context.SaveChanges();
		}

		public IEnumerable<Order> GetAllOrders()
		{
			return _context.Orders.Include(x=>x.Lines).ThenInclude(x=>x.Product);
		}

		public Order GetOrder(int id)
		{
			return _context.Orders.Include(x => x.Lines).FirstOrDefault(x => x.Id==id);
		}

		public void UpdateOrder(Order order)
		{
			_context.Orders.Update(order);
			_context.SaveChanges();
		}
	}
}
