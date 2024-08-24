using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace rest_cap.Server.Helpers
{
    public class ApiAccessAuthorizeFilter : IAsyncActionFilter
    {
        private const string ApiKeyHeaderName = "api-key";
        private readonly IApiKeyValidator _apiKeyValidator;

        public ApiAccessAuthorizeFilter(IApiKeyValidator apiKeyValidator)
        {
            _apiKeyValidator = apiKeyValidator;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            bool isValidApiKey = await _apiKeyValidator.IsApiKeyValidAsync(extractedApiKey);
            if (!isValidApiKey)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
}
