namespace GenerativeAITours.WebAPI
{
    public interface IPromptCache
    {
        public string GetCachedResponse(string promptHash);
        public void SavePromptResponse(string promptHash, string response);
    }
}
