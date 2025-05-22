namespace LibraryManager.Application
{
    using FluentValidation;
    using Microsoft.Extensions.DependencyInjection;

    public static class Dependencies
    {
        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(Dependencies).Assembly);

            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssemblies(typeof(Dependencies).Assembly);
            });

            return services;
        }
    }
}
