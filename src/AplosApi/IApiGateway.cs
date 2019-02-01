using System;
using RestSharp;

namespace AplosApi
{
    public interface IApiGateway
    {
        void Execute(Func<IRestClient, IRestResponse> execute);
        TResponse Retrieve<TResponse>(Func<IRestClient, IRestResponse<TResponse>> retrieve);
    }
}