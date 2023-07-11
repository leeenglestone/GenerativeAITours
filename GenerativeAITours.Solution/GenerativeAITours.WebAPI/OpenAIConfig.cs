namespace GenerativeAITours.WebAPI
{
    public class OpenAIConfig
    {
        public static string ApiKey = "";
        public static string Model = "text-davinci-003";
        public static string ApiUrl = $"https://api.openai.com/v1/engines/{Model}/completions";        

        public static int MaxTokens = 2000;
        public static double Temperature = 0.7;
        public static int N = 1;

        //string model = "gpt-3.5-turbo";         
        //string apiUrl = "https://api.openai.com/v1/chat/completions";
    }
}
