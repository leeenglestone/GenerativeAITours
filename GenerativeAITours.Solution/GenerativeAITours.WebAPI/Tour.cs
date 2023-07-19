using Newtonsoft.Json;

namespace GenerativeAITours.WebAPI
{
    public class Tour
    {
        public Tour() { }

        public string Location { get; set; }
        public int Duration { get; set; }
        public string[] Interests { get; set; }
        public List<ItineraryDay> Days { get; set; }

        public static Tour ParseResult(string result)
        {
            Tour tour = JsonConvert.DeserializeObject<Tour>(result);

            return tour;
        }

        public static string GetResultFromOpenAI(string json)
        {
            TextCompletion textCompletion = JsonConvert.DeserializeObject<TextCompletion>(json);

            if (textCompletion.Choices != null && textCompletion.Choices.Length > 0)
            {
                Choice firstChoice = textCompletion.Choices[0];
                string firstChoiceText = firstChoice.Text;

                if (firstChoiceText.StartsWith("."))
                {
                    firstChoiceText = firstChoiceText.Substring(1, firstChoiceText.Length - 1);
                }

                return firstChoiceText;
            }

            return null;
        }
    }

    public class Activity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class ItineraryDay
    {

        public int DayNumber { get; set; }
        public List<Activity> Activities { get; set; }
    }

    public class Itinerary
    {
        public string Location { get; set; }
        public int Duration { get; set; }
        public List<string> Interests { get; set; }
        public List<ItineraryDay> Days { get; set; }
    }

    public class Choice
    {
        public string Text { get; set; }
        public int Index { get; set; }
        public object Logprobs { get; set; }
        public string FinishReason { get; set; }
    }

    public class TextCompletion
    {
        public string Id { get; set; }
        public string Object { get; set; }
        public long Created { get; set; }
        public string Model { get; set; }
        public Choice[] Choices { get; set; }
        public Usage Usage { get; set; }
    }

    public class Usage
    {
        public int PromptTokens { get; set; }
        public int CompletionTokens { get; set; }
        public int TotalTokens { get; set; }
    }

}
