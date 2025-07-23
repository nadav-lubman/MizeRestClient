using MizeRestClient.Impl.HttpClientImpl;
using MizeRestClient.Impl.WebRequestIMpl;
using MizeRestClient.Interfaces;

namespace MizeRestClient.Core
{
    public static class RestClientFactory
    {
        public static IRestClient CreateHttpClient() => new HttpClientRestClient();
        public static IRestClient CreateWebRequest() => new WebRequestRestClient();
        public static IRestClient CreateDefault() => CreateHttpClient();
    }
}
