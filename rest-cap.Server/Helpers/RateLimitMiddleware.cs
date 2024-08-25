namespace rest_cap.Server.Helpers
{
    public class RateLimitMiddleware
    {
        private readonly RequestDelegate _next;

        public RateLimitMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == StatusCodes.Status429TooManyRequests)
            {
                context.Response.ContentType = "application/json";
                var response = new
                {
                    message = "You have exceeded the number of allowed requests. Please try again later."
                };
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
