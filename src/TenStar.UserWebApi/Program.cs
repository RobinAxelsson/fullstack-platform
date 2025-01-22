
using TenStar.App;

namespace TenStar.UserWebApi;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        builder.Services.AddCors(options =>
        {
           options.AddPolicy("AllowUserWeb", policy =>
           {
                policy.WithOrigins("http://localhost:5147", "https://localhost:5148", "http://localhost:88", "http://localhost", "http://TenStar.UserWeb")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
           });
        });

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSingleton<TenStarAppFacade>();

        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseCors("AllowUserWeb");
            app.MapOpenApi();
        }
        else
        {
            app.UseHttpsRedirection();
        }

        var tenStarAppFacade = new TenStarAppFacade();

        var summaries = new[]
        { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

        app.MapGet("/api/weatherforecast", () =>
        {
            var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
                .ToArray();
            return forecast;
        })
        .WithName("GetWeatherForecast");

        app.MapControllers();

        app.Run();
    }
}

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
