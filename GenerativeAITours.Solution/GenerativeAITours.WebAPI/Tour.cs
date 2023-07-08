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

            //var tour = new Tour();
            //tour.Days = ParseDays(result);
           
            return tour;

        }

        //public static List<Day> ParseDays(string text)
        //{

        //    List<ItineraryDay> days = new List<ItineraryDay>();

        //    // Split the text into individual lines
        //    string[] lines = text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

        //    ItineraryDay currentDay = null;

        //    foreach (var line in lines)
        //    {
        //        if (line.StartsWith("Day "))
        //        {
        //            // If the line starts with "Day ", create a new Day object
        //            currentDay = new ItineraryDay();
        //            currentDay.DayNumber = int.Parse(line.Substring(4)); // Extract the day number from the line
        //            currentDay.Activities = new List<string>();
        //            days.Add(currentDay);
        //        }
        //        else if (currentDay != null && !string.IsNullOrWhiteSpace(line))
        //        {
        //            // If the currentDay is not null and the line is not empty, add the activity to the currentDay
        //            currentDay.Activities.Add(line.Trim());
        //        }
        //    }

        //    return days;
        //}

        public static string GetResultFromOpenAI(string json)
        {
            TextCompletion textCompletion = JsonConvert.DeserializeObject<TextCompletion>(json);

            if (textCompletion.Choices != null && textCompletion.Choices.Length > 0)
            {
                Choice firstChoice = textCompletion.Choices[0];
                string firstChoiceText = firstChoice.Text;

                if(firstChoiceText.StartsWith("."))
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



    //public class ItineraryDay
    //{
    //    public int DayNumber { get; set; }
    //    public List<string> Activities { get; set; }
    //}

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
