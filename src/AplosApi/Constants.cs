using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("AplosApi.Tests")]

namespace AplosApi
{
    internal static class Constants
    {
        public const string ApiUrlBase = "https://app.aplos.com/hermes/api/v1/";
        public const string RestClientId = "X-Rest-Client-Id";
        public const string AplosApiClientTypeHeaderKey = "X-Aplos-Api-Client-Type";
        public const string AplosApiClientTypeHeaderValue = "aplos-api-client-csharp";
        public const string AplosAuthExpiresHeaderKey = "X-Aplos-Auth-Expires";
        public const string AuthorizationHeader = "Authorization";
        public const string AuthorizationType = "Bearer:";
    }
}
