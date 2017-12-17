using HtmlAgilityPack;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System;

namespace Core.Lib.Crawler
{
    public class HtmlAgilityHelper : IHtmlLoader
    {
        private readonly IEnumerable<IFilter<HtmlNode>> _filters;

        public HtmlAgilityHelper(IEnumerable<IFilter<HtmlNode>> filters)
        {
            _filters = filters;
        }
        public HtmlResult Load(Stream steram)
        {
            try
            {
                var docs = new HtmlDocument();
                docs.Load(steram);
                var nodes = docs.DocumentNode.Descendants();
                return HtmlResult.Success(
                    _filters.SelectMany(x => nodes.Where(x.IsEnable))
                        .Distinct());
            }
            catch(Exception ex)
            {
                return HtmlResult.Fail(ex);
            }
        }
    }
}
