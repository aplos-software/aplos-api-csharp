using System;
using RestSharp;

namespace AplosApi
{
    internal class RestClientFactory : IRestClientFactory
    {
        private readonly object _syncLock = new object();
        private readonly IClientContextBuilder _contextBuilder;
        private readonly TimeSpan? _minimumTimeToExpiration;
        private IRestClient _client;
        private IClientContext _context;

        public RestClientFactory(IClientContextBuilder contextBuilder, TimeSpan? minimumTimeToExpiration = null)
        {
            _contextBuilder = contextBuilder;
            _minimumTimeToExpiration = minimumTimeToExpiration ?? TimeSpan.FromSeconds(10);
        }

        public IRestClient BuildClient()
        {
            lock (_syncLock)
            {
                if (ShouldRefreshContext())
                {
                    _context = _contextBuilder.CreateContext();
                    _client = new RestClient(Constants.ApiUrlBase);
                    _client.AddDefaultHeader(Constants.RestClientId, Guid.NewGuid().ToString());
                    _client.AddDefaultHeader(Constants.AplosApiClientTypeHeaderKey, Constants.AplosApiClientTypeHeaderValue);
                    _client.AddDefaultHeader(Constants.AplosAuthExpiresHeaderKey, _context.Expires.ToString("O"));
                    _client.AddDefaultHeader(Constants.AuthorizationHeader, $"{Constants.AuthorizationType} {_context.AuthToken}");
                }

                return _client;
            }
        }

        private bool ShouldRefreshContext()
        {
            if (_context == null) return true;
            if (_context.IsExpired) return true;
            if (_context.TimeToExpiration < _minimumTimeToExpiration) return true;
            return false;
        }
    }
}