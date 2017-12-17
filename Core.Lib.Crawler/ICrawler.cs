using System;
using System.Threading.Tasks;

namespace Core.Lib.Crawler
{
    public interface ICrawler
    {

        Task RunAsync(Action<CrawlerResult> result);
    }
}
