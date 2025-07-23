namespace MizeRestClient.Interfaces
{
    public interface IRestClient : IConfigurable<IRestClient>
    {
        IRestRequest CreateRequest(string relativeUrl);
    }

}
