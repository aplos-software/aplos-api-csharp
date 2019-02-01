using RestSharp;

namespace AplosApi
{
    public interface IRestClientFactory
    {
        IRestClient BuildClient();
    }
}