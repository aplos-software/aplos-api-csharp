using AplosApi.Contract;
using RestSharp;

namespace AplosApi.Services
{
    public class PurposesService : IPurposesService
    {
        private readonly IApiGateway _gateway;

        public PurposesService(IApiGateway gateway)
        {
            _gateway = gateway;
        }

        public PurposesResponse GetPurposes(PurposesFilter filter)
        {
            return _gateway.Retrieve(client =>
            {
                var request = new RestRequest("purposes");

                request.ApplyPaging(filter);

                if (filter.EnabledFilter.HasValue)
                {
                    request.AddQueryParameter("f_enabled", filter.EnabledFilter.Value ? "y" : "n");
                }
                if (!string.IsNullOrWhiteSpace(filter.NameFilter))
                {
                    request.AddQueryParameter("f_name", filter.NameFilter);
                }

                return client.Get<PurposesResponse>(request);
            });
        }

        public PurposeResponse GetPurpose(int purposeId)
        {
            return _gateway.Retrieve(client =>
            {
                var request = new RestRequest($"purposes/{purposeId}");
                return client.Get<PurposeResponse>(request);
            });
        }

        public PurposeResponse PostPurpose(PurposeInfo purpose)
        {
            return _gateway.Retrieve(client =>
            {
                var request = new RestRequest($"purposes");
                request.AddSnakeCaseJsonParameter(purpose);
                return client.Post<PurposeResponse>(request);
            });
        }

        public PurposeResponse PutPurpose(int purposeId, PurposeInfo purpose)
        {
            return _gateway.Retrieve(client =>
            {
                var request = new RestRequest($"purposes/{purposeId}");
                request.AddSnakeCaseJsonParameter(purpose);
                return client.Put<PurposeResponse>(request);
            });
        }

        public ApiResponse DeletePurpose(int purposeId)
        {
            return _gateway.Retrieve(client =>
            {
                var request = new RestRequest($"purposes/{purposeId}");
                return client.Delete<ApiResponse>(request);
            });
        }
    }
}