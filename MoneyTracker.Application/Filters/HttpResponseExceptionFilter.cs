using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MoneyTracker.Application.Constants;
using System.Net;
using System.Security.Authentication;

namespace MoneyTracker.Application.Filters;

public class HttpResponseExceptionFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case AuthenticationException _:
                SetErrorResponse(context.Exception.Message, HttpStatusCode.Forbidden, context);
                return;
            case KeyNotFoundException _:
                SetErrorResponse(context.Exception.Message, HttpStatusCode.NotFound, context);
                return;
            default:
                SetErrorResponse(Messages.SomethingWentWrong, HttpStatusCode.InternalServerError, context);
                return;
        }
    }

    private static void SetErrorResponse(string userErrorMessage, HttpStatusCode statusCode, ExceptionContext context)
    {
        context.Result = new ObjectResult(userErrorMessage)
        {
            StatusCode = (int)statusCode
        };
    }
}