using Microsoft.AspNetCore.Mvc;

namespace DannT.Controllers
{
    public class TaskController : Controller
    {
        [HttpGet("Create")]
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
