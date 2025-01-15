using Journey.Exception.ExceptionsBase;
using Journey.Exception;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Journey.Communication.Responses;

namespace Journey.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is JourneyException)
        {
            JourneyException journeyException = (JourneyException)context.Exception;

            context.HttpContext.Response.StatusCode = (int)journeyException.GetStatusCode();

            ResponseErrorsJson responseJson = new ResponseErrorsJson(journeyException.GetErrorMessages());

            context.Result = new ObjectResult(responseJson);
        }
        else
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            ResponseErrorsJson responseJson = new ResponseErrorsJson(new List<string> { ResourceErrorMessages.ERROR_UNKNOWN });

            context.Result = new ObjectResult(responseJson);
        }
    }
}
