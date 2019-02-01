using AplosApi.Contract;
using RestSharp;

namespace AplosApi.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly IApiGateway _gateway;

        public AccountsService(IApiGateway gateway)
        {
            _gateway = gateway;
        }

        public AccountsResponse GetAccounts(AccountsFilter filter)
        {
            return _gateway.Retrieve(client =>
            {
                var request = new RestRequest("accounts");

                request.ApplyPaging(filter);

                if (filter.EnabledFilter.HasValue)
                {
                    request.AddQueryParameter("f_enabled", filter.EnabledFilter.Value ? "y" : "n");
                }
                if (!string.IsNullOrWhiteSpace(filter.NameFilter))
                {
                    request.AddQueryParameter("f_name", filter.NameFilter);
                }
                if (filter.CategoryFilter.HasValue)
                {
                    request.AddQueryParameter("f_type", filter.CategoryFilter.Value.ToString().ToLower());
                }

                return client.Get<AccountsResponse>(request);
            });
        }

        public AccountResponse GetAccount(int accountNumber)
        {
            return _gateway.Retrieve(client =>
            {
                var request = new RestRequest($"accounts/{accountNumber}");
                return client.Get<AccountResponse>(request);
            });
        }
    }
}