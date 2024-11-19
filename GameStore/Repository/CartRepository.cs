using GameStore.Data;
using GameStore.Interfaces;
using GameStore.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Repository
{
    public class CartRepository : ICart
    {
        private ApplicationContext _context;
        public CartRepository(ApplicationContext context)
        {
            _context = context;
        }
        public void AddCart(Cart cart)
        {
            _context.Carts.Add(cart);
            _context.SaveChanges();
        }

        public IEnumerable<Cart> GetAllCarts()
        {
            return _context.Carts.Include(x=>x.User).Include(x=>x.Product);
        }

        public Cart GetCart(int userId)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.UserId == userId);
            return cart;
        }

        public void RemoveCart(int cartId)
        {
            _context.Carts.Remove(_context.Carts.FirstOrDefault(x=>x.Id==cartId));
            _context.SaveChanges();
        }
    }
}
