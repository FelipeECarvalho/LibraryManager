namespace LibraryManager.Infrastructure
{
    using LibraryManager.Application.Abstractions;
    using LibraryManager.Application.Abstractions.Email;
    using LibraryManager.Infrastructure.Auth;
    using LibraryManager.Infrastructure.BackgroundJobs.ProcessCanceledLoanStatus;
    using LibraryManager.Infrastructure.BackgroundJobs.ProcessNearOverdueLoanStatus;
    using LibraryManager.Infrastructure.BackgroundJobs.ProcessOverdueLoanStatus;
    using LibraryManager.Infrastructure.Email;
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
            services.AddAuth(configuration);
            services.AddEmail(configuration);

            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<ILogContextEnricher, LogContextEnricher>();
            services.AddScoped<ITokenProvider, TokenProvider>();

            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IEmailSender, SmtpEmailSender>();

            services.AddQuartz();
            services.AddQuartzHostedService(options =>
            {
                options.WaitForJobsToComplete = true;
            });

            services.ConfigureOptions<ProcessOverdueLoanStatusJobSetup>();
            services.ConfigureOptions<ProcessNearOverdueLoanStatusJobSetup>();
            services.ConfigureOptions<ProcessCanceledLoanStatusJobSetup>();

            return services;
        }

        private static void AddEmail(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailOptions>(
                configuration.GetSection("Email"));

            var emailOptions = configuration
                .GetSection("Email").Get<EmailOptions>();

            services
                .AddFluentEmail(emailOptions.SenderEmail, emailOptions.Sender)
                .AddSmtpSender(emailOptions.Host, emailOptions.Port);
        }

        private static void AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorization();

            services.Configure<JwtInfoOptions>(
                configuration.GetSection("JwtInfo"));

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    var JwtOptions = configuration
                        .GetSection("JwtInfo").Get<JwtInfoOptions>();

                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = JwtOptions.Issuer,
                        ValidAudience = JwtOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                            JwtOptions.Secret))
                    };
                });
        }
    }
}
