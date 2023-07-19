using GenerativeAITours.Library;
using Newtonsoft.Json;
using System.Text;

namespace GenerativeAITours.WebAPI
{
    public class OpenAI
    {
        internal static async Task<Tour> GetTourFromOpenAIAsync(string prompt, IPromptCache promptCache)
        {
            string apiKey = OpenAIConfig.ApiKey;
            string apiUrl = OpenAIConfig.ApiUrl;

            var requestPayload = new
            {
                prompt = prompt,
                max_tokens = OpenAIConfig.MaxTokens,
                temperature = OpenAIConfig.Temperature,
                n = OpenAIConfig.N
            };

            var jsonPayload = JsonConvert.SerializeObject(requestPayload);
            var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            HttpClient _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            var openAIResponse = await _httpClient.PostAsync(apiUrl, httpContent);

            Tour tour;

            if (openAIResponse.IsSuccessStatusCode)
            {
                var openAIResponseContent = await openAIResponse.Content.ReadAsStringAsync();
                var resultText = Tour.GetResultFromOpenAI(openAIResponseContent);

                promptCache.SavePromptResponse(Prompt.HashPrompt(prompt), resultText);
                tour = Tour.ParseResult(resultText);
            }
            else
            {
                tour = null;
            }

            return tour;
        }
    }
}
