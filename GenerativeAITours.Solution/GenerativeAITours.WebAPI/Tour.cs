using Newtonsoft.Json;

namespace GenerativeAITours.WebAPI
{
    public class Tour
    {
        public Tour() { }

        public string Location { get; set; }
        public int Duration { get; set; }
        public string Interest { get; set; }
        public List<ItineraryDay> Days { get; set; }

        public static Tour ParseResult(string result)
        {
            Tour tour = JsonConvert.DeserializeObject<Tour>(result);

            //var tour = new Tour();
            //tour.Days = ParseDays(result);
           
            return tour;

        }

        //public static List<ItineraryDay> ParseDays(string text)
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

    public class ItineraryDay
    {
        public int DayNumber { get; set; }
        public List<string> Activities { get; set; }
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

/*
 
 {
  "location": "Paris",
  "days": 5,
  "interests": "Culture",
  "itinerary": [
    {
      "day": 1,
      "activities": [
        "Morning: Start your day with a visit to the [[Louvre Museum]]. Explore its vast collection of art and historical artifacts, including the iconic [[Mona Lisa]].",
        "Afternoon: Head to the charming neighborhood of [[Montmartre]]. Visit the stunning [[Sacré-Cœur Basilica]] and wander through the narrow streets filled with artistic history.",
        "Evening: Enjoy a dinner cruise along the [[Seine River]], taking in the illuminated landmarks of Paris."
      ]
    },
    {
      "day": 2,
      "activities": [
        "Morning: Immerse yourself in art at the [[Musée d'Orsay]], home to an impressive collection of Impressionist and Post-Impressionist masterpieces.",
        "Afternoon: Explore the historic [[Île de la Cité]]. Marvel at the exquisite stained glass windows of [[Sainte-Chapelle]] and visit the iconic [[Notre-Dame Cathedral]].",
        "Evening: Attend a classical concert at the renowned [[Palais Garnier]], the opulent opera house of Paris."
      ]
    },
    {
      "day": 3,
      "activities": [
        "Morning: Take a guided tour of the magnificent [[Palace of Versailles]], located just outside of Paris. Explore its lavish halls, stunning gardens, and learn about the French monarchy.",
        "Afternoon: Visit the [[Musée de l'Orangerie]], known for its collection of Monet's Water Lilies paintings and other famous works of art.",
        "Evening: Experience a traditional cabaret show at the legendary [[Moulin Rouge]], renowned for its captivating performances."
      ]
    },
    {
      "day": 4,
      "activities": [
        "Morning: Explore the artistic district of [[Saint-Germain-des-Prés]]. Visit the [[Musée de Cluny]], housing medieval art and the enchanting Lady and the Unicorn tapestries.",
        "Afternoon: Discover the innovative and contemporary artworks at the [[Centre Pompidou]], a vibrant cultural hub in Paris.",
        "Evening: Take a romantic evening stroll along the banks of the [[Seine River]], enjoying the illuminated bridges and landmarks."
      ]
    },
    {
      "day": 5,
      "activities": [
        "Morning: Delve into French art at the [[Musée Rodin]]. Admire the famous sculpture, [[The Thinker]], and explore the beautiful gardens.",
        "Afternoon: Visit the historic [[Père Lachaise Cemetery]], where famous personalities like Oscar Wilde, Jim Morrison, and Frédéric Chopin are buried.",
        "Evening: Enjoy a performance at the [[Opéra Bastille]], a modern opera house known for its exceptional productions."
      ]
    }
  ]
}

 */