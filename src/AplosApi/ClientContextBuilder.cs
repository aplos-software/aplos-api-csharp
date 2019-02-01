using System;
using System.Net;
using RestSharp;

namespace AplosApi
{
    internal class ClientContextBuilder : IClientContextBuilder
    {
        private readonly string _clientId;
        private readonly EncoderDecoder _encDec;

        public ClientContextBuilder(IClientCredentialSource credentialSource)
        {
            _clientId = credentialSource.ClientId;
            _encDec = new EncoderDecoder(credentialSource.Key);
        }

        public IClientContext CreateContext()
        {
            var client = new RestClient(Constants.ApiUrlBase);
            client.AddDefaultHeader(Constants.RestClientId, Guid.NewGuid().ToString());
            client.AddDefaultHeader(Constants.AplosApiClientTypeHeaderKey, Constants.AplosApiClientTypeHeaderValue);

            var authRestRequest = new RestRequest($"auth/{_clientId}");
            var authRestResponse = client.Get<AuthResponse>(authRestRequest);

            if (authRestResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new AuthorizationException($"Authorization failed. Status code {authRestResponse.StatusCode}");
            }

            var authResponse = authRestResponse.Data;

            var token = _encDec.DecryptEncryptedData(authResponse.Data.Token);

            return new ClientContext
            {
                AuthToken = token,
                Expires = authResponse.Data.Expires
            };
        }

        private class AuthResponse
        {
            public string Version { get; set; }
            public int Status { get; set; }
            public TokenResponse Data { get; set; }
        }

        private class TokenResponse
        {
            public DateTimeOffset Expires { get; set; }
            public string Token { get; set; }
        }
    }
}