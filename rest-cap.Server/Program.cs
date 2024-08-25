using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using rest_cap.Server.Entities;
using rest_cap.Server.Helpers;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<StorageContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IApiKeyValidator, ApiKeyValidatorHelper>();
builder.Services.AddScoped<ApiAccessAuthorizeFilter>();

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter(policyName: "GlobalRateLimit", option =>
    {
        option.PermitLimit = 10;
        option.Window = TimeSpan.FromMinutes(1);
        option.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        option.QueueLimit = 5;
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("allowspecificorigin", policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

var app = builder.Build();

app.UseRateLimiter();
app.UseMiddleware<RateLimitMiddleware>();

app.UseDefaultFiles();
app.UseStaticFiles();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("allowspecificorigin"); // Apply CORS policy

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
