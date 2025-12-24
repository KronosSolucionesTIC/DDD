using System.Net;
using System.Text.Json;
using DDD.Api.Common;
using DDD.Domain.Exceptions;

namespace DDD.Api.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(
        RequestDelegate next,
        ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DomainException ex)
        {
            _logger.LogWarning(ex, "Domain error");
            await WriteErrorAsync(
                context,
                HttpStatusCode.BadRequest,
                ex.Message
            );
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Validation error");
            await WriteErrorAsync(
                context,
                HttpStatusCode.BadRequest,
                ex.Message
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled error");
            await WriteErrorAsync(
                context,
                HttpStatusCode.InternalServerError,
                "Ocurrió un error inesperado"
            );
        }
    }

    private static async Task WriteErrorAsync(
        HttpContext context,
        HttpStatusCode statusCode,
        string message)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = ApiResponse<string>.Fail(message);

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response)
        );
    }
}
