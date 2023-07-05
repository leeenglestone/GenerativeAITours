using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<string>> GetAsync([FromBody]string prompt)
        {
            try
            {
                string apiKey = "<Insert Key>";
                string model = "text-davinci-003";
                string apiUrl = $"https://api.openai.com/v1/engines/{model}/completions";

                var requestPayload = new
                {
                    prompt = prompt,
                    max_tokens = 500,
                    temperature = 0.7,
                    n = 1
                };

                var jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(requestPayload);
                var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                var response = await _httpClient.PostAsync(apiUrl, httpContent);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    //var resultText = Tour.GetResultFromOpenAI(responseContent);
                    //var tour = Tour.ParseResult(resultText);

                    //return Ok(tour);
                    return Ok(responseContent);

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

            // Get secret credentails

            // Call Open API

            // Get results

            // Convert into Tour details
            //

            //return tour;
        }
    }
}