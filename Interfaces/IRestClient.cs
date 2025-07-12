namespace MizeRestClient.Interfaces
{
    public interface IRestClient
    {
        IRestClient WithBaseUrl(string baseUrl);
        IRestClient WithBasicAuth(string user, string pass);
        IRestClient WithHeader(string key, string value);
        IRestRequest CreateRequest(string relativeUrl);
    }

}
