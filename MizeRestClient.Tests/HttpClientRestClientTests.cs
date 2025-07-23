using Microsoft.VisualStudio.TestTools.UnitTesting;
using MizeRestClient.Interfaces;

namespace MizeRestClient.Tests
{
    [TestClass]
    public class HttpClientRestClientTests
    {
        private MockHttpMessageHandler m_handler;
        private HttpClient m_httpClient;
        private IRestClient m_restClient;

        [TestInitialize]
        public void Setup()
        {
            m_handler = new MockHttpMessageHandler();
            m_httpClient = new HttpClient(m_handler);
            m_restClient = new HttpClientRestClientWrapper(m_httpClient)
                .WithBaseUrl("https://mock.api")
                .WithHeader("X-Test", "Value");
        }

        [TestMethod]
        public async Task HttpClientRestRequest_GetAsync_ShouldIncludeGlobalHeaders()
        {
            // Arrange
            var request = m_restClient.CreateRequest("/hello");

            // Act
            await request.GetAsync();

            // Assert
            Assert.IsTrue(m_handler.LastRequest.Headers.Contains("X-Test"));
            Assert.AreEqual("Value", m_handler.LastRequest.Headers.GetValues("X-Test").First());
        }

        [TestMethod]
        public async Task RequestAuth_Overrides_ClientAuth()
        {
            // Arrange
            m_restClient.WithBasicAuth("global", "pass");
            IRestRequest request = m_restClient.CreateRequest("/override")
                .WithBasicAuth("local", "123");

            // Act
            await request.GetAsync();

            // Assert
            var authHeader = m_handler.LastRequest.Headers.Authorization;
            Assert.IsNotNull(authHeader);
            Assert.AreEqual("Basic", authHeader.Scheme);

            var expected = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("local:123"));
            Assert.AreEqual(expected, authHeader.Parameter);
        }
    }
}