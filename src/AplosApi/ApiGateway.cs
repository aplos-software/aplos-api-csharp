using System;
using System.Net;
using RestSharp;

namespace AplosApi
{
    internal class ApiGateway : IApiGateway
    {
        private readonly IRestClientFactory _factory;

        public ApiGateway(IRestClientFactory factory)
        {
            _factory = factory;
        }

        public void Execute(Func<IRestClient, IRestResponse> execute)
        {
            var client = _factory.BuildClient();
            var response = execute(client);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new ApiException($"Execute failed. Status code {response.StatusCode}");
            }

            if (response.ErrorException != null)
            {
                throw new ApiException(response.ErrorMessage, response.ErrorException);
            }
        }

        public TResponse Retrieve<TResponse>(Func<IRestClient, IRestResponse<TResponse>> retrieve)
        {
            var client = _factory.BuildClient();
            var response = retrieve(client);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new ApiException($"Retrieve failed. Status code {response.StatusCode}");
            }

            if (response.ErrorException != null)
            {
                throw new ApiException(response.ErrorMessage, response.ErrorException);
            }

            return response.Data;
        }
    }
}