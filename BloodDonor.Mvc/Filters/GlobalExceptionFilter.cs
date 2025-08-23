using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BloodDonor.Mvc.Filters
{
    public class GlobalExceptionFilter : IAsyncExceptionFilter
    { 
        public Task OnExceptionAsync(ExceptionContext context)
        {
            var response = new
            {
                error = "An unexpected error occurred.",
                detail = context.Exception.Message
            };
            context.Result = new ObjectResult(response)
            {
                StatusCode = 500
            };
            context.ExceptionHandled = true;
            return Task.CompletedTask;
        }
    }
}
