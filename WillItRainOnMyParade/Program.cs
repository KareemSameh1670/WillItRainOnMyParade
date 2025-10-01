using WillItRainOnMyParade.BLL.Interfaces;
using WillItRainOnMyParade.BLL.Services;
using WillItRainOnMyParade.DAL.Clients;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddHttpClient<INasaWeatherClient, NasaWeatherClient>();
builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
