using System.Net.Http.Headers;
using System.Text;
using MizeRestClient.Core;
using MizeRestClient.Interfaces;

namespace MizeRestClient.Impl.HttpClientImpl
{
    public class HttpClientRestRequest : ConfigurableBase<HttpClientRestRequest>, IRestRequest
    {
        private readonly HttpClient m_client;
        private readonly string m_url;

        public HttpClientRestRequest(HttpClient client, string url, string authHeader, Dictionary<string, string> headers)
        {
            m_client = client;
            m_url = url;
            m_authHeader = authHeader;
            m_headers = headers;
        }

        public async Task<string> GetAsync()
        {
            using var req = CreateRequest(HttpMethod.Get);

            HttpResponseMessage res = await m_client.SendAsync(req);
            res.EnsureSuccessStatusCode();
            return await res.Content.ReadAsStringAsync();
        }

        public async Task<string> PostAsync(string content)
        {
            using var req = CreateRequest(HttpMethod.Post, content);

            var res = await m_client.SendAsync(req);
            res.EnsureSuccessStatusCode();
            return await res.Content.ReadAsStringAsync();
        }

        private HttpRequestMessage CreateRequest(HttpMethod method, string? content = null)
        {
            var request = new HttpRequestMessage(method, m_url);

            if (!string.IsNullOrEmpty(content))
            {
                request.Content = new StringContent(content, Encoding.UTF8, "application/json");
            }

            ApplyHeaders(request);

            return request;
        }

        private void ApplyHeaders(HttpRequestMessage request)
        {
            if (!string.IsNullOrEmpty(m_authHeader))
            {
                request.Headers.Authorization = AuthenticationHeaderValue.Parse(m_authHeader);
            }

            foreach (var kvp in m_headers)
            {
                request.Headers.Add(kvp.Key, kvp.Value);
            }  
        }

        IRestRequest IConfigurable<IRestRequest>.WithBasicAuth(string user, string pass)
        {
            return WithBasicAuth(user, pass);
        }

        IRestRequest IConfigurable<IRestRequest>.WithHeader(string key, string value)
        {
            return WithHeader(key, value);
        }
    }
}
