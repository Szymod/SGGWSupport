using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace SGGWSupportWeb.Extensions
{
    public static class HttpClientExtensions
    {
        public async static Task<HttpResponseMessage> PatchAsync(this HttpClient client, string requestUri, HttpContent content = null, CancellationToken? cancellationToken = null)
        {
            var method = new HttpMethod("PATCH");

            var request = new HttpRequestMessage(method, requestUri);
            if (content != null)
            {
                request.Content = content;
            }

            return await client.SendAsync(request, cancellationToken ?? CancellationToken.None);
        }

        public async static Task<HttpResponseMessage> PatchAsync(this HttpClient client, Uri requestUri, HttpContent content = null, CancellationToken? cancellationToken = null)
        {
            var method = new HttpMethod("PATCH");

            var request = new HttpRequestMessage(method, requestUri);
            if (content != null)
            {
                request.Content = content;
            }

            return await client.SendAsync(request, cancellationToken ?? CancellationToken.None);
        }
    }
}