using AcmeCorpAPI.Interface;

namespace AcmeCorpAPI.Entities
{
    public class ApiKeyService : IApiKeyService
    {
        private readonly Dictionary<string, ApiKey> _apiKeys;

        public ApiKeyService()
        {
            _apiKeys = new Dictionary<string, ApiKey>();
            {
                new ApiKey { Key = "your-api-key", Owner = "Acme Corp" }; 
            };
        

        }

        public ApiKey GetApiKey(string key)
        {
            _apiKeys.TryGetValue(key, out var apiKey);
            return apiKey;

            throw new NotImplementedException();
        }

        public bool ValidateApiKey(string key)
        {
            return _apiKeys.ContainsKey(key);
            throw new NotImplementedException();
        }
    }
}
