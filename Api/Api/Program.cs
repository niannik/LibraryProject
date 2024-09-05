using Api;
using Api.Middlewares;
using Application;
using Infrastructure;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerUI;
using ConfigureServices = Api.ConfigureServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureService(builder.Configuration, builder.Environment);
builder.Services.AddApiServices(builder.Configuration);

builder.Services.AddControllers();

builder.Logging.ClearProviders();

Serilog.Debugging.SelfLog.Enable(Console.WriteLine);
builder.Host.UseSerilog((context, configuration) =>
{
configuration.ReadFrom.Configuration(context.Configuration);

var seqUrl = builder.Configuration["SeqUrl"];

if (seqUrl is not null)
{
configuration.WriteTo.Seq(seqUrl);
}
});


builder.Services.AddSwaggerGen();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

await InitializeDatabaseAsync(app);

if (app.Environment.IsProduction() == false)
{
app.UseSwagger();
app.UseSwaggerUI(options =>
{
options.DisplayRequestDuration();
options.DocExpansion(DocExpansion.None);
});
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseStaticFiles();


app.UseAuthentication();
app.UseAuthorization();

// do not touch options parameter please
app.UseExceptionHandler(options => { });

app.MapControllers();

app.Run();

static async Task InitializeDatabaseAsync(IApplicationBuilder app)
{
await using var scope = app.ApplicationServices.CreateAsyncScope();

var initializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();

await initializer.CreateDatabaseAsync();
}

public partial class Program { }