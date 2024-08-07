using Hexagonal.Arch.Api.Endpoints;
using Hexagonal.Arch.Infra.Db;
using Hexagonal.Arch.Application;
using Hexagonal.Arch.Infra.IntegrationViaCepApi;
using Hexagonal.Arch.Infra.IntegrationAwsS3;
using Hexagonal.Arch.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDb();
builder.Services.AddApplicationServices();
builder.Services.AddIntegrationViaCepApi(builder.Configuration);
builder.Services.AddIntegrationAwsS3(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

CustomerEndpoints.RegisterCustomersEndpoints(app);
app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.Run();