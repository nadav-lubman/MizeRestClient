using MizeRestClient.Impl.HttpClientImpl;

namespace MizeRestClient.Tests
{
    public class HttpClientRestClientWrapper : HttpClientRestClient
    {
        public HttpClientRestClientWrapper(HttpClient client)
        {
            typeof(HttpClientRestClient)
                .GetField("m_client", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(this, client);
        }
    }
}
