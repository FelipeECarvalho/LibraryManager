namespace LibraryManager.Application
{
    using FluentValidation;
    using LibraryManager.Application.Behaviors;
    using LibraryManager.Application.UseCases;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;

    public static class Dependencies
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(Dependencies).Assembly, includeInternalTypes: true);

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionResiliencePipelineBehaviour<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestLoggingPipelineBehavior<,>));

            services.AddScoped(typeof(CancelLoansUseCase));
            services.AddScoped(typeof(NotifyNearOverdueLoansUseCase));
            services.AddScoped(typeof(OverdueLoansFeeUseCase));
            services.AddScoped(typeof(OverdueLoansUseCase));

            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssemblies(typeof(Dependencies).Assembly);
            });

            return services;
        }
    }
}
