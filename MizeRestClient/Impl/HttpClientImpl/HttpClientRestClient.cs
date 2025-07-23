using MizeRestClient.Core;
using MizeRestClient.Interfaces;

namespace MizeRestClient.Impl.HttpClientImpl
{
    public class HttpClientRestClient : RestClientBase
    {
        private readonly HttpClient m_client = new();

        public override IRestRequest CreateRequest(string relativeUrl)
        {
            return new HttpClientRestRequest(m_client, m_baseUrl + relativeUrl, m_authHeader, new Dictionary<string, string>(m_headers));
        }
    }
}
