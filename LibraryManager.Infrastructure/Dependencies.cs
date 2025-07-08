namespace LibraryManager.Infrastructure
{
    using LibraryManager.Core.Abstractions;
    using LibraryManager.Infrastructure.Auth;
    using LibraryManager.Infrastructure.BackgroundJobs.ProcessCanceledLoanStatus;
    using LibraryManager.Infrastructure.BackgroundJobs.ProcessOverdueLoanStatus;
    using LibraryManager.Infrastructure.Logging;
    using LibraryManager.Infrastructure.Password;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Quartz;
    using System.Text;

    public static class Dependencies
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtInfoOptions>(
                configuration.GetSection("JwtInfo"));

            services.AddAuth(configuration);

            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<ILogContextEnricher, LogContextEnricher>();
            services.AddScoped<ITokenProvider, TokenProvider>();

            services.AddQuartz();
            services.AddQuartzHostedService(options =>
            {
                options.WaitForJobsToComplete = true;
            });

            services.ConfigureOptions<ProcessOverdueLoanStatusJobSetup>();
            services.ConfigureOptions<ProcessCanceledLoanStatusJobSetup>();

            return services;
        }

        private static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorization();

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["JwtInfo:Issuer"],
                        ValidAudience = configuration["JwtInfo:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                            configuration["JwtInfo:Secret"]))
                    };
                });

            return services;
        }
    }
}
