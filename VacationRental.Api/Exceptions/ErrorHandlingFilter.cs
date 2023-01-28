using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

public class ErrorHandlingFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        Log.ForContext<ErrorHandlingFilter>()
            .Error($"Error ocurred during execution of action {context.ActionDescriptor.DisplayName}. Error message {exception}");

        context.ExceptionHandled = true;
    }
}