using System.Net;
using System;
using System.Collections.Generic;
using Core.Lib.Crawler;

namespace Core.Lib.Crawler
{
    public class CrawlerResult : Result<IEnumerable<HtmlElement>>
    {
        internal static CrawlerResult Success(IEnumerable<HtmlElement> result) => new CrawlerResult(result);
        internal static CrawlerResult Fail(Exception ex) => new CrawlerResult(ex);
        internal static CrawlerResult Ref(Result<IEnumerable<HtmlElement>> result) => new CrawlerResult(result);
        private CrawlerResult(Exception error) : base(error)
        {
        }

        private CrawlerResult(IEnumerable<HtmlElement> result) : base(result)
        {
        }
        private CrawlerResult(Result<IEnumerable<HtmlElement>> result) : base(result)
        {
        }
    }
}
