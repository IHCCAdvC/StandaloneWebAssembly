using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherServer.Models;

public class WeatherForecast
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string City { get; set; }
    
    
    [Required]
    public DateOnly Date { get; set; }

    [Required]
    [Range(-50, 50)]
    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    
    [Required]
    [StringLength(50)]
    //TODO make this a ENUM with (Sunny, cloudy, etc.)
    public string Summary { get; set; }
}