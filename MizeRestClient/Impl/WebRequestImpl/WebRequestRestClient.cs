using MizeRestClient.Core;
using MizeRestClient.Impl.WebRequestImpl;
using MizeRestClient.Interfaces;

namespace MizeRestClient.Impl.WebRequestIMpl
{
    public class WebRequestRestClient : RestClientBase
    {
        public override IRestRequest CreateRequest(string relativeUrl)
        {
            return new WebRequestRestRequest(m_baseUrl + relativeUrl, m_authHeader, new Dictionary<string, string>(m_headers));
        }
    }
}
