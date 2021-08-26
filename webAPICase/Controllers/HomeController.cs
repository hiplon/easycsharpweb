using Microsoft.AspNetCore.Mvc;

namespace webAPICase.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        // GET
        [HttpGet()]
        public ActionResult Index()
        {
            return new Microsoft.AspNetCore.Mvc.ContentResult
            {
                Content = "Hi there! From Guide Index ☺",
                ContentType = "text/plain; charset=utf-8"
            };
        }
    }
}