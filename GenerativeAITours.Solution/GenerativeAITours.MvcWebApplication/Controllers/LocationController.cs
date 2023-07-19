using GenerativeAITours.Library;
using GenerativeAITours.MvcWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace GenerativeAITours.MvcWebApplication.Controllers
{
    public class LocationController : Controller
    {
        

        [Route("{country}/{city}")]
        public IActionResult Index(string country, string city)
        {
            TextInfo txtInfo = new CultureInfo("en-US").TextInfo;

            var titleCaseCity = txtInfo.ToTitleCase(city);
            var titleCaseCountry = txtInfo.ToTitleCase(country);

            var model = new LocationViewModel();
            model.Country = titleCaseCountry;
            model.City = titleCaseCity;

            // Compose prompt
            var prompt = Prompt.Build(titleCaseCountry, titleCaseCity, daysDuration: 3, new[] { "Culture" });

            // Hash prompt
            var hashedPrompt = Prompt.HashPrompt(prompt);

            // Retrieve from cache

            
            // Or Open AI, then save

            // Retrieve images




            return View(model);
        }
    }
}
