using GameStore.Models;

namespace GameStore.Services
{
	public class AuthService
	{
		public User? LoggedUser { get; set; }
		public void Login(User user)
		{
			LoggedUser = user;
		}

		public void Logout()
		{
			LoggedUser = null;
		}

		public bool IsLoggedIn()
		{
			if(LoggedUser == null)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		public User GetLoggedUser()
		{
			return LoggedUser;
		}
	}
}
