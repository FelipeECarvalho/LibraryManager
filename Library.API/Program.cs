namespace LibraryManager.API
{
    using LibraryManager.API.Middleware;
    using LibraryManager.Application.Mappings;
    using LibraryManager.Application.Services;
    using LibraryManager.Persistence;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddPersistence(builder.Configuration)
                .AddVersioning();

            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<BookService>();
            builder.Services.AddScoped<AuthorService>();
            builder.Services.AddScoped<LoanService>();

            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.Run();
        }
    }
}
