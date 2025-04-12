using Library.Application.Services;
using Library.Core.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Application
{
    public static class Dependencies
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILoanService, LoanService>();
            services.AddScoped<IAuthorService, AuthorService>();

            return services;
        }
    }
}
