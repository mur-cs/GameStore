using GameStore.Models;

namespace GameStore.Interfaces
{
    public interface IUser
    {
        IEnumerable<User> GetAllUsers();
        User GetUser(string username);
        void SetLoggedUser(User user);
        User GetLoggedUser();
    }
}
