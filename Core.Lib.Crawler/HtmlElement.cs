using HtmlAgilityPack;
using System;
using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;

namespace Core.Lib.Crawler
{

    public class HtmlElement
    {
        public HtmlElement(HtmlNode node)
        {
            this.Name = node.Name;
            this.Attributes = node.Attributes.ToDictionary(x => x.Name, x => x.Value);
            this.Inner = node.Descendants().Select(x => new HtmlElement(x));
        }
        public string Name { get; }
        public Dictionary<string, string> Attributes { get; }
        public IEnumerable<HtmlElement> Inner { get; }
        
    }
}
