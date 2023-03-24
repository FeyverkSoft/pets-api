namespace Asp.Core
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    internal sealed class AspNetCoreHeaderPathBaseMiddleware
    {
        private readonly String _basePathKey;
        private readonly RequestDelegate _next;

        public AspNetCoreHeaderPathBaseMiddleware(RequestDelegate next, String basePathKey)
        {
            _next = next;
            _basePathKey = basePathKey;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.ContainsKey(_basePathKey))
                context.Request.PathBase = new PathString(context.Request.Headers[_basePathKey].First());

            await _next(context);
        }
    }
}