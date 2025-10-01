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

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()   // allow requests from any domain
            .AllowAnyHeader()   // allow any headers
            .AllowAnyMethod();  // allow GET, POST, PUT, DELETE, etc.
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
