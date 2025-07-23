namespace MizeRestClient.Interfaces
{
    public interface IConfigurable<TSelf> where TSelf : IConfigurable<TSelf>
    {
        TSelf WithBasicAuth(string user, string pass);
        TSelf WithHeader(string key, string value);
    }
}
