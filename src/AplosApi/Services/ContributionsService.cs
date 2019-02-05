using AplosApi.Contract;
using RestSharp;

namespace AplosApi.Services
{
    public class ContributionsService : IContributionsService
    {
        private readonly IApiGateway _gateway;

        public ContributionsService(IApiGateway gateway)
        {
            _gateway = gateway;
        }

        public ContributionsResponse GetContributions(ContributionsFilter filter)
        {
            return _gateway.Retrieve(client =>
            {
                var request = new RestRequest("contributions");

                request.ApplyPaging(filter);

                if (filter.ContactIdFilter.HasValue)
                {
                    request.AddQueryParameter("f_contact", filter.ContactIdFilter.Value.ToString());
                }
                if (!string.IsNullOrWhiteSpace(filter.ContactNameFilter))
                {
                    request.AddQueryParameter("f_contactname", filter.ContactNameFilter);
                }
                if (filter.LastUpdatedFilter.HasValue)
                {
                    var value = filter.LastUpdatedFilter.Value
                        .ToUniversalTime()
                        .ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                    request.AddQueryParameter("f_lastupdated", value, false);
                }
                if (!string.IsNullOrWhiteSpace(filter.RangeStartFilter))
                {
                    request.AddQueryParameter("f_rangestart", filter.RangeStartFilter);
                }
                if (!string.IsNullOrWhiteSpace(filter.RangeEndFilter))
                {
                    request.AddQueryParameter("f_rangeend", filter.RangeEndFilter);
                }

                return client.Get<ContributionsResponse>(request);
            });
        }

        public ContributionResponse GetContribution(int contributionId)
        {
            return _gateway.Retrieve(client =>
            {
                var request = new RestRequest($"contributions/{contributionId}");
                return client.Get<ContributionResponse>(request);
            });
        }

        public ApiResponse DeleteContribution(int contributionId)
        {
            return _gateway.Retrieve(client =>
            {
                var request = new RestRequest($"contributions/{contributionId}");
                return client.Delete<ApiResponse>(request);
            });
        }
    }
}