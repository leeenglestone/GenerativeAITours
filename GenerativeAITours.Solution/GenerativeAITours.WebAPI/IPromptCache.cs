using System.Security.Cryptography;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace GenerativeAITours.WebAPI
{
    public interface IPromptCache
    {
        public string GetCachedResponse(string promptHash);
        public string HashPrompt(string prompt);
        public void SavePromptResponse(string promptHash, string response);
        
    }

    public class PromptCacheLocalFileStorage : IPromptCache
    {
        public string GetCachedResponse(string prompt)
        {
            string promptHash = HashPrompt(prompt);

            string filePath = GetFilePath(promptHash);

            if (!File.Exists(filePath))
                return null;

            return File.ReadAllText(filePath);

        }

        public string HashPrompt(string prompt)
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
