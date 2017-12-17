using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using HtmlAgilityPack;
using System;
using System.Linq;
using System.Net.Http;
using System.Collections.Generic;

namespace Core.Lib.Crawler
{
    public class CrawlerBuilder
    {
        public IServiceCollection Services { get; }
        
        public CrawlerBuilder(IServiceCollection services)
        {
            Services = services;
            Services.TryAddTransient<CrawlerContext>();
        }
        public CrawlerBuilder ConfigureRequest(string url,Action<HttpRequestMessage> configReq)
        {
            Services.Configure<HttpRequestMessage>(
                url, 
                req => {
                    req.RequestUri = new Uri(url);
                    configReq(req);
                });
            return this;
        }
        public CrawlerBuilder UseHtmlLoader<TLoader>() where TLoader : class,IHtmlLoader
        {
            Services.TryAddTransient<IHtmlLoader,TLoader>();
            return this;
        }

        public CrawlerBuilder UseHttpClient<TClient>() where TClient : class,IHttpClient
        {
            Services.TryAddTransient(p => new ResultWrappedHttpClient(p.GetService<IHttpClient>()));
            return this;
        }

        public CrawlerBuilder AddFilter<TFilter>(Func<IServiceProvider,TFilter> factory = null) where TFilter : class, IFilter<HtmlNode>
        {
            (factory is null ? (Func<IServiceCollection>)(Services.AddScoped<IFilter<HtmlNode>, TFilter>) : () => Services.AddScoped<IFilter<HtmlNode>>(factory))();
            return this;
        }
    }
}
