# Full Stack Weather App

Using What we learned in [previous](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mongo-app?view=aspnetcore-9.0&tabs=visual-studio) units about stand-alone web API we will build a weather API.
We will also build a Stand-alone WebAssembly client app that runs .NET in browser.

## Creating the Projects

We want two **projects** in one solution. 
To do this run the two `dotnet new` commands to make the front end to the back end. 
Then the last command links the two projects in the `.sln` file in the root. 

```bash
dotnet new webapi -o WeatherServer --use-controllers
dotnet new blazorwasm -o WeatherClient
dotnet new sln --name WeatherApp
dotnet sln add ./WeatherServer/WeatherServer.csproj
dotnet sln add ./WeatherClient/WeatherClient.csproj 
```

To run the project `cd` to each project in two different terminals windows and run them as normal. 

```bash
cd WeatherServer
dotnet run 
cd ..
cd WeatherClient
dotnet run
```

The new file structure should be the following: 

```
WeatherSolution/
├── WeatherSolution.sln
├── WeatherClient/
│   ├── Program.cs
│   ├── App.razor
│   ├── Pages/
└── WeatherServer/
    ├── WeatherServer.csproj
    ├── Program.cs
    ├── Controllers/
    │   └── WeatherForecastController.cs
    ├── Models/
    │   └── WeatherForecast.cs
```


## Web API

### NuGet

```bash
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet tool uninstall -g dotnet-aspnet-codegenerator
dotnet tool install -g dotnet-aspnet-codegenerator
dotnet tool update -g dotnet-aspnet-codegenerator
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
```

### Model

Build the `Models/WeatherForecast.cs` class.

```csharp
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
    public string Summary { get; set; }
}
```

### DB

Run a migration.

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Make a script to seed your db called `Data/SeedData.cs`. 

```csharp
public abstract class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new WeatherContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<WeatherContext>>());

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
            //etc
        );

        context.SaveChanges();
    }
}
```

### Controller

Build out the controller in `Controllers/WeatherController.cs`. 

## Stand-alone WebAssembly client

Lets start Building the client. 


### DTO

Build out a DTO for the client. 

```csharp
public class WeatherForecastDTO
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public int TemperatureC { get; set; }
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    public string? Summary { get; set; }
    public string? City { get; set; }
}
```

### Config

Add an `appsettings.json` file with URL to the backend api. 
**YOUR PORT** might be different. 

```json
{
  "APIBaseAddress": "http://localhost:5066/"
}
```

Add the following to the **Severs** `project.js`

```csharp
app.UseCors(policy => policy
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
```


### Blazor

In `Pages/Home.razor` build our a page to load in data from the Server. 