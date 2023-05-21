using AcmeCorpAPI.Entities;
using AcmeCorpAPI.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace AcmeCorpAPI.Authentication
{
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
    {
        private readonly IApiKeyService _apiKeyService;

        public ApiKeyAuthenticationHandler(
            IOptionsMonitor<ApiKeyAuthenticationOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, ISystemClock clock,
            IApiKeyService apiKeyService) : base(options, logger, encoder, clock)
        {
            _apiKeyService = apiKeyService;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            if (!Request.Headers.TryGetValue("Authorization", out var apiKeyHeaderValues))
                return AuthenticateResult.Fail("Missing Authorization header");

            if (!apiKeyHeaderValues.Any())
                return AuthenticateResult.Fail("Invalid Authorization header");

            var apiKey = apiKeyHeaderValues.First().Split(' ').Last();

            if (!_apiKeyService.ValidateApiKey(apiKey))
                return AuthenticateResult.Fail("Invalid API key");

            var apiKeyData = _apiKeyService.GetApiKey(apiKey);

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, apiKeyData.Owner),
            new Claim(ClaimTypes.NameIdentifier, apiKeyData.Key)
            // Add any other claims as needed
        };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);

            throw new NotImplementedException();
        }
    }
}
