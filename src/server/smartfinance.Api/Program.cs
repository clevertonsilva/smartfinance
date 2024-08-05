using smartfinance.Api.Extensions;
using smartfinance.Api.Middleware;
using smartfinance.Application;
using smartfinance.Domain;
using smartfinance.Infra.Data;
using smartfinance.Infra.Identity;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddApplication();
builder.Services.AddDomain();
builder.Services.AddInfraIdentity(builder.Configuration);
builder.Services.AddInfraData(builder.Configuration);
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler();

app.Run();
