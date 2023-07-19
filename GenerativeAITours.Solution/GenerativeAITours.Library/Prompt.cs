using System.Security.Cryptography;
using System.Text;

namespace GenerativeAITours.Library
{
    public class Prompt
    {
        public static string Build(string country, string city, int daysDuration, string[] interests)
        {
            var joinedInterests = string.Join(',', interests);

            var prompt = new StringBuilder();
            prompt.Append($"Suggest places to vist in {city}. ");
            prompt.Append($"Create a {daysDuration} day itinerary containing 3 activities per day. ");
            prompt.Append($"For people with interests {joinedInterests} with a brief descriptions of each place. ");
            prompt.Append($"Return in json format, a property for location({city}), another property for duration({daysDuration}), another for an array of interests ({joinedInterests}), ");
            prompt.Append("then another for days as an array containing day objects (each with an increasing dayNumber property and an array of activities and each activity with its own name, description and latitude, longitude properties)");

            return prompt.ToString();
        }

        public static string HashPrompt(string prompt)
        {
            using (var md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(prompt);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to a hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                return sb.ToString();
            }
        }
    }
}
