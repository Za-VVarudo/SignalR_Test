using Microsoft.AspNetCore.Mvc;

namespace SignalR_Test.Controllers
{
    [Route("")]
    [Route("[controller]")]
    public class ChatController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
