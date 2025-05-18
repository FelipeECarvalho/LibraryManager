namespace LibraryManager.Application
{
    using Microsoft.Extensions.DependencyInjection;
    using System.Reflection;

    public static class Dependencies
    {
        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddMediatR(configuration => 
            {
                configuration.RegisterServicesFromAssemblies(typeof(Dependencies).Assembly);
            });

            return services;
        }
    }
}
