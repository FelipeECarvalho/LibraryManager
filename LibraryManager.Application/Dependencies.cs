namespace LibraryManager.Application
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Common;
    using Microsoft.Extensions.DependencyInjection;

    public static class Dependencies
    {
        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddScoped<IMediator, Mediator>();

            services.Scan(scan => scan.FromAssembliesOf(typeof(Dependencies))
                .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)), publicOnly: false)
                    .AsImplementedInterfaces()
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)), publicOnly: false)
                    .AsImplementedInterfaces()
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)), publicOnly: false)
                    .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        }
    }
}
