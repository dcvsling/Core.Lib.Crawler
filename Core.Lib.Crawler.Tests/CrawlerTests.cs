using System.Linq;
using System.Collections;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System;
using Xunit;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
namespace Core.Lib.Crawler.Tests
{
    public class CrawlerContextTest
    {
        [Theory]
        [ClassData(typeof(TestDataGenerator))]
        async public Task crawler_test(HttpMessageHandler handler)
        {
            var provider = new ServiceCollection().AddCrawler()
                .UseHttpClient(req => new HttpClient(handler).SendAsync(req))
                .AddFilter(hn => hn.Name == "a")
                //.AddFilter(hn => hn.Name == "source")
                .Services
                .BuildServiceProvider();
           
            await provider.GetService<ICrawlerFactory>()
                .Create(req =>
                {
                    req.Method = HttpMethod.Get;
                    req.RequestUri = new Uri("https://www.google.com");
                })
                .RunAsync(
                    result => result.OnError<Exception>(ex =>
                        {
                            Assert.IsAssignableFrom<Exception>(ex);
                        })
                        .OnSuccess(r =>
                        {
                            var actual = r.Single();

                            Assert.Collection(r, e => Assert.Equal("a", actual.Name));
                            Assert.Collection(r, e => Assert.Equal("href", actual.Attributes.Single().Key));
                            Assert.Collection(r, e => Assert.Equal("https://www.google.com", actual.Attributes.Single().Value));
                            return r;
                        })
                );
        }

        public static IEnumerable<object> GetArgument()
        {
            yield return new object[] { new OkHandler() };
        }
    }

    public class TestDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[] {new OkHandler() as HttpMessageHandler},
            new object[] {new ErrorHandler() as HttpMessageHandler}
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class OkHandler : HttpMessageHandler
    {
        public const string CONTENT = @"<a href='https://www.google.com' />";
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            => Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK) {Content =  new StringContent(CONTENT)});
    }

    public class ErrorHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            => Task.FromResult(new HttpResponseMessage(HttpStatusCode.InternalServerError));
    }
}
