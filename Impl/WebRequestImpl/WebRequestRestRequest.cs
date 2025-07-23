using System.Net;
using System.Text;
using MizeRestClient.Core;
using MizeRestClient.Interfaces;

namespace MizeRestClient.Impl.WebRequestImpl
{
    public class WebRequestRestRequest : ConfigurableBase<WebRequestRestRequest>, IRestRequest
    {
        private readonly string m_url;
        private string m_authHeader;
        private readonly Dictionary<string, string> m_headers;

        public WebRequestRestRequest(string url, string authHeader, Dictionary<string, string> headers)
        {
            m_url = url;
            m_authHeader = authHeader;
            m_headers = headers;
        }

        public async Task<string> GetAsync()
        {
            var req = CreateRequest("GET");

            using var res = await req.GetResponseAsync();
            using var stream = res.GetResponseStream();
            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync();
        }

        public async Task<string> PostAsync(string content)
        {
            var req = CreateRequest("POST", "application/json");

            using var stream = await req.GetRequestStreamAsync();
            var buffer = Encoding.UTF8.GetBytes(content);
            await stream.WriteAsync(buffer, 0, buffer.Length);

            using var res = await req.GetResponseAsync();
            using var reader = new StreamReader(res.GetResponseStream());
            return await reader.ReadToEndAsync();
        }

        private HttpWebRequest CreateRequest(string method, string? contentType = null)
        {
            var request = WebRequest.CreateHttp(m_url);
            request.Method = method;

            if (!string.IsNullOrEmpty(contentType))
            {
                request.ContentType = contentType;
            }

            ApplyHeaders(request);

            return request;
        }

        private void ApplyHeaders(HttpWebRequest request)
        {
            if (!string.IsNullOrEmpty(m_authHeader))
            {
                request.Headers[HttpRequestHeader.Authorization] = m_authHeader;
            }

            foreach (var kvp in m_headers)
            {
                request.Headers[kvp.Key] = kvp.Value;
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
