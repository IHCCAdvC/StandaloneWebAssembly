using Microsoft.EntityFrameworkCore;
using WeatherServer.Models;

namespace WeatherServer.Data;

public abstract class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new WeatherContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<WeatherContext>>());

        if (context == null || context.WeatherForecasts == null)
        {
            throw new NullReferenceException(
                "Null BlazorWebAppMoviesContext or Movie DbSet");
        }

        if (context.WeatherForecasts.Any())
        {
            return;
        }

        context.WeatherForecasts.AddRange(
            new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                TemperatureC = 22,
                City = "Ottumwa",
                Summary = "Sunny"
            },
            new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                TemperatureC = 25,
                City = "Moberly",
                Summary = "Clear"
            },
            new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(2)),
                TemperatureC = 28,
                City = "St. Louis",
                Summary = "Partly Cloudy"
            },
            new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(3)),
                TemperatureC = 20,
                City = "Montreal",
                Summary = "Cloudy"
            },
            new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(4)),
                TemperatureC = 18,
                City = "Cairo",
                Summary = "Rainy"
            }
        );

        context.SaveChanges();
    }
}