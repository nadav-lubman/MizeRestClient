using System.Text;
using MizeRestClient.Interfaces;

namespace MizeRestClient.Core
{
    public abstract class RestClientBase : IRestClient
    {
        protected string m_baseUrl;
        protected string m_authHeader;
        protected Dictionary<string, string> m_headers = new();

        public IRestClient WithBaseUrl(string baseUrl)
        {
            m_baseUrl = baseUrl.TrimEnd('/');
            return this;
        }

        public IRestClient WithBasicAuth(string user, string pass)
        {
            var encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{user}:{pass}"));
            m_authHeader = $"Basic {encoded}";
            return this;
        }

        public IRestClient WithHeader(string key, string value)
        {
            m_headers[key] = value;
            return this;
        }

        public abstract IRestRequest CreateRequest(string relativeUrl);
    }
}
