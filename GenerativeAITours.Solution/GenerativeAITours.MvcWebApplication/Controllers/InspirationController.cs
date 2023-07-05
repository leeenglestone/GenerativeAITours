using Microsoft.AspNetCore.Mvc;

namespace GenerativeAITours.MvcWebApplication.Controllers
{
    public class InspirationController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Generative AI Tours - Get Inspired";

            return View();
        }
    }
}
