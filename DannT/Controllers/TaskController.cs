using Microsoft.AspNetCore.Mvc;

namespace DannT.Controllers
{
    public class TaskController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }
        [HttpGet("Task/{id}")]
        public IActionResult Show() {
            return View();
        }

    }
}
