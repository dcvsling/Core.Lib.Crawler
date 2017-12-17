using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Core.Lib.Crawler
{
    public class DefaultHttpClient : IHttpClient
    {
        private readonly Func<HttpRequestMessage, Task<HttpResponseMessage>> _send;

        public DefaultHttpClient(Func<HttpRequestMessage,Task<HttpResponseMessage>> send)
        {
            _send = send;
        }
        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage msg)
            => _send(msg);
    }
}
