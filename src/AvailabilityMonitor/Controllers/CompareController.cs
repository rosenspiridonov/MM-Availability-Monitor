namespace AvailabilityMonitor.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class CompareController : Controller
    {
        public IActionResult ChangeStockStatus()
        {
            return View();
        }

        public IActionResult NewProducts()
        {
            return View();
        }
    }
}
