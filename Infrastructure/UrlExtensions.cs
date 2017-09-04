using Microsoft.AspNetCore.Http;

namespace SportsStore.Infrastructure
{
    public static class UrlExtensions
    {
        public static string PathAndQuery(this HttpRequest req) =>
            req.QueryString.HasValue
            ? $"{req.Path}{req.QueryString}"
            : req.Path.ToString();
    }
}