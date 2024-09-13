using System.Text.Json;
using System.Text;
using OpenAI_API;
using OpenAI_API.Completions;

namespace helpmepickmymain.AI
{
    public class OpenAi
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly OpenAIAPI openAIAPI;

        public OpenAi(OpenAIAPI openAIAPI)
        {
            this.openAIAPI = openAIAPI;
        }

        //public void SetApiKey(string apiKey)
        //{
        //    _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
        //    string debugBaseAddress = $"{_httpClient.BaseAddress}";
        //    string debugBaseAddress2 = $"{_httpClient.BaseAddress}";
        //}

        public async Task<string> GetSpecRecommendationAsync(string preferences, string specOptions)
        {
            var chatRequest = new CompletionRequest
            {
                Prompt = $"You are an expert in World of Warcraft specs. These are my options: {specOptions}. You must pick between one of those specs. " +
                $"Out of those specs, what should I play based on these preferences: {preferences}?",

                Model = "gpt-3.5-turbo-instruct",
                MaxTokens = 150,
            };

            var completion = await openAIAPI.Completions.CreateCompletionAsync(chatRequest);
            return completion.Completions.FirstOrDefault()?.Text.Trim();
        }
    }
}
