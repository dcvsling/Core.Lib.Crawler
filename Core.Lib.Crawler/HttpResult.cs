using System.Collections;
using System.Net.Http;
using System.IO;
using System;
using Core.Lib.Crawler;

namespace Core.Lib.Crawler
{
    public class HttpResult : Result<HttpResponseMessage>
    {
        internal static HttpResult Success(HttpResponseMessage msg) => new HttpResult(msg);
        internal static HttpResult Fail(Exception ex) => new HttpResult(ex);

        internal static HttpResult Ref(Result<HttpResponseMessage> result) => new HttpResult(result);
        private HttpResult(Exception error) : base(error)
        {
        }

        private HttpResult(HttpResponseMessage result) : base(result)
        {
        }

        private HttpResult(Result<HttpResponseMessage> result): base(result)
        {

        }
    }
}
