using Microsoft.AspNetCore.Mvc.Filters;

namespace Infrastructure.RateLimit.Interfaces;

public interface IRateLimitFilter
{
    Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next);
}