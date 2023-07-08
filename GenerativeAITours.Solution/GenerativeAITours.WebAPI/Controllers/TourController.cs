using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Text;

namespace GenerativeAITours.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TourController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        private readonly ILogger<TourController> _logger;

        public TourController(ILogger<TourController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
        }

        //[EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost(Name = "GetTour")]
        public async Task<ActionResult<string>> GetAsync([FromBody] string prompt)
        {
            try
            {
                var promptCache = new PromptCacheLocalFileStorage();

                var cachedResponse = promptCache.GetCachedResponse(prompt);

                if (cachedResponse != null)
                {
                    var tour = Tour.ParseResult(cachedResponse);

                    foreach (var activity in tour.Days.SelectMany(x => x.Activities))
                    {
                        var activityName = activity.Name;

                        // Get images for tour activities
                        // Check if file exists

                        string locationActivity = tour.Location + " " + activityName;
                        if (!ActivityImageExists(locationActivity))
                        {
                            await SaveActivityImageAsync(locationActivity);
                        }
                    }

                    return Ok(tour);
                }

                string apiKey = "";
                string model = "text-davinci-003";
                //string model = "gpt-3.5-turbo"; 
                string apiUrl = $"https://api.openai.com/v1/engines/{model}/completions";
                //string apiUrl = "https://api.openai.com/v1/chat/completions";

                var requestPayload = new
                {
                    prompt = prompt,
                    //max_tokens = 500,
                    //max_tokens = 1000,
                    //model = "gpt-3.5-turbo",
                    max_tokens = 2000,
                    temperature = 0.7,
                    n = 1
                };

                var jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(requestPayload);
                var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                var openAIResponse = await _httpClient.PostAsync(apiUrl, httpContent);

                if (openAIResponse.IsSuccessStatusCode)
                {
                    var openAIResponseContent = await openAIResponse.Content.ReadAsStringAsync();
                    var resultText = Tour.GetResultFromOpenAI(openAIResponseContent);

                    promptCache.SavePromptResponse(promptCache.HashPrompt(prompt), resultText);

                    var tour = Tour.ParseResult(resultText);

                    return Ok(tour);
                }
                else
                {
                    return BadRequest("API request failed.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        private bool ActivityImageExists(string activityName)
        {
            //var path = $@"c:\temp\genaitours\{activityName.ToLower().Trim()}.jpg";
            var path = $@"C:\Development\GenerativeAITours\GenerativeAITours.Solution\GenerativeAITours.MvcWebApplication\wwwroot\activities\{activityName.ToLower().Trim()}.jpg";

            return System.IO.File.Exists(path);
        }

        private async Task SaveActivityImageAsync(string activityName)
        {
            var unsplashAccessKey = "iNAJIV5KuG9mjNsVU7NULfuMQmrybkQpExkcO2e_gfU";

            var unsplashApiUrl = $"https://api.unsplash.com/search/photos?client_id={unsplashAccessKey}&page={1}&per_page={1}&orientation=landscape&query={activityName}";

            var client = new WebClient();
            var response = await client.DownloadStringTaskAsync(unsplashApiUrl);

            dynamic array = JsonConvert.DeserializeObject(response);

            List<string> urls = new List<string>();

            foreach(var result in array["results"])
            {
                urls.Add(result.urls.regular.ToString());
            }

            //var firstImage = array["results"].First();
            var imageUrl = urls[0];

            byte[] imageBytes = await client.DownloadDataTaskAsync(imageUrl);

            var path = $@"C:\Development\GenerativeAITours\GenerativeAITours.Solution\GenerativeAITours.MvcWebApplication\wwwroot\activities\{activityName.ToLower().Trim()}.jpg";

            System.IO.File.WriteAllBytes(path, imageBytes);

        }
    }
}