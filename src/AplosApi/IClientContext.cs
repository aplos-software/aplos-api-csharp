using System;

namespace AplosApi
{
    public interface IClientContext
    {
        string AuthToken { get; }
        DateTimeOffset Expires { get; }
        TimeSpan TimeToExpiration { get; }
        bool IsExpired { get; }
    }
}