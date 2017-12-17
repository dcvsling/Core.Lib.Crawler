using System.Net.Http;
using System;

namespace Core.Lib.Crawler
{
    public interface ICrawlerFactory
    {
        ICrawler Create(Action<HttpRequestMessage> req);
    }
}
