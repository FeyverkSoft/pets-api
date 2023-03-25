namespace Pets.Api.Middlewares;

using System.Net;

using Exceptions;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

public class ErrorHandlingMiddleware : IMiddleware
{
    private static readonly ActionDescriptor EmptyActionDescriptor = new();
    private static readonly RouteData EmptyRouteData = new();

    private readonly IActionResultExecutor<ObjectResult> _executor;
    private readonly ILoggerFactory _loggerFactory;

    public ErrorHandlingMiddleware(IActionResultExecutor<ObjectResult> executor, ILoggerFactory loggerFactory)
    {
        _executor = executor;
        _loggerFactory = loggerFactory;
    }

    /// <summary>Request handling method.</summary>
    /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Http.HttpContext" /> for the current request.</param>
    /// <param name="next">The delegate representing the remaining middleware in the request pipeline.</param>
    /// <returns>A <see cref="T:System.Threading.Tasks.Task" /> that represents the execution of this middleware.</returns>
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exc)
        {
            var logger = _loggerFactory.CreateLogger("System");
            logger.LogCritical(exc, exc.Message);
            var actionContext = new ActionContext(context, context.GetRouteData() ?? EmptyRouteData, EmptyActionDescriptor);
            var resp = new ProblemDetails
            {
                Type = ErrorCodes.InternalServerError,
                Status = (Int32)HttpStatusCode.InternalServerError,
#if DEBUG
                    Detail = exc.Message,
#else
                Detail = "internal server error"
#endif
            };
            resp.Extensions.Add("traceId", context.TraceIdentifier);

            await _executor.ExecuteAsync(actionContext, new ObjectResult(resp)
            {
                StatusCode = (Int32)HttpStatusCode.InternalServerError
            });
        }
    }
}