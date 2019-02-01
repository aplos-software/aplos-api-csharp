using System;

namespace AplosApi
{
    internal class ClientContext : IClientContext
    {
        public string AuthToken { get; set; }
        public DateTimeOffset Expires { get; set; }
        public TimeSpan TimeToExpiration => Expires - DateTimeOffset.Now;
        public bool IsExpired => TimeToExpiration < TimeSpan.Zero;
    }
}