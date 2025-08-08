using Microsoft.AspNetCore.Mvc;

namespace DannT.Controllers
{
    public class FeedController : Controller
    {
        [HttpGet("/Feed")]
        public IActionResult Feed()
        {
            return View();
        }
    }
}
