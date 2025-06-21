namespace LibraryManager.Application
{
    using FluentValidation;
    using LibraryManager.Application.Behaviors;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;

    public static class Dependencies
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(Dependencies).Assembly, includeInternalTypes: true);

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssemblies(typeof(Dependencies).Assembly);
            });

            return services;
        }
    }
}
