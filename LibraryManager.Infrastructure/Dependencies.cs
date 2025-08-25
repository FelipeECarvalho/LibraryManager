namespace LibraryManager.Infrastructure
{
    using LibraryManager.Application.Abstractions;
    using LibraryManager.Application.Abstractions.Email;
    using LibraryManager.Infrastructure.Auth;
    using LibraryManager.Infrastructure.BackgroundJobs;
    using LibraryManager.Infrastructure.BackgroundJobs.ScheduledJobs;
    using LibraryManager.Infrastructure.BackgroundJobs.ScheduledJobs.ProcessCancelLoans;
    using LibraryManager.Infrastructure.BackgroundJobs.ScheduledJobs.ProcessNearOverdueLoans;
    using LibraryManager.Infrastructure.BackgroundJobs.ScheduledJobs.ProcessOverdueLoans;
    using LibraryManager.Infrastructure.BackgroundJobs.ScheduledJobs.ProcessOverdueLoansFee;
    using LibraryManager.Infrastructure.Constants;
    using LibraryManager.Infrastructure.Email;
    using LibraryManager.Infrastructure.Logging;
    using LibraryManager.Infrastructure.Password;
    using LibraryManager.Infrastructure.Resilience;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Tokens;
    using Polly;
    using Polly.Retry;
    using Quartz;
    using System.Text;

    public static class Dependencies
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuth(configuration);
            services.AddEmail(configuration);
            services.AddResilience();

            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<ILogContextEnricher, LogContextEnricher>();
            services.AddScoped<ITokenProvider, TokenProvider>();

            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IEmailSender, SmtpEmailSender>();

            services.AddQuartz();
            services.AddQuartzHostedService(options =>
            {
                options.WaitForJobsToComplete = true;
            });

            services.ConfigureOptions<ProcessOverdueLoansJobSetup>();
            services.ConfigureOptions<ProcessNearOverdueLoansJobSetup>();
            services.ConfigureOptions<ProcessCancelLoansJobSetup>();
            services.ConfigureOptions<ProcessOverdueLoansFeeJobSetup>();

            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            services.Configure<JobSchedulesOptions>(
                configuration.GetSection(JobSchedulesOptions.SectionName)
            );

            services.Configure<BackgroundJobOptions>(
                configuration.GetSection(BackgroundJobOptions.SectionName)
            );

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

        private static void AddResilience(this IServiceCollection services)
        {
            services.AddResiliencePipeline(ResiliencePipelineConstants.ImmediatelyRetry, (builder, context) =>
            {
                var loggerFactory = context.ServiceProvider.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger("PollyPipelines");

                builder.AddRetry(new RetryStrategyOptions
                {
                    MaxRetryAttempts = 3,
                    BackoffType = DelayBackoffType.Constant,
                    Delay = TimeSpan.Zero,
                    ShouldHandle = new PredicateBuilder()
                        .Handle<HttpRequestException>()
                        .Handle<TimeoutException>()
                        .Handle<ApplicationException>(),
                    OnRetry = retryArguments =>
                    {
                        logger.LogWarning(
                            "Failed attempt {AttemptNumber} Exception: {ExceptionType} - {ExceptionMessage}",
                            retryArguments.AttemptNumber,
                            retryArguments.Outcome.Exception?.GetType().Name,
                            retryArguments.Outcome.Exception?.Message);

                        return ValueTask.CompletedTask;
                    }
                });
            });

            services.AddScoped<ITransaction, ResilientTransaction>();
        }
    }
}
