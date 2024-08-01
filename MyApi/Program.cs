
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Services;
using WebFramework;

namespace MyApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(); 
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(@"Server=127.0.0.1; Port= 5432; Database= LibraryApi; user Id = postgres; Password = 147852");
            });
            builder.Services.AddControllers();
            builder.Services.AddScoped<BookServices>();
            builder.Services.AddScoped<AuthorServices>();
            builder.Services.AddScoped<BorrowedBookServices>();
            builder.Services.AddScoped<CategoryServices>();
            builder.Services.AddScoped<UserServices>();
            builder.Services.AddScoped<AuthServices>();
            builder.Services.AddScoped<JwtServices>();
            builder.Services.AddScoped<UserBooksServices>();
         
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            //builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCustomIdentity();
            builder.Services.AddJwtAuthentication();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
