using Microsoft.Extensions.DependencyInjection;
using HtmlAgilityPack;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

namespace Core.Lib.Crawler
{
    public class DefaultCrawler : ICrawler
    {
        private readonly CrawlerContext _context;

        public DefaultCrawler(CrawlerContext context)
        {
            _context = context;
        }

        public Task RunAsync(Action<CrawlerResult> result)
            => Task.Run(() => result(CrawlerResult.Ref(
                _context.HttpClient.Send(_context.Request)
                    .OnError(Fail(result))
                    .OnSuccess(r => TaskResult<Stream>.Wait(r.Content.ReadAsStreamAsync()).ForResult())
                    .OnSuccess(r => _context.HtmlLoader.Load(r.Value))
                    .OnSuccess(r => ToElement(r.Value)))));

        private Action<Exception> Fail(Action<CrawlerResult> callback)
            => ex => callback(CrawlerResult.Fail(ex));
        private IEnumerable<HtmlElement> ToElement(IEnumerable<HtmlNode> nodes)
            => nodes.Select(ToElement);
        private HtmlElement ToElement(HtmlNode node) => new HtmlElement(node);
    }
}
