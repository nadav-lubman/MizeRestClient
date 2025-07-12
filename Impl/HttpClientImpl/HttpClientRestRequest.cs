using System.Net.Http.Headers;
using System.Text;
using MizeRestClient.Interfaces;

namespace MizeRestClient.Impl.HttpClientImpl
{
    public class HttpClientRestRequest : IRestRequest
    {
        private readonly HttpClient m_client;
        private readonly string m_url;
        private string m_authHeader;
        private readonly Dictionary<string, string> m_headers;

        public HttpClientRestRequest(HttpClient client, string url, string authHeader, Dictionary<string, string> headers)
        {
            m_client = client;
            m_url = url;
            m_authHeader = authHeader;
            m_headers = headers;
        }

        public IRestRequest WithBasicAuth(string user, string pass)
        {
            string encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{user}:{pass}"));
            m_authHeader = $"Basic {encoded}";
            return this;
        }

        public IRestRequest WithHeader(string key, string value)
        {
            m_headers[key] = value;
            return this;
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
    }
}
