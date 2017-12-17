using System.Net.Http;
using System.Threading.Tasks;

namespace Core.Lib.Crawler
{
    public interface IHttpClient
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage msg);
    }
}
