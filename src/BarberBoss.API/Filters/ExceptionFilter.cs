using System.Net;
using BarberBoss.Communication.DTOs.Response;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BarberBoss.API.Filters;

/// <summary>
/// Custom exception filter
/// </summary>
public class ExceptionFilter : IExceptionFilter
{
    /// <summary>
    /// Filter the exceptions
    /// </summary>
    /// <param name="context">Context about the exception</param>
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is BarberBossException)
        {
            HandleProjectException(context);
        }
        else
        {
            HandleUnknownError(context);
        }
    }

    /// <summary>
    /// Builds the body of exceptions related to the project
    /// </summary>
    /// <param name="context">Context about the exception</param>
    private void HandleProjectException(ExceptionContext context)
    {
        var barberBossException = (BarberBossException)context.Exception;
        var errorResponse = new ResponseErrorJson(barberBossException.GetErrors());

        context.HttpContext.Response.StatusCode = barberBossException.StatusCode;
        context.Result = new ObjectResult(errorResponse);
    }

    /// <summary>
    /// Handle unknown exceptions
    /// </summary>
    /// <param name="context">Context about the exception</param>
    private void HandleUnknownError(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson(ResourceErrorMessages.UNKNOWN_ERROR);
        
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}