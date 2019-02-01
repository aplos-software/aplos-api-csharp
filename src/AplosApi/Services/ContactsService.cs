using AplosApi.Contract;
using RestSharp;

namespace AplosApi.Services
{
    public class ContactsService : IContactsService
    {
        private readonly IApiGateway _gateway;

        public ContactsService(IApiGateway gateway)
        {
            _gateway = gateway;
        }

        public ContactsResponse GetContacts(ContactsFilter filter)
        {
            return _gateway.Retrieve(client =>
            {
                var request = new RestRequest("contacts");

                request.ApplyPaging(filter);

                if (filter.LastUpdatedFilter.HasValue)
                {
                    var value = filter.LastUpdatedFilter.Value
                        .ToUniversalTime()
                        .ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                    request.AddQueryParameter("f_lastupdated", value, false);
                }
                if (!string.IsNullOrWhiteSpace(filter.EmailFilter))
                {
                    request.AddQueryParameter("f_email", filter.EmailFilter);
                }
                if (!string.IsNullOrWhiteSpace(filter.NameFilter))
                {
                    request.AddQueryParameter("f_name", filter.NameFilter);
                }
                if (filter.TypeFilter.HasValue)
                {
                    request.AddQueryParameter("f_type", filter.TypeFilter.Value.ToString().ToLowerInvariant());
                }

                return client.Get<ContactsResponse>(request);
            });
        }

        public ContactResponse GetContact(int contactId)
        {
            return _gateway.Retrieve(client =>
            {
                var request = new RestRequest($"contacts/{contactId}");
                return client.Get<ContactResponse>(request);
            });
        }
    }
}
