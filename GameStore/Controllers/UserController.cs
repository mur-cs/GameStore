using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
