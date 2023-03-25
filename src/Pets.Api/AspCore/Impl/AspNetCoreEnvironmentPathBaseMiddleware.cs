namespace Pets.Api.AspCore.Impl
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    internal sealed class AspNetCoreEnvironmentPathBaseMiddleware
    {
        private readonly String _basePath;
        private readonly RequestDelegate _next;

        public AspNetCoreEnvironmentPathBaseMiddleware(RequestDelegate next, String basePath)
        {
            _next = next;
            _basePath = basePath;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Request.PathBase = _basePath;
            await _next(context);
        }
    }
}