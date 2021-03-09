using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Map.API.Helpers.SharedResponse
{

    public class ValidateModelAsyncActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                throw new ShredValidationException(context.ModelState, true);
            }
            await next();
        }
    }
}
