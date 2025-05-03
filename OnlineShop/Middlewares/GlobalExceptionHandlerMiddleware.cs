﻿using OnlineShop.Exceptions;
using OnlineShop.Features;
using System.Text.Json;

namespace OnlineShop.Middlewares;

public class GlobalExceptionHandlerMiddleware(RequestDelegate next)
{  
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (BadRequestException ex)
        {
            await SetContext(context, ex.Message, StatusCodes.Status400BadRequest);
        }
        catch (NotFoundException ex)
        {
            await SetContext(context, ex.Message, StatusCodes.Status404NotFound);
        }
        catch (TooManyRequestException ex)
        {
            await SetContext(context, ex.Message, StatusCodes.Status429TooManyRequests);
        }
        catch (Exception ex)
        {
            await SetContext(context, ex.Message, StatusCodes.Status500InternalServerError);
        }

    }

    private static async Task SetContext(HttpContext context, string message, int statusCode)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        var result = BaseResult.Fail(message);
        await context.Response.WriteAsync(JsonSerializer.Serialize(result));
    }
}
