using GameStore.Models;

namespace GameStore.Interfaces
{
    public interface ICart
    {
        void AddCart(Cart cart);
        void RemoveCart(int cartId);
        Cart GetCart(int userId);
        IEnumerable<Cart> GetAllCarts();
    }
}
