using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherServer.Data;
using WeatherServer.Models;

namespace WeatherServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController(WeatherContext context) : ControllerBase
    {
        // GET: api/Weather
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> GetWeatherForecasts()
        {
            return await context.WeatherForecasts.ToListAsync();
        }

        // GET: api/Weather/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WeatherForecast>> GetWeatherForecast(int id)
        {
            var weatherForecast = await context.WeatherForecasts.FindAsync(id);

            if (weatherForecast == null)
            {
                return NotFound();
            }

            return weatherForecast;
        }

        //GET: api/Weather/city/city
        [HttpGet("city/{city}")]
        public async Task<ActionResult<WeatherForecast>> GetWeatherForecastByCity(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
                return BadRequest("City cannot be null or empty");

            var forecast = await context.WeatherForecasts
                .FirstOrDefaultAsync(w => w.City.ToLower() == city.ToLower());

            return forecast != null 
                ? Ok(forecast) 
                : NotFound($"No weather data found for city: {city}");
        }
    }
}