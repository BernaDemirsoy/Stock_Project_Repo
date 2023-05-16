using Microsoft.AspNetCore.Mvc;

namespace StockProject.UI.Areas.Supplier.Controllers
{
    [Area("Supplier")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
