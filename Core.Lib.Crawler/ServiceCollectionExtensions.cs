using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using System.Runtime.InteropServices.ComTypes;
using Core.Lib.Crawler;
using HtmlAgilityPack;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {   
        public static CrawlerBuilder AddCrawler(this IServiceCollection services)
            => new CrawlerBuilder(services)
                .UseDefaultService();

        public static CrawlerBuilder UseDefaultService(this CrawlerBuilder builder)
        {
            builder.UseHtmlLoader<HtmlAgilityHelper>()
                  .UseHttpClient<HttpClientFactoryClient>()
                  .Services
                  .AddHttpClient()
                  .AddSingleton<ICrawlerFactory, DefaultCrawlerFactory>()
                  .AddTransient<IConfigureOptions<CrawlerContext>,CrawlerContextConfigureOptions>();
            return builder;
        }

        public static Result<TResult> WaitResult<TResult>(this Task<TResult> task)
            => TaskResult<TResult>.Wait(task).ForResult();

        public static CrawlerBuilder AddFilter(this CrawlerBuilder builder, Func<HtmlNode, bool> predicate)
        {
            builder.AddFilter(p => new DefaultFilter<HtmlNode>(predicate));
            return builder;
        }

        public static CrawlerBuilder UseHttpClient(this CrawlerBuilder builder, Func<HttpRequestMessage, Task<HttpResponseMessage>> send)
        {
            builder.Services.Replace(ServiceDescriptor.Transient<IHttpClient>(p => new DefaultHttpClient(send)));
            return builder;
        }
    }
}
