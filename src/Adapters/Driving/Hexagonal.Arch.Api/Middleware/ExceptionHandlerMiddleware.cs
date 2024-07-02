namespace Hexagonal.Arch.Api.Middleware;

public class ExceptionHandlerMiddleware(RequestDelegate _next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionMessageAsync(context, ex);
        }
    }

    private static Task HandleExceptionMessageAsync(HttpContext context, Exception exception)
    {
        if (exception.GetType() == typeof(KeyNotFoundException)) 
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
        }

        if (exception.GetType() == typeof(FormatException) || exception.GetType() == typeof(NullReferenceException))
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
        }

        context.Response.ContentType = "text";
        return context.Response.WriteAsync(exception.Message);
    }
}