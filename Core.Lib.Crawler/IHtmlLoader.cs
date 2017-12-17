using System.Collections.Generic;
using System.IO;

namespace Core.Lib.Crawler
{
    public interface IHtmlLoader
    {
        HtmlResult Load(Stream steram);
    }
}
