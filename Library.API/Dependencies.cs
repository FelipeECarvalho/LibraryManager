namespace Library.API
{
    using Microsoft.EntityFrameworkCore;
    using Library.Core.Repositories;
    using Library.Infrastructure;
    using Library.Persistence.Repositories;

    public static class Dependencies
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LibraryDbContext>(c
                => c.UseSqlServer(configuration.GetConnectionString("LibraryDbContextConnection")));

            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILoanRepository, LoanRepository>();

            return services;
        }
    }
}
