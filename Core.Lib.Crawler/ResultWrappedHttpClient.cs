using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Core.Lib.Crawler
{
    public class ResultWrappedHttpClient
    {
        private readonly IHttpClient _client;

        public ResultWrappedHttpClient(IHttpClient client)
        {
            _client = client;
        }
        public HttpResult Send(HttpRequestMessage msg)
        {
            try
            {
                return HttpResult.Ref(TaskResult<HttpResponseMessage>
                    .Wait(_client
                        .SendAsync(msg)
                        .ContinueWith(t => t.Result.EnsureSuccessStatusCode()))
                        .ForResult());
            }
            catch(Exception ex)
            {
                return HttpResult.Fail(ex);
            }
        }
    }

}
