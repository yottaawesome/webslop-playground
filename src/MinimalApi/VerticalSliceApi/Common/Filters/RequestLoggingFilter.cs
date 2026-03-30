using System.Diagnostics;

namespace VerticalSliceApi.Common.Filters;

public class RequestLoggingFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        var httpContext = context.HttpContext;
        var method = httpContext.Request.Method;
        var path = httpContext.Request.Path;

        var logger = httpContext.RequestServices.GetRequiredService<ILoggerFactory>()
            .CreateLogger("RequestLogging");

        logger.LogInformation("→ {Method} {Path}", method, path);

        var stopwatch = Stopwatch.StartNew();
        var result = await next(context);
        stopwatch.Stop();

        logger.LogInformation("← {Method} {Path} completed in {Elapsed}ms",
            method, path, stopwatch.ElapsedMilliseconds);

        return result;
    }
}
