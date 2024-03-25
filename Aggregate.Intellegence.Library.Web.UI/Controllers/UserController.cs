using Microsoft.AspNetCore.Mvc;

namespace Aggregate.Intellegence.Library.Web.UI.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
