using System;
using System.Linq;
using AplosApi.Contract;
using RestSharp;

namespace AplosApi
{
    public static class RestClientExtensions
    {
        public static string RestClientId(this IRestClient client)
        {
            return client.DefaultParameters
                .Where(x => x.Type == ParameterType.HttpHeader)
                .Where(x => x.Name == Constants.RestClientId)
                .Select(x => x.Value.ToString())
                .FirstOrDefault();
        }

        public static string AuthorizationToken(this IRestClient client)
        {
            return client.DefaultParameters
                .Where(x => x.Type == ParameterType.HttpHeader)
                .Where(x => x.Name == Constants.AuthorizationHeader)
                .Select(x => x.Value.ToString().Replace(Constants.AuthorizationType, "").Trim())
                .FirstOrDefault();
        }

        public static DateTimeOffset AuthorizationExpiration(this IRestClient client)
        {
            return client.DefaultParameters
                .Where(x => x.Type == ParameterType.HttpHeader)
                .Where(x => x.Name == Constants.AplosAuthExpiresHeaderKey)
                .Select(x => DateTimeOffset.Parse(x.Value.ToString()))
                .FirstOrDefault();
        }

        public static void ApplyPaging(this IRestRequest request, PagedFilter filter)
        {
            if (filter.PageNumber.HasValue)
            {
                request.AddQueryParameter("page_num", filter.PageNumber.Value.ToString());
            }
            if (filter.PageSize.HasValue)
            {
                request.AddQueryParameter("page_size", filter.PageSize.Value.ToString());
            }
        }

        public static void AddSnakeCaseJsonParameter(this IRestRequest request, object value)
        {
            var json = SimpleJson.SimpleJson.SerializeObject(value, new SnakeJsonSerializerStrategy());
            request.AddParameter(new Parameter("", json, "application/json", ParameterType.RequestBody));
        }
    }
}