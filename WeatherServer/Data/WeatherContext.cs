using Microsoft.EntityFrameworkCore;
using WeatherServer.Models;

namespace WeatherServer.Data;

public class WeatherContext(DbContextOptions<WeatherContext> options) : DbContext(options)
{
    public DbSet<WeatherForecast> WeatherForecasts { get; set; } = null!;
}