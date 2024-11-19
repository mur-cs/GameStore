using GameStore.Data;
using GameStore.Interfaces;
using GameStore.Models;

namespace GameStore.Repository
{
    public class UserRepository : IUser
    {
        private ApplicationContext _context;
        private User? _LoggedUser;

        public UserRepository(ApplicationContext context)
        {
            _context=context;
        }
        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users;
        }

        public User GetLoggedUser()
        {
            return _LoggedUser;
        }

        public User GetUser(string username)
        {
            return _context.Users.FirstOrDefault(x => x.UserName == username);
        }

        public void SetLoggedUser(User user)
        {
            _LoggedUser = user;
        }
    }
}
