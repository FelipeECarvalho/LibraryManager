using Library.Application.Services;
using Library.Core.Interfaces.Services;
using Library.Infrastructure;

namespace Library.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddInfrastructure(builder.Configuration);

            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ILoanService, LoanService>();
            builder.Services.AddScoped<IAuthorService, AuthorService>();

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

            app.Run();
        }
    }
}
