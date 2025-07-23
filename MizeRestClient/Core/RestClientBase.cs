using MizeRestClient.Interfaces;

namespace MizeRestClient.Core
{
    public abstract class RestClientBase : ConfigurableBase<RestClientBase>, IRestClient
    {
        protected string m_baseUrl;

        public RestClientBase WithBaseUrl(string baseUrl)
        {
            m_baseUrl = baseUrl.TrimEnd('/');
            return this;
        }

        public abstract IRestRequest CreateRequest(string relativeUrl);

        IRestClient IConfigurable<IRestClient>.WithBasicAuth(string user, string pass)
        {
            return WithBasicAuth(user, pass);
        }

        IRestClient IConfigurable<IRestClient>.WithHeader(string key, string value)
        {
            return WithHeader(key, value);
        }
    }
}
