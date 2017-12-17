using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Core.Lib.Crawler
{
    public class CrawlerContext
    {
        public ResultWrappedHttpClient HttpClient { get; set; }
        public IHtmlLoader HtmlLoader { get; set; }
        public HttpRequestMessage Request { get; set; } = new HttpRequestMessage();
    }

    public class CrawlerContextConfigureOptions : IConfigureNamedOptions<CrawlerContext>
    {
        private readonly IHtmlLoader _loader;
        private readonly ResultWrappedHttpClient _client;

        public CrawlerContextConfigureOptions(IHtmlLoader loader,ResultWrappedHttpClient client)
        {
            _loader = loader;
            _client = client;
        }
        public void Configure(string name, CrawlerContext options)
        {
            options.Request = new HttpRequestMessage() { RequestUri = new Uri(name) };
            options.HttpClient = _client;
            options.HtmlLoader = _loader;
        }

        public void Configure(CrawlerContext options)
            => Configure(string.Empty, options);
    }
}
