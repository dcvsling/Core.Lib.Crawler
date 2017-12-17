using System.Net.Http;
using System.Threading.Tasks;

namespace Core.Lib.Crawler
{
    public class HttpClientFactoryClient : IHttpClient
    {
        private readonly IHttpClientFactory _factory;

        public HttpClientFactoryClient(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage msg)
            => _factory.CreateClient(msg.RequestUri.AbsoluteUri).SendAsync(msg);
    }
}
