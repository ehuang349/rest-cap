using Microsoft.EntityFrameworkCore;

namespace rest_cap.Server.Entities
{
    public class ApiKeyMiddleware
    {
        private const string ApiKeyHeaderName = "api-key";
        private readonly RequestDelegate _requestDelegate;
        private readonly StorageContext _storageContext;

        public ApiKeyMiddleware(RequestDelegate requestDelegate, StorageContext storageContext)
        {
            _requestDelegate = requestDelegate;
            _storageContext = storageContext;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
            {
                var apiKeyExists = await _storageContext.ApiAccess
                    .AnyAsync(a => a.ApiKey == extractedApiKey && a.IsActive);

                if (apiKeyExists)
                {
                    await _requestDelegate(context); // API key is valid, continue processing
                    return;
                }
            }

            context.Response.StatusCode = StatusCodes.Status401Unauthorized; // Invalid API key
        }
    }
}
