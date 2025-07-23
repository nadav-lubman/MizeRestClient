using MizeRestClient.Interfaces;
using System.Text;

namespace MizeRestClient.Core
{
    public abstract class ConfigurableBase<TSelf> : IConfigurable<TSelf>
        where TSelf : ConfigurableBase<TSelf>, IConfigurable<TSelf>
    {
        protected string? m_authHeader;
        protected Dictionary<string, string> m_headers = new();

        public TSelf WithBasicAuth(string user, string pass)
        {
            var x = typeof(TSelf);
            var encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{user}:{pass}"));
            m_authHeader = $"Basic {encoded}";
            return (TSelf)this;
        }

        public TSelf WithHeader(string key, string value)
        {
            m_headers[key] = value;
            return (TSelf)this;
        }
    }
}
