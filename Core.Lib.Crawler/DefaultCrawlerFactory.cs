using System.Net.Http;
using System;
using Microsoft.Extensions.Options;

namespace Core.Lib.Crawler
{
    public class DefaultCrawlerFactory : ICrawlerFactory
    {
        private readonly IOptionsMonitorCache<CrawlerContext> _cache;
        private readonly IOptionsSnapshot<CrawlerContext> _snapshot;

        public DefaultCrawlerFactory(IOptionsMonitorCache<CrawlerContext> cache,IOptionsSnapshot<CrawlerContext> snapshot)
        {
            _cache = cache;
            _snapshot = snapshot;
        }
        public ICrawler Create(Action<HttpRequestMessage> config)
        {
            var req = new HttpRequestMessage();
            config(req);
            var ctx = _cache.GetOrAdd(req.RequestUri.AbsoluteUri, () => _snapshot.Get(req.RequestUri.AbsoluteUri));
            ctx.Request = req;
            return new DefaultCrawler(ctx);
        }
    }
}
