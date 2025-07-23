namespace MizeRestClient.Interfaces
{
    public interface IRestRequest : IConfigurable<IRestRequest>
    {
        Task<string> GetAsync();
        Task<string> PostAsync(string content);
    }
}
