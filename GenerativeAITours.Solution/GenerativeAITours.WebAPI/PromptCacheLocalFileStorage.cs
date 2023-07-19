using GenerativeAITours.Library;

namespace GenerativeAITours.WebAPI
{
    public class PromptCacheLocalFileStorage : IPromptCache
    {
        public string GetCachedResponse(string prompt)
        {
            string promptHash = Prompt.HashPrompt(prompt);
            string filePath = GetFilePath(promptHash);

            if (!File.Exists(filePath))
                return null;

            return File.ReadAllText(filePath);
        }

        public void SavePromptResponse(string promptHash, string response)
        {
            string filePath = GetFilePath(promptHash);
            File.WriteAllText(filePath, response);
        }

        private static string GetFilePath(string promptHash)
        {
            return $@"c:\temp\genaitours\{promptHash}.txt";
        }
    }
}
