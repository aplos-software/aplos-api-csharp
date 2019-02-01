namespace AplosApi
{
    public interface IClientCredentialSource
    {
        string ClientId { get; }
        string Key { get; }
    }
}