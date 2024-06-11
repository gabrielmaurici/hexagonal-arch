using Hexagonal.Arch.Infra.Db;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDb(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/weatherforecast", () =>
{

})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();