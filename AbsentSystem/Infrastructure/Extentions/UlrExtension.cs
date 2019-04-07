using Microsoft.AspNetCore.Http;

namespace AbsentSystem.Infrastructure.Extentions
{
    public static class UlrExtension
    {
        public static string PathAndQuery(this HttpRequest request) =>
        request.QueryString.HasValue
        ? $"{request.Path}{request.QueryString}"
        : request.Path.ToString();

    }
}
