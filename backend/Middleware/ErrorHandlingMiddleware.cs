
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using EduAdmin.Common.Model;

namespace EduAdmin.Middleware;

public class ErrorHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try {
            await next(context);
        } 
        catch (ApplicationException ex)
        {
             await HandlerException(
                context,
                StatusCodes.Status400BadRequest,
                [ex.Message]
            );
        } 
        catch (Exception ex){
            await HandlerException(
                context,
                StatusCodes.Status500InternalServerError,
                [ex.Message]
            );
        }
    }
    
    public static async Task HandlerException(HttpContext context, int statusCode, List<string> errors)
    {
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(new ResponseError("Error", statusCode, errors));
    }
}