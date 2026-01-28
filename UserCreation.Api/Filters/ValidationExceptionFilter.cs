using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace UserCreation.Api.Filters
{
    public class ValidationExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ArgumentException)
            {
                context.Result = new BadRequestObjectResult(new { error = context.Exception.Message });
            }
        }
    }
}
