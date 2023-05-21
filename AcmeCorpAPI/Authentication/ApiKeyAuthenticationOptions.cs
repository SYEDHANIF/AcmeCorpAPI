using Microsoft.AspNetCore.Authentication;

namespace AcmeCorpAPI.Authentication
{
    public class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
    {
        public const string DefaultScheme = "UNC1978";
        public string Scheme => DefaultScheme;
        public string AuthenticationType = DefaultScheme;
    }
}
