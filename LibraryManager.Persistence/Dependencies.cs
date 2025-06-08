namespace LibraryManager.Persistence
{
    using LibraryManager.Core.Repositories;
    using LibraryManager.Persistence.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class Dependencies
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LibraryDbContext>(c
                => c.UseSqlServer(configuration.GetConnectionString("LibraryDbContextConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBorrowerRepository, BorrowerRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ILoanRepository, LoanRepository>();

            return services;
        }
    }
}
