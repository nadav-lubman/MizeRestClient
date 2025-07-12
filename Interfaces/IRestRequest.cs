namespace MizeRestClient.Interfaces
{
    public interface IRestRequest
    {
        IRestRequest WithBasicAuth(string user, string pass);
        IRestRequest WithHeader(string key, string value);
        Task<string> GetAsync();
        Task<string> PostAsync(string content);
    }
}
