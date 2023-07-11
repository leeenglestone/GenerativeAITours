using GenerativeAITours.MvcWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace GenerativeAITours.MvcWebApplication.Controllers
{
    public class TourController : Controller
    {
        

        [Route("{country}/{city}")]
        public IActionResult Index(string country, string city)
        {
            TextInfo txtInfo = new CultureInfo("en-US").TextInfo;

            var model = new TourViewModel();
            model.Country = txtInfo.ToTitleCase(country);
            model.City = txtInfo.ToTitleCase(city);

            return View(model);
        }
    }
}
