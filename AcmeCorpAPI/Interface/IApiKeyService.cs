using AcmeCorpAPI.Entities;

namespace AcmeCorpAPI.Interface
{
    public interface IApiKeyService
    {
        bool ValidateApiKey(string key);
        ApiKey GetApiKey(string key);
    }
}
