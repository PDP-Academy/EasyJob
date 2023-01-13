using EasyJob.Domain.Exceptions;
using System;
using System.Net;
using System.Text.Json;

namespace EasyJob.Api.Middlewares;

public class GlobalExceptionHandlingMiddleware 
{
    private readonly RequestDelegate _next;
    
    public GlobalExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch(ValidationException validationException)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            await HandleExceptionAsync(httpContext, validationException.Message);
        }
        catch(NotFoundException notFoundException)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;

            var serializedObject = JsonSerializer.Serialize(new
            {
                notFoundException.Message
            });

            await HandleExceptionAsync(httpContext, serializedObject);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex.Message);
        }
    }
    private async Task HandleExceptionAsync(HttpContext context, string message)
    {
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsync(message);
    }
}