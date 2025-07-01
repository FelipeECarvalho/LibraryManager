namespace LibraryManager.API
{
    using LibraryManager.API.Middleware;
    using LibraryManager.Application;
    using LibraryManager.Infrastructure;
    using LibraryManager.Persistence;
    using Serilog;

    internal static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog((context, loggerConfig) => 
                loggerConfig.ReadFrom.Configuration(context.Configuration) );

            builder.Services
                .AddPersistence(builder.Configuration)
                .AddInfrastructure(builder.Configuration)
                .AddVersioning()
                .AddSwaggerGenWithAuthentication()
                .AddApplication();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<RequestLogContextMiddleware>();

            app.UseSerilogRequestLogging();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.Run();
        }
    }
}
