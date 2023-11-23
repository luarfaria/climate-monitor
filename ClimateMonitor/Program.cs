//using ClimateMonitor.CustomProblemDetails;
using ClimateMonitor.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddTransient<AlertService>();
builder.Services.AddTransient<DeviceSecretValidatorService>();
builder.Services.AddTransient<DeviceFirmwareValidatorService>();
//builder.Services.AddTransient<ProblemDetailsFactory, CustomProblemDetailsFactory>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
