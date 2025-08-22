namespace LibraryManager.API
{
    using LibraryManager.API.Exceptions;
    using LibraryManager.API.Middleware;
    using LibraryManager.Application;
    using LibraryManager.Infrastructure;
    using LibraryManager.Persistence;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Hybrid;
    using Microsoft.Extensions.Hosting;
    using Serilog;

    internal class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddProblemDetails(config =>
            {
                config.CustomizeProblemDetails = context =>
                {
                    context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);
                };
            });

            builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

            builder.Host.UseSerilog((context, loggerConfig) =>
                loggerConfig.ReadFrom.Configuration(context.Configuration));

            builder.Services
                .AddPersistence(builder.Configuration)
                .AddInfrastructure(builder.Configuration)
                .AddVersioning()
                .AddSwaggerGenWithAuthentication()
                .AddRateLimiter()
                .AddApplication();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddHybridCache(options =>
            {
                options.DefaultEntryOptions = new HybridCacheEntryOptions
                {
                    LocalCacheExpiration = TimeSpan.FromMinutes(1),
                    Expiration = TimeSpan.FromMinutes(5)
                };
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
            await db.Database.MigrateAsync();

            app.UseHttpsRedirection();

            app.UseMiddleware<RequestLogContextMiddleware>();

            app.UseSerilogRequestLogging();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseRateLimiter();

            app.MapControllers();

            app.UseExceptionHandler();

            app.Run();
        }
    }
}
