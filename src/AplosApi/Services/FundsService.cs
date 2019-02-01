using AplosApi.Contract;
using RestSharp;

namespace AplosApi.Services
{
    public class FundsService : IFundsService
    {
        private readonly IApiGateway _gateway;

        public FundsService(IApiGateway gateway)
        {
            _gateway = gateway;
        }

        public FundsResponse GetFunds(FundsFilter filter)
        {
            return _gateway.Retrieve(client =>
            {
                var request = new RestRequest("funds");

                request.ApplyPaging(filter);

                if (filter.AccountNumberFilter.HasValue)
                {
                    request.AddQueryParameter("f_accountnumber", filter.AccountNumberFilter.Value.ToString());
                }
                if (!string.IsNullOrWhiteSpace(filter.NameFilter))
                {
                    request.AddQueryParameter("f_name", filter.NameFilter);
                }

                return client.Get<FundsResponse>(request);
            });
        }

        public FundResponse GetFund(int fundId)
        {
            return _gateway.Retrieve(client =>
            {
                var request = new RestRequest($"funds/{fundId}");
                return client.Get<FundResponse>(request);
            });
        }
    }
}