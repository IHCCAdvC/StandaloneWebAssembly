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
dotnet sln add ./WeatherServer/WeatherServer.csproj 
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
│   ├── WeatherClient.csproj
│   ├── Program.cs
│   ├── App.razor
│   ├── Pages/
│   │   ├── Index.razor
│   │   ├── Counter.razor
│   │   └── FetchData.razor
│   ├── Shared/
│   │   ├── MainLayout.razor
│   │   └── NavMenu.razor
│   └── wwwroot/
│       ├── css/
│       ├── lib/
│       └── index.html
└── WeatherServer/
    ├── WeatherServer.csproj
    ├── Program.cs
    ├── Controllers/
    │   └── WeatherForecastController.cs
    ├── Models/
    │   └── WeatherForecast.cs
    └── Properties/
        └── launchSettings.json
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

### DB

Migration

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Controlelr

```bash
dotnet aspnet-codegenerator controller -name WeatherController -async -api -m WeatherForcast -dc WeatherContext -outDir Controllers
```

## Stand-alone WebAssembly client


