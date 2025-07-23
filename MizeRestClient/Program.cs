using MizeRestClient.Impl.HttpClientImpl;
using MizeRestClient.Interfaces;
namespace MizeRestClient
{
    public class Program
    {
        public static void Main(String[] args)
        {
            IRestClient m_restClient = new HttpClientRestClient()
                .WithBaseUrl("https://mock.api")
                .WithBasicAuth("user", "pass")
                .WithHeader("X-Test", "Value");

            IRestRequest request = m_restClient
                .CreateRequest("/hello")
                .WithBasicAuth("user", "pass");

            request.GetAsync();
        }
    }
}
