using HtmlAgilityPack;
using System;
using System.Collections.Generic;

namespace Core.Lib.Crawler
{
    public class HtmlResult : Result<IEnumerable<HtmlNode>>
    {
        internal static HtmlResult Success(IEnumerable<HtmlNode> msg) => new HtmlResult(msg);
        internal static HtmlResult Fail(Exception ex) => new HtmlResult(ex);

        private HtmlResult(Exception error) : base(error)
        {
        }

        private HtmlResult(IEnumerable<HtmlNode> result) : base(result)
        {
        }
    }
}
