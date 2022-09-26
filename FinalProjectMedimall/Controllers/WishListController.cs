using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMedimall.Controllers
{
    public class WishListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
