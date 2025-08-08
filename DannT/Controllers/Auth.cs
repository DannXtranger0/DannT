using Microsoft.AspNetCore.Mvc;

namespace DannT.Controllers
{
    public class Auth : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult RequestPassword()
        {
            return View();
        }

    }
}
