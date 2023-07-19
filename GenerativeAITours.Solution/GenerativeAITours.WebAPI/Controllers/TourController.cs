using Microsoft.AspNetCore.Mvc;

namespace GenerativeAITours.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TourController : ControllerBase
    {
        private readonly ILogger<TourController> _logger;

        public TourController(ILogger<TourController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "GetTour")]
        public async Task<ActionResult<string>> GetAsync([FromBody] string prompt)
        {
            try
            {
                Tour tour = null;

                IPromptCache promptCache = new PromptCacheLocalFileStorage();
                var tourFromCache = promptCache.GetCachedResponse(prompt);

                // If retrieved from cache
                if (tourFromCache != null)
                {
                    tour = Tour.ParseResult(tourFromCache);
                }
                else
                {
                    tour = await OpenAI.GetTourFromOpenAIAsync(prompt, promptCache);
                }

                await ImageCache.SaveActivityImagesIfDontExistAsync(tour);

                return Ok(tour);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }        
    }
}